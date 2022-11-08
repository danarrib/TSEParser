using ChoETL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TSEParser
{
    internal class Program
    {
        public const string Versao = "1.2";
        public static string diretorioLocalDados { get; set; }
        public static string urlTSE { get; set; }
        public static string IdPleito { get; set; }
        public static List<string> UFs { get; set; }
        public static bool compararIMGBUeBU { get; set; }
        public static string connectionString { get; set; }
        public static string instanciabd { get; set; }
        public static string banco { get; set; }
        public static string usuario { get; set; }
        public static string senha { get; set; }
        public static string caminhoparquet { get; set; }
        public static ModoOperacao modoOperacao { get; set; }
        public static MotorBanco motorBanco { get; set; }
        public static string secaoUnica { get; set; }
        public static bool segundoTurno { get; set; }
        public static string continuar { get; set; }

        static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                ProcessarParametros(args);

                // Criar/Atualizar o banco de dados
                using (var context = new TSEContext(connectionString, motorBanco))
                {
                    var pm = context.Database.GetPendingMigrations();
                    if (pm.Any())
                    {
                        Console.WriteLine("Executando as migrações de Banco de dados. Por favor aguarde...");
                        context.Database.SetCommandTimeout(TimeSpan.FromMinutes(30));
                        context.Database.Migrate();
                    }
                }

                var servico = new ProcessarServico(diretorioLocalDados, urlTSE, compararIMGBUeBU, connectionString, motorBanco, segundoTurno);

                if (modoOperacao == ModoOperacao.Normal)
                {
                    foreach (var UF in UFs)
                    {
                        servico.ProcessarUF(UF, continuar);
                    }
                }
                else if (modoOperacao == ModoOperacao.CarregarUnicaSecao)
                {
                    var arrChave = secaoUnica.Split(@"/");
                    var UF = arrChave[0];
                    var CodMunicipio = arrChave[1];
                    var CodZonaEleitoral = arrChave[2];
                    var CodSecaoEleitoral = arrChave[3];

                    servico.ProcessarUnicaSecao(UF, CodMunicipio, CodZonaEleitoral, CodSecaoEleitoral);
                }
                else if (modoOperacao == ModoOperacao.GerarParquetDoSQL)
                {
                    var pqServico = new ParquetServico();
                    pqServico.GerarParquetDoSQL(connectionString, caminhoparquet, UFs);
                }


                Console.WriteLine("Processo finalizou com sucesso.");
                return 0;
            }
            catch (Exception ex)
            {
                StringBuilder sbTrace = new StringBuilder();

                sbTrace.AppendLine("Name: " + ex.GetType().Name);
                sbTrace.AppendLine("Message: " + ex.Message);
                sbTrace.AppendLine("Stack Trace: " + (ex.StackTrace ?? "null"));

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                    sbTrace.AppendLine("-------------------- Caused by: --------------------");
                    sbTrace.AppendLine("Name: " + ex.GetType().Name);
                    sbTrace.AppendLine("Message: " + ex.Message);
                    sbTrace.AppendLine("Stack Trace: " + (ex.StackTrace ?? "null"));
                }

                Console.WriteLine(sbTrace.ToString());
                return -1;
            }
        }

        private static void ProcessarParametros(string[] args)
        {
            // Inicializar os valores padrão
            instanciabd = @".\SQL2019DEV";
            banco = "TSEParser_T1";
            usuario = string.Empty;
            senha = string.Empty;
            motorBanco = MotorBanco.SqlServer;

            modoOperacao = ModoOperacao.Normal;
            secaoUnica = String.Empty;
            segundoTurno = false;
            continuar = string.Empty;

            diretorioLocalDados = AppDomain.CurrentDomain.BaseDirectory;
            if (!diretorioLocalDados.EndsWith(@"\"))
                diretorioLocalDados += @"\";

            caminhoparquet = diretorioLocalDados + banco + ".parquet";

            IdPleito = "406";
            urlTSE = @"https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/" + IdPleito + @"/";

            compararIMGBUeBU = true;

            UFs = new List<string>();
            UFs.AddRange(new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS",
                "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO", "ZZ" });

            var textoAjuda = @$"TSE Parser Versão {Versao} - Programa para processar os Boletins de Urna.

Parâmetros:

    -instancia=[host\nome]  Especifica o hostname ou IP do servidor do banco de dados e a instância.
                            Caso seja Postgres, usar host:porta. (padrão é ""{instanciabd}"")

    -sqlserver              Define o SQL Server como motor do banco de dados (padrão é Postgres)

    -postgres               Define o SQL Server como motor do banco de dados (padrão é Postgres)

    -banco=[nome]           Especifica nome do banco de dados. (padrão é ""{banco}"")

    -usuario=[login]        Especifica nome de usuário banco de dados. (padrão é ""{usuario}"")

    -senha=[senha]          Especifica a senha do banco de dados. (padrão é ""{senha}"")

    Nota:   Se ""-usuario"" e ""-senha"" não forem informados e o motor de BD for SqlServer, o programa irá 
            assumir que a conexão com o servidor SQL usa a autenticação do Windows. (Trusted_Connection=true)

    -naocompararbu          Faz com que o arquivo bu não seja usado para comparar com o imgbu.
                            (se omitido, o sistema irá decodificar tanto o imgbu quanto o bu, e comparar ambos)

    -gerarparquetdosql      Lê do banco de dados SQL e gera um arquivo no formato Apache Parquet.

    -parquet=[caminho]      Especifica o caminho e nome do arquivo Parquet. (padrão é ""{caminhoparquet}"")

    -pleito=[IdPleito]      Especifica o número do pleito. (padrão é ""{IdPleito}"")

    -ufs=[ListaDeUFs]       Especifica quais UFs deverão ser processadas. Lista separada por vírgulas (SP,RJ,MA).
                            (se omitido, todas as UFs são processadas, inclusive a ""ZZ"" (Exterior))

    -dir=[Diretorio]        Especifica o diretório onde os arquivos estão salvos.
                            (se omitido, irá procurar no diretório atual).

    -carregarsecao=[chave]  Carrega uma unica seção eleitoral informada usando o arquivo BU em vez do IMGBU.
                            Formato: UF/CodMunicipio/ZonaEleitoral/Secao.
                            Exemplo: -carregarsecao=MA/09237/0084/0215
    -continuar=[chave]      Continua o processamento a partir da chave informada. Não deve ser usado junto com ""-ufs""
                            Formato: UF/CodMunicipio/ZonaEleitoral/Secao. (Apenas UF e Município são obrigatórios)
                            Exemplo: -carregarsecao=MA/09237/0084/0215

    -segundoturno, -2t      Define que esta é uma apuração de segundo turno.

    -ajuda, -h, -?          Exibe esta mensagem.

";

            if (args == null)
            {
                return;
            }

            foreach (var arg in args)
            {
                if (arg.ToLower().Equals("-ajuda") || arg.ToLower().Equals("-h") || arg.ToLower().Equals("-?"))
                {
                    Console.WriteLine(textoAjuda);
                    throw new Exception("Executar o programa sem nenhum argumento irá baixar todas as UFs no diretório atual.");
                }
                else if (arg.ToLower() == "-naocompararbu")
                {
                    compararIMGBUeBU = false;
                }
                else if (arg.ToLower() == "-gerarparquetdosql")
                {
                    modoOperacao = ModoOperacao.GerarParquetDoSQL;
                }
                else if (arg.ToLower() == "-sqlserver")
                {
                    motorBanco = MotorBanco.SqlServer;
                }
                else if (arg.ToLower() == "-postgres")
                {
                    motorBanco = MotorBanco.Postgres;
                }
                else if (arg.ToLower() == "-segundoturno" || arg.ToLower() == "-2t")
                {
                    segundoTurno = true;
                }
                else if (arg.ToLower().StartsWith("-pleito="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""pleito"" informado incorretamente. Favor usar ""-pleito=406"", sendo neste caso 406 o número do pleito.");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    IdPleito = arr[1];
                    urlTSE = @"https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/" + IdPleito + @"/";
                }
                else if (arg.ToLower().StartsWith("-ufs="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""ufs"" informado incorretamente. Favor usar ""-ufs=SP,RJ,MA,BA"", informando as UFs desejadas e separando-as com vírgula.");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    var arrUFs = arr[1].Split(",");
                    UFs.Clear();
                    foreach (var uf in arrUFs)
                    {
                        UFs.Add(uf.ToUpper());
                    }
                }
                else if (arg.ToLower().StartsWith("-dir="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""dir"" inválido. Favor usar ""-dir=C:\DiretorioDeSaida"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }

                    if (!Directory.Exists(arr[1]))
                    {
                        Console.WriteLine(@$"Argumento ""dir"" inválido. Diretório ""{arr[1]}"" não existe.");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }

                    diretorioLocalDados = arr[1];
                    if (!diretorioLocalDados.EndsWith(@"\"))
                        diretorioLocalDados += @"\";
                }
                else if (arg.ToLower().StartsWith("-parquet="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""parquet"" inválido. Favor usar ""-parquet=C:\DiretorioDeSaida\arquivo.parquet"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }

                    var diretorio = Path.GetDirectoryName(arr[1]);
                    if (!Directory.Exists(diretorio))
                    {
                        Console.WriteLine(@$"Argumento ""parquet"" inválido. Diretório ""{diretorio}"" não existe.");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }

                    caminhoparquet = arr[1];
                    if (File.Exists(caminhoparquet))
                    {
                        Console.WriteLine(@$"Argumento ""parquet"" inválido. Arquivo ""{caminhoparquet}"" já existe.");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                }
                else if (arg.ToLower().StartsWith("-instancia="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""instancia"" inválido. Favor usar ""-instancia=servidor\nome"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    instanciabd = arr[1];
                }
                else if (arg.ToLower().StartsWith("-banco="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""banco"" inválido. Favor usar ""-banco=NomeDoBanco"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    banco = arr[1];
                }
                else if (arg.ToLower().StartsWith("-usuario="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""usuario"" inválido. Favor usar ""-usuario=username"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    usuario = arr[1];
                }
                else if (arg.ToLower().StartsWith("-senha="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""senha"" inválido. Favor usar ""-senha=suasenha"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    senha = arr[1];
                }
                else if (arg.ToLower().StartsWith("-carregarsecao="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""carregarsecao"" inválido. Favor usar ""-carregarsecao=[chaves]"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    var chave = arr[1];
                    var arrChave = chave.Split(@"/");
                    if (arrChave.Count() != 4)
                    {
                        Console.WriteLine(@"Argumento ""carregarsecao"" inválido. A chave deve ter 4 elementos: UF, Código do Municipio, " +
                                            "Zona Eleitoral e Seção Eleitoral. Exemplo: -carregarsecao=MA/09237/0084/0215");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    secaoUnica = arr[1];
                    modoOperacao = ModoOperacao.CarregarUnicaSecao;
                }
                else if (arg.ToLower().StartsWith("-continuar="))
                {
                    // Não pode usar -continuar e -ufs junto
                    if (args.Where(x => x.StartsWith("-ufs=")).Any())
                    {
                        Console.WriteLine(@"Argumento ""continuar"" não pode ser usado junto com o argumento ""ufs"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }

                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""continuar"" inválido. Favor usar ""-continuar=[chaves]"".");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    var chave = arr[1];
                    var arrChave = chave.Split(@"/");
                    if (arrChave.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""continuar"" inválido. A chave deve ter 2 elementos: UF e Código do Municipio. " +
                                            "Exemplo: -continuar=MA/09237");
                        throw new Exception("Erro ao executar o programa. Abortando.");
                    }
                    continuar = arr[1];
                    UFs = new List<string>();
                    UFs.Add(arrChave[0]);
                }
            }

            if (motorBanco == MotorBanco.SqlServer)
            {
                if (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(senha))
                    connectionString = $"Server={instanciabd};Database={banco};User Id={usuario};Password={senha};";
                else
                    connectionString = $"Server={instanciabd};Database={banco};Trusted_Connection=True;";
            }
            else if (motorBanco == MotorBanco.Postgres)
            {
                if (instanciabd.Contains(":"))
                {
                    var arrinstancia = instanciabd.Split(":");
                    connectionString = $"Server={arrinstancia[0]};Port={arrinstancia[1]};Database={banco};User Id={usuario};Password={senha};";
                }
                else
                {
                    connectionString = $"Server={instanciabd};Database={banco};Username={usuario};Password={senha}";
                }
            }

            var textoApresentacao = $@"TSE Parser Versão {Versao} - Programa para processar os Boletins de Urna.

Diretório:              {diretorioLocalDados}
Pleito:                 {IdPleito}
UFs:                    {string.Join(",", UFs)}
Comparar com BU:        {compararIMGBUeBU.SimOuNao()}
Segundo Turno:          {segundoTurno.SimOuNao()}
Modo de operação:       {modoOperacao}
Chave de Seção:         {secaoUnica}
Continuar de:           {continuar}
Connection String:      {connectionString}
Caminho Parquet:        {caminhoparquet}
";
            Console.WriteLine(textoApresentacao);
        }

    }

    public enum ModoOperacao : byte
    {
        Normal = 0,
        CriarBanco = 1,
        CarregarUnicaSecao = 2,
        GerarParquetDoSQL = 3,
    }
    public enum MotorBanco : byte
    {
        SqlServer = 0,
        Postgres = 1,
    }



}
