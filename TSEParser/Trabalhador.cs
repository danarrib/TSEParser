using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Internal;

namespace TSEParser
{
    public class Trabalhador
    {
        public CrawlerModels.SecaoEleitoral secao { get; set; }
        public CrawlerModels.Municipio municipio { get; set; }
        public CrawlerModels.ZonaEleitoral zonaEleitoral { get; set; }
        public string UF { get; set; }
        public string diretorioZona { get; set; }
        public string urlTSE { get; set; }
        public string diretorioLocalDados { get; set; }
        public bool compararIMGBUeBU { get; set; }
        public StringBuilder mensagemLog { get; set; }

        public Trabalhador(CrawlerModels.SecaoEleitoral _secao, CrawlerModels.Municipio _municipio, CrawlerModels.ZonaEleitoral _zonaEleitoral, string _UF, string _diretorioZona, string _urlTSE, string _diretorioLocalDados, bool _compararIMGBUeBU)
        {
            secao = _secao;
            municipio = _municipio;
            zonaEleitoral = _zonaEleitoral;
            UF = _UF;
            diretorioZona = _diretorioZona;
            urlTSE = _urlTSE;
            diretorioLocalDados = _diretorioLocalDados;
            compararIMGBUeBU = _compararIMGBUeBU;
            mensagemLog = new StringBuilder();
        }

        public List<BoletimUrna> ProcessarSecao()
        {
            var ThreadNome = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Processando UF {UF} - Munic {municipio.cd} {municipio.nm} - Zona {zonaEleitoral.cd} - Seção {secao.ns} - Thread {ThreadNome}");

            List<BoletimUrna> lstBU = new List<BoletimUrna>();

            string diretorioSecao = diretorioZona + @"\" + secao.ns;
            if (!Directory.Exists(diretorioSecao))
                throw new Exception($"O diretório da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd} não foi localizado.");

            if (!File.Exists(diretorioSecao + @"\config.json"))
                throw new Exception($"O arquivo de configuração da Seção {secao.ns} zona {zonaEleitoral.cd} muncipio {municipio.cd} não foi localizado.");

            var jsonConfiguracaoSecao = File.ReadAllText(diretorioSecao + @"\config.json");

            CrawlerModels.BoletimUrna boletimUrna;
            try
            {
                boletimUrna = JsonSerializer.Deserialize<CrawlerModels.BoletimUrna>(jsonConfiguracaoSecao);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao interpretar o JSON de boletim de urna da seção " + secao.ns + ", zona " + zonaEleitoral.cd + ", município " + municipio.cd + ", UF " + UF, ex);
            }

            foreach (var objHash in boletimUrna.hashes)
            {
                if (objHash.st != "Totalizado" && objHash.st != "Recebido")
                {
                    string mensagem = $"UF {UF} MUN {municipio.cd} {municipio.nm} ZN {zonaEleitoral.cd} SE {secao.ns} - Hash Situação: {objHash.st}. Será ignorado.";
                    EscreverLog(mensagem);
                    Console.WriteLine(mensagem);
                    continue;
                }

                string diretorioHash = diretorioSecao + @"\" + objHash.hash;
                if (!Directory.Exists(diretorioHash))
                    throw new Exception($"O diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd} não foi localizado.");

                // Obter o arquivo imgbu e o bu
                var arquivo = objHash.nmarq.Find(x => x.Contains(".imgbu"));
                var arquivoBU = objHash.nmarq.Find(x => x.Contains(".bu"));

                if (!string.IsNullOrWhiteSpace(arquivo) && !string.IsNullOrWhiteSpace(arquivoBU))
                {
                    if (!File.Exists(diretorioHash + @"\" + arquivo))
                        throw new Exception($"O arquivo {arquivo} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                    using (var servico = new BoletimUrnaServico())
                    {
                        var bu = servico.ProcessarBoletimUrna(diretorioHash + @"\" + arquivo);
                        if (BoletimEstaCorrompido(bu))
                        {
                            // O arquivo deve estar corrompido. Tentar baixar novamente do TSE para processar
                            Console.WriteLine("O arquivo atual está corrompido. Tentando baixar novamente do TSE...");
                            string urlArquivoABaixar = urlTSE + @"dados/" + UF.ToLower() + @"/" + municipio.cd + @"/" + zonaEleitoral.cd + @"/" + secao.ns + @"/" + objHash.hash + @"/" + arquivo;
                            try
                            {
                                BaixarArquivo(urlArquivoABaixar, diretorioHash + @"\" + arquivo);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Erro ao baixar o arquivo " + arquivo + " da " + secao.ns + ", zona " + zonaEleitoral.cd + ", município " + municipio.cd + ", UF " + UF, ex);
                            }
                            bu = servico.ProcessarBoletimUrna(diretorioHash + @"\" + arquivo);

                            if (BoletimEstaCorrompido(bu))
                            {
                                // Mesmo depois de baixar novamente o arquivo, ele continua corrompido. Isso não deveria acontecer.
                                EscreverLog($"UF {UF} MUN {municipio.cd} ZN {zonaEleitoral.cd} SE {secao.ns} - Arquivo corrompido. Re-tentativa não funcionou. {urlArquivoABaixar} ");
                            }
                        }

                        if (compararIMGBUeBU)
                        {
                            if (!File.Exists(diretorioHash + @"\" + arquivoBU))
                                throw new Exception($"O arquivo {arquivoBU} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                            TSEBU.EntidadeBoletimUrna ebu = null;
                            try
                            {
                                ebu = servico.DecodificarArquivoBU(diretorioHash + @"\" + arquivoBU);
                            }
                            catch (Exception exbu)
                            {
                                // O arquivo BU está corrompido. Tentar baixar novamente antes de falhar.
                                string urlArquivoABaixar = urlTSE + @"dados/" + UF.ToLower() + @"/" + municipio.cd + @"/" + zonaEleitoral.cd + @"/" + secao.ns + @"/" + objHash.hash + @"/" + arquivoBU;
                                try
                                {
                                    BaixarArquivo(urlArquivoABaixar, diretorioHash + @"\" + arquivoBU);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Erro ao baixar o arquivo " + arquivoBU + " da " + secao.ns + ", zona " + zonaEleitoral.cd + ", município " + municipio.cd + ", UF " + UF, ex);
                                }

                                try
                                {
                                    ebu = servico.DecodificarArquivoBU(diretorioHash + @"\" + arquivoBU);
                                }
                                catch (Exception exbu2)
                                {
                                    EscreverLog($"UF {UF} MUN {municipio.cd} ZN {zonaEleitoral.cd} SE {secao.ns} - Arquivo BU está corrompido e não pode ser decodificado. {exbu2.Message}");
                                }
                            }

                            if (ebu != null)
                            {
                                BoletimUrna bu2 = null;
                                try
                                {
                                    bu2 = servico.ProcessarArquivoBU(ebu);
                                }
                                catch (Exception ex2)
                                {
                                    // Erro ao decodificar o arquivo BU. Talvez o arquivo esteja corrompido (difícilmente, mas vamos tentar novamente).
                                    string urlArquivoABaixar = urlTSE + @"dados/" + UF.ToLower() + @"/" + municipio.cd + @"/" + zonaEleitoral.cd + @"/" + secao.ns + @"/" + objHash.hash + @"/" + arquivoBU;
                                    try
                                    {
                                        BaixarArquivo(urlArquivoABaixar, diretorioHash + @"\" + arquivoBU);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro ao baixar o arquivo " + arquivoBU + " da " + secao.ns + ", zona " + zonaEleitoral.cd + ", município " + municipio.cd + ", UF " + UF, ex);
                                    }

                                    try
                                    {
                                        ebu = servico.DecodificarArquivoBU(diretorioHash + @"\" + arquivoBU);
                                    }
                                    catch (Exception exbu2)
                                    {
                                        EscreverLog($"UF {UF} MUN {municipio.cd} ZN {zonaEleitoral.cd} SE {secao.ns} - Arquivo BU está corrompido e não pode ser decodificado. {exbu2.Message}");
                                    }

                                    try
                                    {
                                        bu2 = servico.ProcessarArquivoBU(ebu);
                                    }
                                    catch (Exception ex3)
                                    {
                                        EscreverLog($"UF {UF} MUN {municipio.cd} ZN {zonaEleitoral.cd} SE {secao.ns} - Arquivo BU está corrompido e não pode ser decodificado. {ex3.Message}");
                                    }
                                }

                                if (bu2 != null)
                                {
                                    bu2.UF = UF;
                                    bu2.NomeMunicipio = municipio.nm;

                                    var inconsistencias = servico.CompararBoletins(bu, bu2);

                                    if (inconsistencias.Count > 0)
                                    {
                                        EscreverLog($"UF {UF} MUN {municipio.cd} ZN {zonaEleitoral.cd} SE {secao.ns} - Arquivos IMGBU (A) e BU (B) não são iguais.\n" + inconsistencias.Join("\n") + "\n");
                                    }
                                }
                            }
                        }

                        lstBU.Add(bu);
                    }
                }
                else
                {
                    Console.WriteLine($"Para esta seção, não há arquivo. UF {UF}, Município {municipio.cd}, Zona {zonaEleitoral.cd}, Seção {secao.ns}");
                }
            }

            return lstBU;
        }

        public bool BoletimEstaCorrompido(BoletimUrna bu)
        {
            if (string.IsNullOrWhiteSpace(bu.UF))
                return true;
            if (string.IsNullOrWhiteSpace(bu.CodigoMunicipio))
                return true;
            if (string.IsNullOrWhiteSpace(bu.SecaoEleitoral))
                return true;
            if (bu.AberturaUrnaEletronica == DateTime.MinValue)
                return true;
            if (bu.FechamentoUrnaEletronica == DateTime.MinValue)
                return true;
            if (bu.PR_Total == 0)
                return true;

            return false;
        }

        private void BaixarArquivo(string urlArquivo, string arquivoLocal)
        {
            int tentativas = 0;
            int maxTentativas = 5;
            using (var client = new TSEWebClient())
            {
                while (true)
                {
                    tentativas++;
                    try
                    {
                        client.DownloadFile(urlArquivo, arquivoLocal);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (!ex.Message.Contains("Slow Down") && !ex.Message.Contains("timed out"))
                        {
                            throw ex;
                        }

                        if (tentativas > maxTentativas)
                        {
                            throw ex;
                        }

                        // Esperar 1 minuto para tentar baixar novamente
                        Console.WriteLine("Erro ao baixar o arquivo. Esperando 1 minuto para tentar novamente...");
                        Thread.Sleep(60 * 1000);
                    }
                }
            }
        }

        public void EscreverLog(string mensagem)
        {
            mensagemLog.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + mensagem);
        }
    }

}
