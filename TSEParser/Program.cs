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
                if (!ProcessarParametros(args))
                    return 2;

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
                    if (!string.IsNullOrWhiteSpace(continuar))
                    {
                        // Está continuando a partir de uma UF e Município específico. Remover as UFs anteriores da lista
                        var arrChave = continuar.Split(@"/");
                        var continuarUF = arrChave[0];

                        var index = UFs.IndexOf(continuarUF);

                        if (index > -1)
                            UFs.RemoveRange(0, index);
                    }

                    foreach (var UF in UFs)
                    {
                        servico.ProcessarUF(UF, continuar, string.Empty);

                        continuar = string.Empty; // Limpar o "continuar" para que as próximas UFs operem normalmente.
                    }
                }
                else if (modoOperacao == ModoOperacao.CarregarUnicaSecao)
                {
                    var arrChave = secaoUnica.Split(@"/");
                    var UF = arrChave[0];

                    servico.ProcessarUF(UF, continuar, secaoUnica);
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

        private static bool ProcessarParametros(string[] args)
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
    -continuar=[chave]      Continua o processamento a partir da chave informada.
                            Formato: UF/CodMunicipio.
                            Exemplo: -carregarsecao=MA/09237/0084/0215

    -segundoturno, -2t      Define que esta é uma apuração de segundo turno.

    -ajuda, -h, -?          Exibe esta mensagem.

";

            if (args == null)
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (arg.ToLower().Equals("-ajuda") || arg.ToLower().Equals("-h") || arg.ToLower().Equals("-?"))
                {
                    Console.WriteLine(textoAjuda);
                    Console.WriteLine("Executar o programa sem nenhum argumento irá baixar todas as UFs no diretório atual.");
                    return false;
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
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
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
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
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
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }

                    if (!Directory.Exists(arr[1]))
                    {
                        Console.WriteLine(@$"Argumento ""dir"" inválido. Diretório ""{arr[1]}"" não existe.");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
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
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }

                    var diretorio = Path.GetDirectoryName(arr[1]);
                    if (!Directory.Exists(diretorio))
                    {
                        Console.WriteLine(@$"Argumento ""parquet"" inválido. Diretório ""{diretorio}"" não existe.");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }

                    caminhoparquet = arr[1];
                    if (File.Exists(caminhoparquet))
                    {
                        Console.WriteLine(@$"Argumento ""parquet"" inválido. Arquivo ""{caminhoparquet}"" já existe.");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                }
                else if (arg.ToLower().StartsWith("-instancia="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""instancia"" inválido. Favor usar ""-instancia=servidor\nome"".");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    instanciabd = arr[1];
                }
                else if (arg.ToLower().StartsWith("-banco="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""banco"" inválido. Favor usar ""-banco=NomeDoBanco"".");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    banco = arr[1];
                }
                else if (arg.ToLower().StartsWith("-usuario="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""usuario"" inválido. Favor usar ""-usuario=username"".");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    usuario = arr[1];
                }
                else if (arg.ToLower().StartsWith("-senha="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""senha"" inválido. Favor usar ""-senha=suasenha"".");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    senha = arr[1];
                }
                else if (arg.ToLower().StartsWith("-carregarsecao="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""carregarsecao"" inválido. Favor usar ""-carregarsecao=[chaves]"".");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    var chave = arr[1];
                    var arrChave = chave.Split(@"/");
                    if (arrChave.Count() != 4)
                    {
                        Console.WriteLine(@"Argumento ""carregarsecao"" inválido. A chave deve ter 4 elementos: UF, Código do Municipio, " +
                                            "Zona Eleitoral e Seção Eleitoral. Exemplo: -carregarsecao=MA/09237/0084/0215");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    secaoUnica = arr[1];
                    modoOperacao = ModoOperacao.CarregarUnicaSecao;
                }
                else if (arg.ToLower().StartsWith("-continuar="))
                {
                    var arr = arg.Split("=");
                    if (arr.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""continuar"" inválido. Favor usar ""-continuar=[chaves]"".");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    var chave = arr[1];
                    var arrChave = chave.Split(@"/");
                    if (arrChave.Count() != 2)
                    {
                        Console.WriteLine(@"Argumento ""continuar"" inválido. A chave deve ter 2 elementos: UF e Código do Municipio. " +
                                            "Exemplo: -continuar=MA/09237");
                        Console.WriteLine("Erro ao executar o programa. Abortando.");
                        return false;
                    }
                    continuar = arr[1];
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

            return true;
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
