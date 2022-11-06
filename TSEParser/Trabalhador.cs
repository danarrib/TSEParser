using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Internal;
using System.IO.Compression;
using System.Linq;

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
        public bool compararRDV { get; set; }
        public bool processarLogDeUrna { get; set; }
        public StringBuilder mensagemLog { get; set; }
        public List<VotosLog> votosLog { get; set; }
        public List<VotosSecaoRDV> votosRDV { get; set; }
        public bool segundoTurno { get; set; }

        public Trabalhador(CrawlerModels.SecaoEleitoral _secao, CrawlerModels.Municipio _municipio, CrawlerModels.ZonaEleitoral _zonaEleitoral, 
            string _UF, string _diretorioZona, string _urlTSE, string _diretorioLocalDados, bool _compararIMGBUeBU, bool _compararRDV, bool _processarLogDeUrna, bool _segundoTurno)
        {
            secao = _secao;
            municipio = _municipio;
            zonaEleitoral = _zonaEleitoral;
            UF = _UF;
            diretorioZona = _diretorioZona;
            urlTSE = _urlTSE;
            diretorioLocalDados = _diretorioLocalDados;
            compararIMGBUeBU = _compararIMGBUeBU;
            compararRDV = _compararRDV;
            processarLogDeUrna = _processarLogDeUrna;
            mensagemLog = new StringBuilder();
            votosLog = new List<VotosLog>();
            segundoTurno = _segundoTurno;
        }

        public List<BoletimUrna> ProcessarSecao()
        {
            string descricaoSecao = $"UF {UF}, Município {municipio.cd} {municipio.nm}, Zona {zonaEleitoral.cd}, Seção {secao.ns}";
            var ThreadNome = Thread.CurrentThread.ManagedThreadId.ToString();

            Console.WriteLine($"Processando {descricaoSecao} - Thread {ThreadNome}");

            List<BoletimUrna> lstBU = new List<BoletimUrna>();

            string diretorioSecao = diretorioZona + @"\" + secao.ns;
            if (!Directory.Exists(diretorioSecao))
                throw new Exception($"O diretório da seção eleitoral {secao.ns} não foi localizado. {descricaoSecao}.");

            if (!File.Exists(diretorioSecao + @"\config.json"))
                throw new Exception($"O arquivo de configuração da Seção {secao.ns} não foi localizado. {descricaoSecao}.");

            var jsonConfiguracaoSecao = File.ReadAllText(diretorioSecao + @"\config.json");

            CrawlerModels.BoletimUrna boletimUrna;
            try
            {
                boletimUrna = JsonSerializer.Deserialize<CrawlerModels.BoletimUrna>(jsonConfiguracaoSecao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao interpretar o JSON de boletim de urna da seção {descricaoSecao}.", ex);
            }

            foreach (var objHash in boletimUrna.hashes)
            {
                if (objHash.st != "Totalizado" && objHash.st != "Recebido")
                {
                    string mensagem = $"{descricaoSecao} - Hash Situação: {objHash.st}. Será ignorado.";
                    EscreverLog(mensagem);
                    Console.WriteLine(mensagem);
                    continue;
                }

                string diretorioHash = diretorioSecao + @"\" + objHash.hash;
                if (!Directory.Exists(diretorioHash))
                    throw new Exception($"O diretório hash {objHash.hash} não foi localizado. {descricaoSecao}.");

                // Obter o arquivo imgbu e o bu
                var arquivoIMGBU = objHash.nmarq.Find(x => x.Contains(".imgbu"));
                var arquivoBU = objHash.nmarq.Find(x => x.Contains(".bu"));
                var arquivoRDV = objHash.nmarq.Find(x => x.Contains(".rdv"));
                var arquivoLog = objHash.nmarq.Find(x => x.Contains(".logjez"));

                if (!string.IsNullOrWhiteSpace(arquivoIMGBU) || !string.IsNullOrWhiteSpace(arquivoBU))
                {
                    if (string.IsNullOrWhiteSpace(arquivoIMGBU))
                    {
                        // Para esta Seção, não há arquivo IMGBU. Então carregar a partir do arquivo BU.
                        EscreverLog($"{descricaoSecao} - Não há arquivo IMGBU para esta seção. Será carregado o arquivo BU.");

                        var bu = LerArquivoBU(diretorioHash + @"\" + arquivoBU, descricaoSecao, objHash.hash);
                        if (bu != null)
                        {
                            lstBU.Add(bu);
                        }
                    }
                    else
                    {
                        if (!File.Exists(diretorioHash + @"\" + arquivoIMGBU))
                            throw new Exception($"O arquivo {arquivoIMGBU} não foi localizado. {descricaoSecao}.");

                        using (var servico = new BoletimUrnaServico())
                        {
                            var bu = servico.ProcessarBoletimUrna(diretorioHash + @"\" + arquivoIMGBU);
                            if (BoletimEstaCorrompido(bu))
                            {
                                // O arquivo deve estar corrompido. Tentar baixar novamente do TSE para processar
                                Console.WriteLine("O arquivo atual está corrompido. Tentando baixar novamente do TSE...");
                                string urlArquivoABaixar = urlTSE + @"dados/" + UF.ToLower() + @"/" + municipio.cd + @"/" + zonaEleitoral.cd + @"/" + secao.ns + @"/" + objHash.hash + @"/" + arquivoIMGBU;
                                try
                                {
                                    BaixarArquivo(urlArquivoABaixar, diretorioHash + @"\" + arquivoIMGBU);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Erro ao baixar o arquivo {arquivoIMGBU} da url {urlArquivoABaixar}: {ex.Message}. {descricaoSecao}.", ex);
                                }
                                bu = servico.ProcessarBoletimUrna(diretorioHash + @"\" + arquivoIMGBU);

                                if (BoletimEstaCorrompido(bu))
                                {
                                    // Mesmo depois de baixar novamente o arquivo, ele continua corrompido. Isso não deveria acontecer.
                                    EscreverLog($"{descricaoSecao} - Arquivo corrompido. Re-tentativa não funcionou. {urlArquivoABaixar} ");
                                }
                            }

                            if (compararIMGBUeBU)
                            {
                                var bu2 = LerArquivoBU(diretorioHash + @"\" + arquivoBU, descricaoSecao, objHash.hash);
                                if (bu2 != null)
                                {
                                    if (arquivoIMGBU.Contains(".imgbusa"))
                                    {
                                        // Arquivo do Sistema de Apuração não tem algumas informações. Obter a partir do BU.
                                        bu.LocalVotacao = bu2.LocalVotacao;
                                        bu.EleitoresAptos = bu2.EleitoresAptos;
                                        bu.EleitoresFaltosos = bu2.EleitoresFaltosos;
                                        bu.Comparecimento = bu2.Comparecimento;
                                        bu.AberturaUrnaEletronica = bu2.AberturaUrnaEletronica;
                                        bu.FechamentoUrnaEletronica = bu2.FechamentoUrnaEletronica;
                                    }

                                    var inconsistencias = servico.CompararBoletins(bu, bu2);

                                    if (inconsistencias.Count > 0)
                                    {
                                        EscreverLog($"{descricaoSecao} - Arquivos IMGBU (A) e BU (B) não são iguais.\n" + inconsistencias.Join("\n") + "\n");

                                        var diferencaVotos = inconsistencias.Where(x => x.ToLower().Contains("Quantidade de votos do candidato".ToLower()) && x.ToLower().Contains("é diferente".ToLower())).ToList();
                                        if (diferencaVotos.Any())
                                        {
                                            // Tem diferença de votos, então devemos usar o arquivo BU para carregar, e não o arquivo IMGBU
                                            bu = bu2;
                                            EscreverLog($"{descricaoSecao} - Houve diferença de votos, então o arquivo BU será usado em vez do IMGBU.");
                                        }
                                    }
                                }
                            }

                            if (compararRDV)
                            {
                                if (string.IsNullOrWhiteSpace(arquivoRDV))
                                {
                                    EscreverLog($"{descricaoSecao} - Não há registro de votos (RDV).");
                                }
                                else
                                {
                                    if (!File.Exists(diretorioHash + @"\" + arquivoRDV))
                                    {
                                        // O arquivo pode estar dentro do zip. Descompactar.
                                        if (!File.Exists(diretorioHash + @"\pacote.zip"))
                                            throw new Exception($"O arquivo {arquivoRDV} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                                        using (ZipArchive zip = ZipFile.Open(diretorioHash + @"\pacote.zip", ZipArchiveMode.Read))
                                        {
                                            var zipEntry = zip.GetEntry(arquivoRDV);
                                            if (zipEntry != null)
                                            {
                                                zipEntry.ExtractToFile(diretorioHash + @"\" + arquivoRDV);
                                            }
                                            else
                                            {
                                                throw new Exception($"O arquivo {arquivoRDV} não foi localizado no pacote zip, diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");
                                            }
                                        }
                                    }

                                    using (var rdvServico = new RegistroDeVotoServico())
                                    {
                                        TSERDV.EntidadeResultadoRDV rdv = null;
                                        try
                                        {
                                            rdv = rdvServico.DecodificarRegistroVoto(diretorioHash + @"\" + arquivoRDV);
                                        }
                                        catch (Exception ex)
                                        {
                                            EscreverLog($"{descricaoSecao} - O Registro de votos (arquivo RDV) está corrompido.");
                                        }

                                        if (rdv != null)
                                        {
                                            votosRDV = rdvServico.ObterVotos(rdv, municipio.cd.ToInt(), zonaEleitoral.cd.ToShort(), secao.ns.ToShort());

                                            if (votosRDV.Count == 0)
                                            {
                                                EscreverLog($"{descricaoSecao} - O Registro de votos está vazio.");
                                            }
                                            else
                                            {
                                                rdvServico.CompararBUeRDV(bu, votosRDV, out string mensagem);
                                                if (!string.IsNullOrWhiteSpace(mensagem))
                                                {
                                                    EscreverLog($"{descricaoSecao} - O Boletim de Urna não corresponde ao Registro de votos.\n{mensagem}\n");
                                                }
                                            }
                                        }
                                    }

                                    // Excluir o arquivo RDV
                                    if (File.Exists(diretorioHash + @"\pacote.zip"))
                                        File.Delete(diretorioHash + @"\" + arquivoRDV);
                                }
                            }

                            if (processarLogDeUrna)
                            {
                                if (string.IsNullOrWhiteSpace(arquivoLog))
                                {
                                    EscreverLog($"{descricaoSecao} - Não há Log de urna (LOGJEZ).");
                                }
                                else
                                {
                                    if (!File.Exists(diretorioHash + @"\" + arquivoLog))
                                    {
                                        // O arquivo pode estar dentro do zip. Descompactar.
                                        if (!File.Exists(diretorioHash + @"\pacote.zip"))
                                            throw new Exception($"O arquivo {arquivoLog} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                                        using (ZipArchive zip = ZipFile.Open(diretorioHash + @"\pacote.zip", ZipArchiveMode.Read))
                                        {
                                            var zipEntry = zip.GetEntry(arquivoLog);
                                            if (zipEntry != null)
                                            {
                                                zipEntry.ExtractToFile(diretorioHash + @"\" + arquivoLog);
                                            }
                                            else
                                            {
                                                throw new Exception($"O arquivo {arquivoLog} não foi localizado no pacote zip, diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");
                                            }
                                        }
                                    }

                                    var logServico = new LogDeUrnaServico();
                                    votosLog = logServico.ProcessarLogUrna(
                                        diretorioHash + @"\" + arquivoLog,
                                        UF,
                                        municipio.cd,
                                        municipio.nm,
                                        zonaEleitoral.cd,
                                        secao.ns,
                                        diretorioHash,
                                        out DateTime dhZeresima,
                                        out string mensagensLog,
                                        out short modeloUrna,
                                        segundoTurno
                                        );
                                    if (!string.IsNullOrWhiteSpace(mensagensLog))
                                    {
                                        EscreverLog($"{descricaoSecao} - O processamento do Log da Urna gerou as seguintes mensagens:\n{mensagensLog}");
                                    }

                                    bu.Zeresima = dhZeresima;
                                    bu.ModeloUrnaEletronica = modeloUrna;
                                    var mensagens = logServico.CompararLogUrnaComBU(bu, votosLog);

                                    if (!string.IsNullOrWhiteSpace(mensagens))
                                    {
                                        EscreverLog($"{descricaoSecao} - O Boletim de Urna não corresponde ao Log da Urna:\n{mensagens}\n");
                                        bu.LogUrnaInconsistente = true;
                                    }

                                    // Excluir o arquivo LOGJEZ
                                    if (File.Exists(diretorioHash + @"\pacote.zip"))
                                        File.Delete(diretorioHash + @"\" + arquivoLog);
                                }
                            }

                            lstBU.Add(bu);
                        }
                    }
                }
                else
                {
                    EscreverLog($"{descricaoSecao} - Para esta seção, não há arquivo IMGBU ou BU.");
                }
            }

            return lstBU;
        }

        public BoletimUrna LerArquivoBU(string arquivoBU, string descricaoSecao, string hash)
        {
            using (var servico = new BoletimUrnaServico())
            {
                if (!File.Exists(arquivoBU))
                    throw new Exception($"O arquivo {arquivoBU} não foi localizado. {descricaoSecao}");

                TSEBU.EntidadeBoletimUrna ebu = null;
                try
                {
                    ebu = servico.DecodificarArquivoBU(arquivoBU);
                }
                catch (Exception exbu)
                {
                    // O arquivo BU está corrompido. Tentar baixar novamente antes de falhar.
                    string urlArquivoABaixar = $"{urlTSE}dados/{UF.ToLower()}/{municipio.cd}/{zonaEleitoral.cd}/{secao.ns}/{hash}/{arquivoBU}";
                    try
                    {
                        BaixarArquivo(urlArquivoABaixar, arquivoBU);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao baixar o arquivo {arquivoBU} da url {urlArquivoABaixar}: {ex.Message}. {descricaoSecao}.", ex);
                    }

                    try
                    {
                        ebu = servico.DecodificarArquivoBU(arquivoBU);
                    }
                    catch (Exception exbu2)
                    {
                        EscreverLog($"{descricaoSecao} - Arquivo BU está corrompido e não pode ser decodificado. {exbu2.Message}");
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
                        string urlArquivoABaixar = $"{urlTSE}dados/{UF.ToLower()}/{municipio.cd}/{zonaEleitoral.cd}/{secao.ns}/{hash}/{arquivoBU}";
                        try
                        {
                            BaixarArquivo(urlArquivoABaixar, arquivoBU);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Erro ao baixar o arquivo {arquivoBU} da url {urlArquivoABaixar}: {ex.Message}. {descricaoSecao}.", ex);
                        }

                        try
                        {
                            ebu = servico.DecodificarArquivoBU(arquivoBU);
                        }
                        catch (Exception exbu2)
                        {
                            EscreverLog($"{descricaoSecao} - Arquivo BU está corrompido e não pode ser decodificado. {exbu2.Message}");
                        }

                        try
                        {
                            bu2 = servico.ProcessarArquivoBU(ebu);
                        }
                        catch (Exception ex3)
                        {
                            EscreverLog($"{descricaoSecao} - Arquivo BU está corrompido e não pode ser decodificado. {ex3.Message}");
                        }
                    }

                    if (bu2 != null)
                    {
                        bu2.UF = UF;
                        bu2.NomeMunicipio = municipio.nm;
                        return bu2;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

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
