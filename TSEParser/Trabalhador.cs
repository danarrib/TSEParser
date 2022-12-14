using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Internal;
using System.IO.Compression;
using System.Linq;
using System.Diagnostics.Metrics;

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
        public bool excluirArquivosDescompactados { get; set; }
        public DefeitosSecao DefeitosSecao { get; set; }

        public Trabalhador(CrawlerModels.SecaoEleitoral _secao, CrawlerModels.Municipio _municipio, CrawlerModels.ZonaEleitoral _zonaEleitoral,
            string _UF, string _diretorioZona, string _urlTSE, string _diretorioLocalDados, bool _compararIMGBUeBU, bool _compararRDV, 
            bool _processarLogDeUrna, bool _segundoTurno, bool _excluirArquivosDescompactados)
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
            DefeitosSecao = null;
            excluirArquivosDescompactados = _excluirArquivosDescompactados;
        }

        public List<BoletimUrna> ProcessarSecao()
        {
            string descricaoSecao = $"UF {UF}, Município {municipio.cd} {municipio.nm}, Zona {zonaEleitoral.cd}, Seção {secao.ns}";
            //var ThreadNome = Thread.CurrentThread.ManagedThreadId.ToString();
            //Console.WriteLine($"Processando {descricaoSecao} - Thread {ThreadNome}");
            //Console.Write(".");

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

                    IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);

                    if (objHash.st == "Sem arquivo")
                        DefeitosSecao.SemArquivo = true;
                    else if (objHash.st == "Rejeitado")
                        DefeitosSecao.Rejeitado = true;
                    else if (objHash.st == "Excluído")
                        DefeitosSecao.Excluido = true;

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
                var arquivoLogSA = objHash.nmarq.Find(x => x.Contains(".logsajez"));

                if (!string.IsNullOrWhiteSpace(arquivoIMGBU) || !string.IsNullOrWhiteSpace(arquivoBU))
                {
                    if (string.IsNullOrWhiteSpace(arquivoIMGBU))
                    {
                        // Para esta Seção, não há arquivo IMGBU. Então carregar a partir do arquivo BU.
                        EscreverLog($"{descricaoSecao} - Não há arquivo IMGBU para esta seção. Será carregado o arquivo BU.");

                        IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                        DefeitosSecao.ArquivoIMGBUFaltando = true;

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
                                EscreverLog($"{descricaoSecao} - Arquivo IMGBU corrompido.");
                                IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                DefeitosSecao.ArquivoIMGBUCorrompido = true;
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

                                    var inconsistencias = servico.CompararBoletins(bu, bu2, out int CodigoIdentificadorUEBU);

                                    if(CodigoIdentificadorUEBU != 0)
                                    {
                                        IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                        DefeitosSecao.CodigoIdentificacaoUrnaEletronicaBU = CodigoIdentificadorUEBU;
                                    }

                                    if (inconsistencias.Count > 0)
                                    {
                                        EscreverLog($"{descricaoSecao} - Arquivos IMGBU (A) e BU (B) não são iguais.\n" + String.Join("\n", inconsistencias) + "\n");

                                        IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                        DefeitosSecao.ArquivoBUeIMGBUDiferentes = true;

                                        var diferencaVotos = inconsistencias.Where(x => x.ToLower().Contains("Quantidade de votos do candidato".ToLower()) && x.ToLower().Contains("é diferente".ToLower())).ToList();
                                        if (diferencaVotos.Any())
                                        {
                                            // Tem diferença de votos, então devemos usar o arquivo BU para carregar, e não o arquivo IMGBU
                                            bu = bu2;
                                            EscreverLog($"{descricaoSecao} - Houve diferença de votos, então o arquivo BU será usado em vez do IMGBU.");
                                            IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                            DefeitosSecao.DiferencaVotosBUeIMGBU = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!File.Exists(diretorioHash + @"\" + arquivoBU))
                                    {
                                        EscreverLog($"{descricaoSecao} - O arquivo BU não foi encontrado.");
                                        IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                        DefeitosSecao.ArquivoBUFaltando = true;
                                    }
                                    else
                                    {
                                        EscreverLog($"{descricaoSecao} - Não foi possível ler o arquivo BU. Provavelmente ele está corrompido.");
                                        IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                        DefeitosSecao.ArquivoBUCorrompido = true;
                                    }
                                }
                            }

                            if (compararRDV)
                            {
                                if (string.IsNullOrWhiteSpace(arquivoRDV))
                                {
                                    EscreverLog($"{descricaoSecao} - Não há registro de votos (RDV).");
                                    IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                    DefeitosSecao.ArquivoRDVFaltando = true;
                                }
                                else
                                {
                                    if (!File.Exists(diretorioHash + @"\" + arquivoRDV))
                                    {
                                        // O arquivo está dentro do zip. Descompactar.
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
                                            IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                            DefeitosSecao.ArquivoRDVCorrompido = true;
                                        }

                                        if (rdv != null)
                                        {
                                            try
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
                                            catch (Exception ex)
                                            {
                                                EscreverLog($"{descricaoSecao} - O Registro de votos está corrompido.");
                                                IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                                DefeitosSecao.ArquivoRDVCorrompido = true;
                                            }
                                        }
                                    }

                                    // Excluir o arquivo RDV
                                    if (excluirArquivosDescompactados && File.Exists(diretorioHash + @"\pacote.zip"))
                                        File.Delete(diretorioHash + @"\" + arquivoRDV);
                                }
                            }

                            if (processarLogDeUrna)
                            {
                                if (string.IsNullOrWhiteSpace(arquivoLog) && string.IsNullOrWhiteSpace(arquivoLogSA))
                                {
                                    EscreverLog($"{descricaoSecao} - Não há Log de urna (LOGJEZ ou LOGSAJEZ).");
                                    IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                                    DefeitosSecao.ArquivoLOGJEZFaltando = true;
                                }

                                if (!string.IsNullOrWhiteSpace(arquivoLog))
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

                                    if (!string.IsNullOrWhiteSpace(arquivoLogSA) && !File.Exists(diretorioHash + @"\" + arquivoLogSA))
                                    {
                                        if (!File.Exists(diretorioHash + @"\" + arquivoLogSA))
                                        {
                                            // O arquivo pode estar dentro do zip. Descompactar.
                                            if (!File.Exists(diretorioHash + @"\pacote.zip"))
                                                throw new Exception($"O arquivo {arquivoLogSA} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                                            using (ZipArchive zip = ZipFile.Open(diretorioHash + @"\pacote.zip", ZipArchiveMode.Read))
                                            {
                                                var zipEntry = zip.GetEntry(arquivoLogSA);
                                                if (zipEntry != null)
                                                {
                                                    zipEntry.ExtractToFile(diretorioHash + @"\" + arquivoLogSA);
                                                }
                                                else
                                                {
                                                    throw new Exception($"O arquivo {arquivoLogSA} não foi localizado no pacote zip, diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");
                                                }
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
                                        segundoTurno,
                                        string.IsNullOrWhiteSpace(arquivoLogSA) ? string.Empty : diretorioHash + @"\" + arquivoLogSA,
                                        out int codIdenUELog,
                                        out short qtdJaVotou,
                                        out short qtdJustificativas,
                                        out DateTime dhAberturaUrna,
                                        out DateTime dhFechamentoUrna
                                        );

                                    bu.Zeresima = dhZeresima;
                                    bu.ModeloUrnaEletronica = modeloUrna;
                                    bu.CodigoIdentificacaoUrnaEletronicaLog = codIdenUELog;
                                    bu.QtdJaVotouLog = qtdJaVotou;
                                    bu.QtdJustificativasLog = qtdJustificativas;
                                    bu.AberturaUELog = dhAberturaUrna;
                                    bu.FechamentoUELog = dhFechamentoUrna;
                                    var compararVotos = string.IsNullOrWhiteSpace(arquivoLogSA); // Se o Log foi gerado pelo sistema de apuração, então não vai ter informação de votos consistente.
                                    var mensagens = logServico.CompararLogUrnaComBU(bu, votosLog, compararVotos);

                                    if (!string.IsNullOrWhiteSpace(mensagensLog) || !string.IsNullOrWhiteSpace(mensagens))
                                    {
                                        var textoALogar = string.Empty;
                                        if (!string.IsNullOrWhiteSpace(mensagensLog))
                                            textoALogar += mensagensLog;

                                        if (!string.IsNullOrWhiteSpace(mensagens))
                                            textoALogar += mensagens;

                                        if (!string.IsNullOrWhiteSpace(arquivoLogSA))
                                            textoALogar += "Esta seção usou o Sistema de Apuração, então os votos logados não puderam ser comparados com o Boletim de Urna.\n";

                                        EscreverLog($"{descricaoSecao} - O processamento do Log da Urna gerou as seguintes mensagens:\n{textoALogar}");

                                        if (textoALogar.Contains("Quantidade de votos para") && textoALogar.Contains("é diferente no BU"))
                                            bu.LogUrnaInconsistente = true;
                                    }

                                    // Excluir o arquivo LOGJEZ
                                    if (excluirArquivosDescompactados && File.Exists(diretorioHash + @"\pacote.zip"))
                                        File.Delete(diretorioHash + @"\" + arquivoLog);

                                    if (excluirArquivosDescompactados && File.Exists(diretorioHash + @"\pacote.zip") && !string.IsNullOrWhiteSpace(arquivoLogSA))
                                        File.Delete(diretorioHash + @"\" + arquivoLogSA);
                                }

                                if (string.IsNullOrWhiteSpace(arquivoLog) && !string.IsNullOrWhiteSpace(arquivoLogSA))
                                {
                                    // Tem apenas o Log do Sistema de apuração
                                    if (!File.Exists(diretorioHash + @"\" + arquivoLogSA))
                                    {
                                        // O arquivo pode estar dentro do zip. Descompactar.
                                        if (!File.Exists(diretorioHash + @"\pacote.zip"))
                                            throw new Exception($"O arquivo {arquivoLogSA} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                                        using (ZipArchive zip = ZipFile.Open(diretorioHash + @"\pacote.zip", ZipArchiveMode.Read))
                                        {
                                            var zipEntry = zip.GetEntry(arquivoLogSA);
                                            if (zipEntry != null)
                                            {
                                                zipEntry.ExtractToFile(diretorioHash + @"\" + arquivoLogSA);
                                            }
                                            else
                                            {
                                                throw new Exception($"O arquivo {arquivoLogSA} não foi localizado no pacote zip, diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");
                                            }
                                        }
                                    }

                                    var logServico = new LogDeUrnaServico();
                                    votosLog = logServico.ProcessarLogUrna(
                                        diretorioHash + @"\" + arquivoLogSA,
                                        UF,
                                        municipio.cd,
                                        municipio.nm,
                                        zonaEleitoral.cd,
                                        secao.ns,
                                        diretorioHash,
                                        out DateTime dhZeresima,
                                        out string mensagensLog,
                                        out short modeloUrna,
                                        segundoTurno,
                                        string.Empty,
                                        out int codIdenUELog,
                                        out short qtdJaVotou,
                                        out short qtdJustificativas,
                                        out DateTime dhAberturaUrna,
                                        out DateTime dhFechamentoUrna
                                        );

                                    bu.Zeresima = dhZeresima;
                                    bu.ModeloUrnaEletronica = modeloUrna;
                                    bu.CodigoIdentificacaoUrnaEletronicaLog = codIdenUELog;
                                    bu.QtdJaVotouLog = qtdJaVotou;
                                    bu.QtdJustificativasLog = qtdJustificativas;
                                    bu.AberturaUELog = dhAberturaUrna;
                                    bu.FechamentoUELog = dhFechamentoUrna;
                                    var mensagens = logServico.CompararLogUrnaComBU(bu, votosLog, false);

                                    if (!string.IsNullOrWhiteSpace(mensagensLog) || !string.IsNullOrWhiteSpace(mensagens))
                                    {
                                        var textoALogar = string.Empty;
                                        if (!string.IsNullOrWhiteSpace(mensagensLog))
                                            textoALogar += mensagensLog;

                                        if (!string.IsNullOrWhiteSpace(mensagens))
                                            textoALogar += mensagens;

                                        if (!string.IsNullOrWhiteSpace(arquivoLogSA))
                                            textoALogar += "Esta seção usou o Sistema de Apuração, então os votos logados não puderam ser comparados com o Boletim de Urna.\n";

                                        EscreverLog($"{descricaoSecao} - O processamento do Log da Urna (SA) gerou as seguintes mensagens:\n{textoALogar}");

                                        if (textoALogar.Contains("Quantidade de votos para") && textoALogar.Contains("é diferente no BU"))
                                            bu.LogUrnaInconsistente = true;
                                    }

                                    // Excluir o arquivo LOGJEZ
                                    if (excluirArquivosDescompactados && File.Exists(diretorioHash + @"\pacote.zip"))
                                        File.Delete(diretorioHash + @"\" + arquivoLogSA);
                                }

                            }

                            if (arquivoIMGBU.ToLower().Contains(".imgbusa"))
                            {
                                bu.ResultadoSistemaApuracao = true;
                            }

                            lstBU.Add(bu);
                        }
                    }
                }
                else
                {
                    EscreverLog($"{descricaoSecao} - Para esta seção, não há arquivo IMGBU ou BU.");
                    IniciarDefeitosSecao(municipio.cd, zonaEleitoral.cd, secao.ns);
                    DefeitosSecao.ArquivoBUFaltando = true;
                    DefeitosSecao.ArquivoIMGBUFaltando = true;
                }
            }

            return lstBU;
        }

        public void IniciarDefeitosSecao(string codMunicipio, string codZona, string codSecao)
        {
            if (DefeitosSecao == null)
                DefeitosSecao = new DefeitosSecao()
                {
                    MunicipioCodigo = codMunicipio.ToInt(),
                    CodigoZonaEleitoral = codZona.ToShort(),
                    CodigoSecao = codSecao.ToShort(),
                };
        }

        public BoletimUrna LerArquivoBU(string arquivoBU, string descricaoSecao, string hash)
        {
            using (var servico = new BoletimUrnaServico())
            {
                if (!File.Exists(arquivoBU))
                {
                    EscreverLog($"{descricaoSecao} - O arquivo BU não existe.");
                    return null;
                }

                TSEBU.EntidadeBoletimUrna ebu = null;
                try
                {
                    ebu = servico.DecodificarArquivoBU(arquivoBU);
                }
                catch (Exception exbu)
                {
                    EscreverLog($"{descricaoSecao} - Arquivo BU está corrompido e não pode ser decodificado. {exbu.Message}");
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
                        EscreverLog($"{descricaoSecao} - Arquivo BU está corrompido e não pode ser decodificado. {ex2.Message}");
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

        public void EscreverLog(string mensagem)
        {
            mensagemLog.AppendLine(DateTime.Now.DataHoraPTBR() + " - " + mensagem);
        }
    }

}
