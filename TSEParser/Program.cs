using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TSEParser
{
    internal class Program
    {
        public const string Versao = "1.0";
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
        public static ModoOperacao modoOperacao { get; set; }

        public enum ModoOperacao : byte
        {
            Normal = 0,
            CriarBanco = 1,
        }

        private static void ProcessarParametros(string[] args)
        {
            // Inicializar os valores padrão
            connectionString = @"Server=.\SQLEXPRESS;Database=Eleicoes2022;Trusted_Connection=True;";
            instanciabd = @".\SQLEXPRESS";
            banco = "Eleicoes2022";
            usuario = "";
            senha = "";

            diretorioLocalDados = AppDomain.CurrentDomain.BaseDirectory;
            if (!diretorioLocalDados.EndsWith(@"\"))
                diretorioLocalDados += @"\";
            
            IdPleito = "406";
            urlTSE = @"https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/" + IdPleito + @"/";
            
            compararIMGBUeBU = true;
            
            modoOperacao = ModoOperacao.Normal;
            
            UFs = new List<string>();
            UFs.AddRange(new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS",
                "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO", "ZZ" });

            var textoAjuda = @$"TSE Parser Versão {Versao} - Programa para processar os Boletins de Urna.

Parametros:

    -instancia=[host\nome]  Especifica o hostname ou IP do servidor do banco de dados e a instância.
                            (padrão é "".\SQLEXPRESS"")

    -banco=[nome]           Especifica nome do banco de dados. (padrão é ""Eleicoes2022"")

    -usuario=[login]        Especifica nome de usuário banco de dados. (padrão é não informado)

    -senha=[senha]          Especifica nome do banco de dados. (padrão é não informado)

    -criarbanco             Cria o banco de dados.
    
    Nota:   Se ""-usuario"" e ""-senha"" não forem informados, o programa irá assumir que a conexão com o
            servidor SQL usa a autenticação do Windows. (Trusted_Connection=true)

    -naocompararbu      Faz com que o arquivo bu não seja usado para comparar com o imgbu.
                        (por padrão, o sistema irá decodificar tanto o imgbu quanto o bu, e comparar ambos)

    -pleito=[IdPleito]  Especifica o número do pleito. (por padrão é 406)

    -ufs=[ListaDeUFs]   Especifica quais UFs deverão ser processadas. Lista separada por vírgulas (SP,RJ,MA).
                        (por padrão, todas as UFs são processadas, inclusive a ""ZZ"" (Exterior))

    -dir=[Diretorio]    Especifica o diretório onde os arquivos estão salvos.
                        (por padrão, irá procurar no diretório atual).

    -ajuda, -h, -?      Exibe esta mensagem.

";

            if (args == null)
            {
                return;
            }

            foreach (var arg in args)
            {
                if (arg.ToLower().Contains("-ajuda") || arg.ToLower().Contains("-h") || arg.ToLower().Contains("-?"))
                {
                    Console.WriteLine(textoAjuda);
                    throw new Exception("Executar o programa sem nenhum argumento irá baixar todas as UFs no diretório atual.");
                }
                else if (arg.ToLower() == "-criarbanco")
                {
                    modoOperacao = ModoOperacao.CriarBanco;
                }
                else if (arg.ToLower() == "-naocompararbu")
                {
                    compararIMGBUeBU = false;
                }
                else if (arg.ToLower().Contains("-pleito="))
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
                else if (arg.ToLower().Contains("-ufs="))
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
            }

            if(!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(senha))
                connectionString = $"Server={instanciabd};Database={banco};User Id={usuario};Password={senha};";
            else
                connectionString = $"Server={instanciabd};Database={banco};Trusted_Connection=True;";

            var textoApresentacao = $@"TSE Parser Versão {Versao} - Programa para processar os Boletins de Urna.

Diretório:          {diretorioLocalDados}
Pleito:             {IdPleito}
UFs:                {string.Join(",", UFs)}
Comparar com BU:    {compararIMGBUeBU.SimOuNao()}
Connection String:  {connectionString}
";
            Console.WriteLine(textoApresentacao);
        }

        static int Main(string[] args)
        {
            try
            {
                ProcessarParametros(args);

                if (modoOperacao == ModoOperacao.Normal)
                {
                    foreach (var UF in UFs)
                    {
                        var servico = new ProcessarServico(diretorioLocalDados, urlTSE, compararIMGBUeBU, connectionString);
                        servico.ProcessarUF(UF);
                    }
                }
                else if(modoOperacao == ModoOperacao.CriarBanco)
                {
                    var context = new TSEContext(connectionString);
                    context.Database.Migrate();
                    Console.WriteLine("Banco de dados criado/atualizado com sucesso.");
                }

                Console.WriteLine("Processo finalizou com sucesso.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

            /*
            var arquivoimg = @"D:\Downloads\Urnas\RS\88013\0002\0199\634171307730696e616c4f793433645047634a7567313056314b4657773379574476774a78765432714a673d\o00406-8801300020199.imgbu";
            var arquivobu = @"D:\Downloads\Urnas\RS\88013\0002\0199\634171307730696e616c4f793433645047634a7567313056314b4657773379574476774a78765432714a673d\o00406-8801300020199.bu";

            var buServico = new BoletimUrnaServico();
            var imgbu = buServico.ProcessarBoletimUrna(arquivoimg);
            var ebu = buServico.DecodificarArquivoBU(arquivobu);
            var bu = buServico.ProcessarArquivoBU(ebu);

            bu.UF = imgbu.UF;
            bu.NomeMunicipio = imgbu.NomeMunicipio;
            bu.NomeEleicao = imgbu.NomeEleicao;
            bu.TurnoEleicao = imgbu.TurnoEleicao;
            bu.ResumoDaCorrespondencia = imgbu.ResumoDaCorrespondencia;
            bu.CodigoVerificador = imgbu.CodigoVerificador;

            using (var context = new TSEContext())
            {
                var procServico = new ProcessarServico("", "", false);
                procServico.SalvarBoletimUrna(bu, context);
                context.SaveChanges();
            }

            Console.WriteLine("Processo finalizou com sucesso.");
            */
        }
    }




}
