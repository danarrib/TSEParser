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
            List<string> UFs = new List<string>();
            UFs.AddRange(new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG",
                "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" });

            UFs.Clear();
            UFs.AddRange(new[] { "ZZ" });
            
            foreach (var UF in UFs)
            {
                var servico = new ProcessarServico(diretorioLocalDados, urlTSE, compararIMGBUeBU);
                servico.ProcessarUF(UF);
            }

            Console.WriteLine("Processo finalizou com sucesso.");
        }
    }

    


}
