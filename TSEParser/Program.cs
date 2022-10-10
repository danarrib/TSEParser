using System;
using System.Collections.Generic;

namespace TSEParser
{
    internal class Program
    {
        public const string diretorioLocalDados = @"D:\Downloads\Urnas\";
        public const string urlTSE = @"https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/";

        /// <summary>
        /// Se estiver true, vai comparar o arquivo IMGBU com o arquivo BU, e se houver diferença, vai escrever no log.
        /// </summary>
        public const bool compararIMGBUeBU = true;

        static void Main(string[] args)
        {
            /*
            List<string> UFs = new List<string>();
            UFs.AddRange(new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG",
                "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO", "ZZ" });

            foreach (var UF in UFs)
            {
                var servico = new ProcessarServico(diretorioLocalDados, urlTSE, compararIMGBUeBU);
                servico.ProcessarUF(UF);
            }
            */

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
        }
    }




}
