using Lexical.FileProvider.PackageLoader;
using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using TSEBU;
using TSEParser.CrawlerModels;

namespace TSEParser
{
    public class LogDeUrnaServico
    {
        public List<VotosLog> ProcessarLogUrna(string arquivoLog, string UF, string codMunicipio, string nomeMunicipio, string codZona, string codSecao, string diretorioHash,
            out DateTime dhZeresima, out string mensagens, out short modeloUrna, bool segundoTurno, string arquivoLogSA, out int codigoIdentificacaoUrnaEletronica,
            out short qtdJaVotou, out short qtdJustificativas, out DateTime dhAberturaUrna, out DateTime dhFechamentoUrna)
        {
            string descricaoSecao = $"UF {UF}, Município {codMunicipio} {nomeMunicipio}, Zona {codZona}, Seção {codSecao}";
            List<VotosLog> retorno = new List<VotosLog>();
            List<LogDeUrna> arquivosLog = new List<LogDeUrna>();
            mensagens = string.Empty;
            dhAberturaUrna = DateTime.MinValue;
            dhFechamentoUrna = DateTime.MinValue;
            qtdJaVotou = 0;
            qtdJustificativas = 0;
            dhZeresima = DateTime.MinValue;
            modeloUrna = 0;
            codigoIdentificacaoUrnaEletronica = 0;

            // O arquivo .logjez é um arquivo compactado. Precisa descompactar para pegar o texto.
            try
            {
                using (var zip = SevenZipArchive.Open(arquivoLog))
                {
                    foreach (var arquivo in zip.Entries)
                    {
                        arquivo.WriteToFile(diretorioHash + @"\" + arquivo.Key);
                        if (arquivo.Key.ToLower().Contains(".jez"))
                        {
                            // É um arquivo compactado, descompactar também
                            using (var zip2 = SevenZipArchive.Open(diretorioHash + @"\" + arquivo.Key))
                            {
                                if (zip2.Entries.Count == 0)
                                {
                                    // O arquivo .jez está vazio. Ignorar.
                                    mensagens += $"O arquivo .logjez possui um arquivo .jez vazio.\n";
                                    continue;
                                }

                                if (zip2.Entries.Count > 1)
                                {
                                    mensagens += $"O arquivo .logjez possui um arquivo .jez que contém mais do que um arquivo.\n";
                                    continue;
                                }

                                // Obter o logd.dat
                                var zip2Entry = zip2.Entries.First();

                                if (zip2Entry.Key.ToLower() != "logd.dat")
                                {
                                    mensagens += $"O arquivo .logjez possui um arquivo .jez que contém um arquivo diferente de logd.dat.\n";
                                    continue;
                                }

                                zip2Entry.WriteToFile(diretorioHash + @"\" + zip2Entry.Key);

                                var novoLog = new LogDeUrna(zip2Entry.Key, arquivo.Key, zip2Entry.LastModifiedTime.Value);
                                novoLog.TextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + zip2Entry.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
                                arquivosLog.Add(novoLog);

                                File.Delete(diretorioHash + @"\" + zip2Entry.Key);
                            }
                        }
                        else if (arquivo.Key.ToLower() == "logd.dat")
                        {
                            arquivo.WriteToFile(diretorioHash + @"\" + arquivo.Key);

                            var novoLog = new LogDeUrna(arquivo.Key, string.Empty, arquivo.LastModifiedTime.Value);
                            novoLog.TextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + arquivo.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
                            arquivosLog.Add(novoLog);
                        }
                        else
                        {
                            Debugger.Break(); // Olhar que arquivo exótico é este, que não tem nem JEZ nem logd.dat
                        }
                        File.Delete(diretorioHash + @"\" + arquivo.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                mensagens += $"O arquivo .logjez está corrompido: {ex.Message}\n";
                return retorno;
            }

            // O arquivo .logsajez é um arquivo compactado. Precisa descompactar para pegar o texto.
            if (!string.IsNullOrWhiteSpace(arquivoLogSA))
            {
                try
                {
                    using (var zip = SevenZipArchive.Open(arquivoLogSA))
                    {
                        foreach (var arquivo in zip.Entries)
                        {
                            arquivo.WriteToFile(diretorioHash + @"\" + arquivo.Key);
                            if (arquivo.Key.ToLower().Contains(".jez"))
                            {
                                // É um arquivo compactado, descompactar também
                                using (var zip2 = SevenZipArchive.Open(diretorioHash + @"\" + arquivo.Key))
                                {
                                    if (zip2.Entries.Count == 0)
                                    {
                                        // O arquivo .jez está vazio. Ignorar.
                                        mensagens += $"O arquivo .logsajez possui um arquivo .jez vazio.\n";
                                        continue;
                                    }

                                    if (zip2.Entries.Count > 1)
                                    {
                                        mensagens += $"O arquivo .logsajez possui um arquivo .jez que contém mais do que um arquivo.\n";
                                        continue;
                                    }

                                    // Obter o logd.dat
                                    var zip2Entry = zip2.Entries.First();

                                    if (zip2Entry.Key.ToLower() != "logd.dat")
                                    {
                                        mensagens += $"O arquivo .logsajez possui um arquivo .jez que contém um arquivo diferente de logd.dat.\n";
                                        continue;
                                    }

                                    zip2Entry.WriteToFile(diretorioHash + @"\" + zip2Entry.Key);

                                    var novoLog = new LogDeUrna(zip2Entry.Key, arquivo.Key, zip2Entry.LastModifiedTime.Value);
                                    novoLog.TextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + zip2Entry.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
                                    arquivosLog.Add(novoLog);

                                    File.Delete(diretorioHash + @"\" + zip2Entry.Key);
                                }
                            }
                            else if (arquivo.Key.ToLower() == "logd.dat")
                            {
                                arquivo.WriteToFile(diretorioHash + @"\" + arquivo.Key);

                                var novoLog = new LogDeUrna(arquivo.Key, string.Empty, arquivo.LastModifiedTime.Value);
                                novoLog.TextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + arquivo.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
                                arquivosLog.Add(novoLog);
                            }
                            else
                            {
                                Debugger.Break(); // Olhar que arquivo exótico é este, que não tem nem JEZ nem logd.dat
                            }
                            File.Delete(diretorioHash + @"\" + arquivo.Key);
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensagens += $"O arquivo .logsajez está corrompido: {ex.Message}\n";
                    return retorno;
                }
            }

            // Agora temos todos os logs em memória. Remover os arquivos duplicados.
            if (arquivosLog.Count > 1)
            {
                for (int i = 0; i < arquivosLog.Count; i++)
                {
                    // Comparar este arquivo com os próximos, e determinar se algumas das linhas dele existem no outro.
                    // Se positivo, selecionar o maior arquivo
                    for (int a = i + 1; a < arquivosLog.Count; a++)
                    {
                        var arquivoA = arquivosLog[i];
                        var arquivoB = arquivosLog[a];
                        var percentualDiferenca = CompararLogs(arquivoA.TextoLog, arquivoB.TextoLog);

                        if (percentualDiferenca > 50)
                        {
                            // Estes arquivos são muito parecidos. Manter o maior e limpar o menor
                            if (arquivoA.TextoLog.Count() > arquivoB.TextoLog.Count())
                                arquivoB.TextoLog.Clear();
                            else
                                arquivoA.TextoLog.Clear();
                        }
                    }
                }
            }

            // O numerador de voto precisa ser declarado fora, pois ele incrementa mesmo em logs diferentes
            short numeroVoto = 0;

            // Para cada arquivo do Log, processar
            foreach (var item in arquivosLog)
            {
                codigoIdentificacaoUrnaEletronica = 0;

                int linhaAtual = 0;

                if (segundoTurno)
                {
                    // Precisa percorrer o log inteiro até encontrar onde começa o segundo turno.
                    foreach (var linha in item.TextoLog)
                    {
                        if (linha.ToLower().Contains("Iniciando aplicação - Oficial - 2º turno".ToLower()))
                        {
                            var data = ObterDataLinha(linha);
                            if (data > new DateTime(2022, 10, 20)) // Se o relógio da urna tiver sido perdido, a urna vai iniciar em uma data antiga. Então temos que ignorar e continuar procurando.
                            {
                                // TODO: Depois trocar essa data fixa por um valor que permita que a ferramenta possa ser usada em outros pleitos.
                                item.InicioSegundoTurno = ObterDataLinha(linha);
                                break;
                            }
                        }
                    }

                    // Se este arquivo não tem dados do segundo turno, pular ele.
                    if (item.InicioSegundoTurno == DateTime.MinValue)
                        continue;
                }

                // Começar a processar os logs
                var urnaEncerrada = false;
                var estaVotando = false;
                var urnaTestada = false;

                var dhInicioVoto = DateTime.MinValue;
                var dhHabilitacaoUrna = DateTime.MinValue;
                var dhFimVoto = DateTime.MinValue;
                bool votoPossuiBiometria = false;
                DedoBiometria votoDedoBiometria = DedoBiometria.Indefinido;
                short votoScoreBiometria = 0;
                byte votoQtdTeclasIndevidas = 0;
                bool votoHabilitacaoCancelada = false;
                bool votoDF = false;
                bool votoDE = false;
                bool votoSE = false;
                bool votoGO = false;
                bool votoPR = false;
                bool votoNuloSuspensaoDF = false;
                bool votoNuloSuspensaoDE = false;
                bool votoNuloSuspensaoSE = false;
                bool votoNuloSuspensaoGO = false;
                bool votoNuloSuspensaoPR = false;
                bool votoComputado = false;
                bool votoEleitorSuspenso = false;
                int votoLinha = 0;
                int votoLinhaFim = 0;
                var dataLinha = DateTime.MinValue;
                int codigoMunicipioLog = 0;
                short codigoZonaEleitoralLog = 0;
                short codigoSecaoEleitoralLog = 0;
                bool jaVotou = false;

                foreach (var linha in item.TextoLog)
                {
                    linhaAtual++;

                    if (linha.IndexOf("\t") > 0)
                    {
                        var arrLinha = linha.Split("\t");

                        if (arrLinha.Length < 4)
                            continue;

                        if (arrLinha[0].Length < 19)
                            continue;

                        // Data da linha
                        try
                        {
                            dataLinha = ObterDataLinha(arrLinha[0]);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }

                        // Se for segundo turno, somente interessam as linhas que começam após a data do segundo turno. Ignorar as linhas anteriores
                        if (segundoTurno && dataLinha < item.InicioSegundoTurno)
                            continue;

                        // Código identificador da UE
                        var idue = arrLinha[2];
                        if (!int.TryParse(idue, out int codIdenUE))
                            mensagens += $"Erro ao obter o código identificador da urna eletrônica na linha {linhaAtual} no jez \"{item.NomeArquivoJez}\" log \"{item.NomeArquivoLog}\".\n";

                        if (codigoIdentificacaoUrnaEletronica != 0 && codigoIdentificacaoUrnaEletronica != codIdenUE)
                        {
                            mensagens += $"O código identificador da urna eletrônica mudou na linha {linhaAtual} no jez \"{item.NomeArquivoJez}\" log \"{item.NomeArquivoLog}\". Antes era {codigoIdentificacaoUrnaEletronica} e agora é {codIdenUE}.\n";
                            codigoIdentificacaoUrnaEletronica = codIdenUE;
                        }
                        else if (codigoIdentificacaoUrnaEletronica == 0)
                            codigoIdentificacaoUrnaEletronica = codIdenUE;
                    }

                    if (linha.ToLower().Contains("Urna pronta para receber votos".ToLower()))
                    {
                        if (dhAberturaUrna != DateTime.MinValue)
                        {
                            if (dhAberturaUrna > dataLinha)
                                dhAberturaUrna = dataLinha;
                        }
                        else
                        {
                            dhAberturaUrna = dataLinha;
                        }
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Identifica que a Urna está testada".ToLower()))
                    {
                        urnaTestada = true;
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Urna não testada".ToLower()))
                    {
                        urnaTestada = false;
                    }
                    else if (!estaVotando &&
                        (linha.ToLower().Contains("Imprimindo relatório [ZERÉSIMA]".ToLower())
                        || linha.ToLower().Contains("Imprimindo relatório [ZERÉSIMA DE SEÇÃO]".ToLower()))
                        )
                    {
                        var tmpZeresima = dataLinha;
                        if (tmpZeresima > dhZeresima)
                            dhZeresima = tmpZeresima;
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Identificação do Modelo de Urna".ToLower()))
                    {
                        var chave = "Identificação do Modelo de Urna: UE";
                        var tmp = linha.Substring(linha.IndexOf(chave) + chave.Length);
                        tmp = tmp.Substring(0, tmp.IndexOf("\t"));
                        modeloUrna = tmp.ToShort();
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Município: ".ToLower()))
                    {
                        var chave = "Município: ";
                        var tmp = linha.Substring(linha.IndexOf(chave) + chave.Length);
                        tmp = tmp.Substring(0, tmp.IndexOf("\t"));
                        var tmpCodigo = tmp.ToInt();

                        if (tmpCodigo != 0)
                            codigoMunicipioLog = tmpCodigo;
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Zona Eleitoral: ".ToLower()))
                    {
                        var chave = "Zona Eleitoral: ";
                        var tmp = linha.Substring(linha.IndexOf(chave) + chave.Length);
                        tmp = tmp.Substring(0, tmp.IndexOf("\t"));
                        var tmpCodigo = tmp.ToShort();
                        if (tmpCodigo != 0)
                            codigoZonaEleitoralLog = tmpCodigo;
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Seção Eleitoral: ".ToLower()))
                    {
                        var chave = "Seção Eleitoral: ";
                        var tmp = linha.Substring(linha.IndexOf(chave) + chave.Length);
                        tmp = tmp.Substring(0, tmp.IndexOf("\t"));
                        var tmpCodigo = tmp.ToShort();
                        if (tmpCodigo != 0)
                            codigoSecaoEleitoralLog = tmpCodigo;
                    }
                    else if (!estaVotando && linha.ToLower().Contains("Título digitado pelo mesário".ToLower()))
                    {
                        estaVotando = true;
                        numeroVoto++;
                        dhInicioVoto = dataLinha;
                        votoLinha = linhaAtual;

                        // Zerando variáveis de voto
                        dhHabilitacaoUrna = DateTime.MinValue;
                        dhFimVoto = DateTime.MinValue;
                        votoPossuiBiometria = false;
                        votoDedoBiometria = DedoBiometria.Indefinido;
                        votoScoreBiometria = 0;
                        votoQtdTeclasIndevidas = 0;
                        votoHabilitacaoCancelada = false;
                        votoDF = false;
                        votoDE = false;
                        votoSE = false;
                        votoGO = false;
                        votoPR = false;
                        votoNuloSuspensaoDF = false;
                        votoNuloSuspensaoDE = false;
                        votoNuloSuspensaoSE = false;
                        votoNuloSuspensaoGO = false;
                        votoNuloSuspensaoPR = false;
                        votoComputado = false;
                        votoEleitorSuspenso = false;
                        jaVotou = false;
                    }
                    else if (estaVotando)
                    {
                        if (linha.ToLower().Contains("Aguardando digitação do título".ToLower()))
                        {
                            // A votação acabou inesperadamente. Salvar o que tiver por enquanto.
                            votoHabilitacaoCancelada = true;
                            dhFimVoto = dataLinha;
                            votoLinhaFim = linhaAtual;

                            // Salvar o voto
                            VotosLog voto = new VotosLog()
                            {
                                SecaoEleitoralMunicipioCodigo = codMunicipio.ToInt(),
                                SecaoEleitoralCodigoZonaEleitoral = codZona.ToShort(),
                                SecaoEleitoralCodigoSecao = codSecao.ToShort(),
                                DedoBiometria = votoDedoBiometria,
                                ScoreBiometria = votoScoreBiometria,
                                PossuiBiometria = votoPossuiBiometria,
                                EleitorSuspenso = votoEleitorSuspenso,
                                VotoComputado = votoComputado,
                                VotouDF = votoDF,
                                VotouDE = votoDE,
                                VotouSE = votoSE,
                                VotouGO = votoGO,
                                VotouPR = votoPR,
                                VotoNuloSuspensaoDF = votoNuloSuspensaoDF,
                                VotoNuloSuspensaoDE = votoNuloSuspensaoDE,
                                VotoNuloSuspensaoSE = votoNuloSuspensaoSE,
                                VotoNuloSuspensaoGO = votoNuloSuspensaoGO,
                                VotoNuloSuspensaoPR = votoNuloSuspensaoPR,
                                InicioVoto = dhInicioVoto,
                                FimVoto = dhFimVoto,
                                HabilitacaoCancelada = votoHabilitacaoCancelada,
                                QtdTeclasIndevidas = votoQtdTeclasIndevidas,
                                IdVotoLog = numeroVoto,
                                LinhaLog = votoLinha,
                                LinhaLogFim = votoLinhaFim,
                                HabilitacaoUrna = dhHabilitacaoUrna,
                                ModeloUrnaEletronica = modeloUrna,
                                CodigoIdentificacaoUrnaEletronica = codigoIdentificacaoUrnaEletronica,
                                UrnaTestada = urnaTestada,
                                MunicipioCodigoLog = codigoMunicipioLog,
                                CodigoZonaEleitoralLog = codigoZonaEleitoralLog,
                                CodigoSecaoLog = codigoSecaoEleitoralLog,
                                JaVotou = jaVotou,
                            };
                            retorno.Add(voto);

                            estaVotando = false;
                        }
                        else if (linha.ToLower().Contains("Título inválido".ToLower())
                            || linha.ToLower().Contains("Mesário cancelou entrada dos dados".ToLower())
                            || linha.ToLower().Contains("Eleitor já justificou".ToLower())
                            || linha.ToLower().Contains("Urna ligada em".ToLower())
                            )
                        {
                            // Essas mensagens abortam o voto atual.
                            estaVotando = false;
                            if (votoPR || votoGO || votoSE || votoDE || votoDF)
                            {
                                // mensagens += $"Linha {linhaAtual} - O votação que iniciou na linha {votoLinha} não foi computada.\n";

                                dhFimVoto = dataLinha;
                                votoLinhaFim = linhaAtual;

                                // Salvar o voto
                                VotosLog voto = new VotosLog()
                                {
                                    SecaoEleitoralMunicipioCodigo = codMunicipio.ToInt(),
                                    SecaoEleitoralCodigoZonaEleitoral = codZona.ToShort(),
                                    SecaoEleitoralCodigoSecao = codSecao.ToShort(),
                                    DedoBiometria = votoDedoBiometria,
                                    ScoreBiometria = votoScoreBiometria,
                                    PossuiBiometria = votoPossuiBiometria,
                                    EleitorSuspenso = votoEleitorSuspenso,
                                    VotoComputado = votoComputado,
                                    VotouDF = votoDF,
                                    VotouDE = votoDE,
                                    VotouSE = votoSE,
                                    VotouGO = votoGO,
                                    VotouPR = votoPR,
                                    VotoNuloSuspensaoDF = votoNuloSuspensaoDF,
                                    VotoNuloSuspensaoDE = votoNuloSuspensaoDE,
                                    VotoNuloSuspensaoSE = votoNuloSuspensaoSE,
                                    VotoNuloSuspensaoGO = votoNuloSuspensaoGO,
                                    VotoNuloSuspensaoPR = votoNuloSuspensaoPR,
                                    InicioVoto = dhInicioVoto,
                                    FimVoto = dhFimVoto,
                                    HabilitacaoCancelada = votoHabilitacaoCancelada,
                                    QtdTeclasIndevidas = votoQtdTeclasIndevidas,
                                    IdVotoLog = numeroVoto,
                                    LinhaLog = votoLinha,
                                    LinhaLogFim = votoLinhaFim,
                                    HabilitacaoUrna = dhHabilitacaoUrna,
                                    ModeloUrnaEletronica = modeloUrna,
                                    CodigoIdentificacaoUrnaEletronica = codigoIdentificacaoUrnaEletronica,
                                    UrnaTestada = urnaTestada,
                                    MunicipioCodigoLog = codigoMunicipioLog,
                                    CodigoZonaEleitoralLog = codigoZonaEleitoralLog,
                                    CodigoSecaoLog = codigoSecaoEleitoralLog,
                                    JaVotou = jaVotou,
                                };
                                retorno.Add(voto);
                            }
                        }
                        else if (linha.ToLower().Contains("Justificativa recebida".ToLower()))
                        {
                            qtdJustificativas++;
                            estaVotando = false;
                        }
                        else if (linha.ToLower().Contains("O eleitor identificado já votou".ToLower()))
                        {
                            qtdJaVotou++;
                            jaVotou = true;

                            dhFimVoto = dataLinha;
                            votoLinhaFim = linhaAtual;

                            // Salvar o voto
                            VotosLog voto = new VotosLog()
                            {
                                SecaoEleitoralMunicipioCodigo = codMunicipio.ToInt(),
                                SecaoEleitoralCodigoZonaEleitoral = codZona.ToShort(),
                                SecaoEleitoralCodigoSecao = codSecao.ToShort(),
                                DedoBiometria = votoDedoBiometria,
                                ScoreBiometria = votoScoreBiometria,
                                PossuiBiometria = votoPossuiBiometria,
                                EleitorSuspenso = votoEleitorSuspenso,
                                VotoComputado = votoComputado,
                                VotouDF = votoDF,
                                VotouDE = votoDE,
                                VotouSE = votoSE,
                                VotouGO = votoGO,
                                VotouPR = votoPR,
                                VotoNuloSuspensaoDF = votoNuloSuspensaoDF,
                                VotoNuloSuspensaoDE = votoNuloSuspensaoDE,
                                VotoNuloSuspensaoSE = votoNuloSuspensaoSE,
                                VotoNuloSuspensaoGO = votoNuloSuspensaoGO,
                                VotoNuloSuspensaoPR = votoNuloSuspensaoPR,
                                InicioVoto = dhInicioVoto,
                                FimVoto = dhFimVoto,
                                HabilitacaoCancelada = votoHabilitacaoCancelada,
                                QtdTeclasIndevidas = votoQtdTeclasIndevidas,
                                IdVotoLog = numeroVoto,
                                LinhaLog = votoLinha,
                                LinhaLogFim = votoLinhaFim,
                                HabilitacaoUrna = dhHabilitacaoUrna,
                                ModeloUrnaEletronica = modeloUrna,
                                CodigoIdentificacaoUrnaEletronica = codigoIdentificacaoUrnaEletronica,
                                UrnaTestada = urnaTestada,
                                MunicipioCodigoLog = codigoMunicipioLog,
                                CodigoZonaEleitoralLog = codigoZonaEleitoralLog,
                                CodigoSecaoLog = codigoSecaoEleitoralLog,
                                JaVotou = jaVotou,
                            };
                            retorno.Add(voto);

                            estaVotando = false;
                        }
                        else if (linha.ToLower().Contains("O eleitor não possui biometria".ToLower()))
                        {
                            votoPossuiBiometria = false;
                        }
                        else if (linha.ToLower().Contains("Dedo reconhecido e o score para habilitá-lo".ToLower()))
                        {
                            votoPossuiBiometria = true;
                            if (linha.ToLower().Contains("Polegar esquerdo".ToLower()))
                                votoDedoBiometria = DedoBiometria.PolegarEsquerdo;
                            else if (linha.ToLower().Contains("Polegar direito".ToLower()))
                                votoDedoBiometria = DedoBiometria.PolegarDireito;
                            else if (linha.ToLower().Contains("Indicador esquerdo".ToLower()))
                                votoDedoBiometria = DedoBiometria.IndicadorEsquerdo;
                            else if (linha.ToLower().Contains("Indicador direito".ToLower()))
                                votoDedoBiometria = DedoBiometria.IndicadorDireito;

                            var strScore = linha.Substring(linha.IndexOf("[") + 1);
                            strScore = strScore.Substring(0, strScore.IndexOf("]"));
                            votoScoreBiometria = strScore.ToShort();
                        }
                        else if (linha.ToLower().Contains("Eleitor foi habilitado".ToLower()))
                        {
                            dhHabilitacaoUrna = dataLinha;
                        }
                        else if (linha.ToLower().Contains("Tecla indevida pressionada".ToLower()))
                        {
                            votoQtdTeclasIndevidas++;
                        }
                        else if (linha.ToLower().Contains("Eleitor foi suspenso pelo mesário".ToLower()))
                        {
                            votoEleitorSuspenso = true;
                        }
                        else if (linha.ToLower().Contains("Voto confirmado para".ToLower()))
                        {
                            if (linha.ToLower().Contains("Deputado Federal".ToLower()))
                                votoDF = true;
                            else if (linha.ToLower().Contains("Deputado Estadual".ToLower()) || linha.ToLower().Contains("Deputado Distrital".ToLower()))
                                votoDE = true;
                            else if (linha.ToLower().Contains("Senador".ToLower()))
                                votoSE = true;
                            else if (linha.ToLower().Contains("Governador".ToLower()))
                                votoGO = true;
                            else if (linha.ToLower().Contains("Presidente".ToLower()))
                                votoPR = true;
                        }
                        else if (linha.ToLower().Contains("Atribuido voto nulo por suspensão".ToLower()))
                        {
                            if (linha.ToLower().Contains("Deputado Federal".ToLower()))
                                votoNuloSuspensaoDF = true;
                            else if (linha.ToLower().Contains("Deputado Estadual".ToLower()) || linha.ToLower().Contains("Deputado Distrital".ToLower()))
                                votoNuloSuspensaoDE = true;
                            else if (linha.ToLower().Contains("Senador".ToLower()))
                                votoNuloSuspensaoSE = true;
                            else if (linha.ToLower().Contains("Governador".ToLower()))
                                votoNuloSuspensaoGO = true;
                            else if (linha.ToLower().Contains("Presidente".ToLower()))
                                votoNuloSuspensaoPR = true;
                        }
                        else if (linha.ToLower().Contains("O voto do eleitor foi computado".ToLower()))
                        {
                            votoComputado = true;
                            dhFimVoto = dataLinha;
                            votoLinhaFim = linhaAtual;

                            // Salvar o voto
                            VotosLog voto = new VotosLog()
                            {
                                SecaoEleitoralMunicipioCodigo = codMunicipio.ToInt(),
                                SecaoEleitoralCodigoZonaEleitoral = codZona.ToShort(),
                                SecaoEleitoralCodigoSecao = codSecao.ToShort(),
                                DedoBiometria = votoDedoBiometria,
                                ScoreBiometria = votoScoreBiometria,
                                PossuiBiometria = votoPossuiBiometria,
                                EleitorSuspenso = votoEleitorSuspenso,
                                VotoComputado = votoComputado,
                                VotouDF = votoDF,
                                VotouDE = votoDE,
                                VotouSE = votoSE,
                                VotouGO = votoGO,
                                VotouPR = votoPR,
                                VotoNuloSuspensaoDF = votoNuloSuspensaoDF,
                                VotoNuloSuspensaoDE = votoNuloSuspensaoDE,
                                VotoNuloSuspensaoSE = votoNuloSuspensaoSE,
                                VotoNuloSuspensaoGO = votoNuloSuspensaoGO,
                                VotoNuloSuspensaoPR = votoNuloSuspensaoPR,
                                InicioVoto = dhInicioVoto,
                                FimVoto = dhFimVoto,
                                HabilitacaoCancelada = votoHabilitacaoCancelada,
                                QtdTeclasIndevidas = votoQtdTeclasIndevidas,
                                IdVotoLog = numeroVoto,
                                LinhaLog = votoLinha,
                                LinhaLogFim = votoLinhaFim,
                                HabilitacaoUrna = dhHabilitacaoUrna,
                                ModeloUrnaEletronica = modeloUrna,
                                CodigoIdentificacaoUrnaEletronica = codigoIdentificacaoUrnaEletronica,
                                UrnaTestada = urnaTestada,
                                MunicipioCodigoLog = codigoMunicipioLog,
                                CodigoZonaEleitoralLog = codigoZonaEleitoralLog,
                                CodigoSecaoLog = codigoSecaoEleitoralLog,
                                JaVotou = jaVotou,
                            };
                            retorno.Add(voto);

                            estaVotando = false;
                        }
                    }
                    else if (!urnaEncerrada && linha.ToLower().Contains("Procedimento de encerramento confirmado".ToLower()))
                    {
                        urnaEncerrada = true;
                        dhFechamentoUrna = dataLinha;
                    }
                }
            }

            // Varrer todas os itens de log para ver se há mais de um modelo de urna ou mais de um código identificador de urna
            short tmpModeloUrna = 0;
            bool houveTrocaDeModeloDeUrna = false;

            int tmpCodigoIdentificacaoUrnaEletronica = 0;
            bool houveTrocaDeCodigoIdentificadorDeUrna = false;
            foreach (var item in retorno)
            {
                if (!houveTrocaDeModeloDeUrna && tmpModeloUrna == 0 && item.ModeloUrnaEletronica != tmpModeloUrna)
                    tmpModeloUrna = item.ModeloUrnaEletronica;
                else if (!houveTrocaDeModeloDeUrna && item.ModeloUrnaEletronica != tmpModeloUrna)
                {
                    houveTrocaDeModeloDeUrna = true;
                    tmpModeloUrna = 0;
                }

                if (!houveTrocaDeCodigoIdentificadorDeUrna && tmpCodigoIdentificacaoUrnaEletronica == 0 && item.ModeloUrnaEletronica != tmpCodigoIdentificacaoUrnaEletronica)
                    tmpCodigoIdentificacaoUrnaEletronica = item.CodigoIdentificacaoUrnaEletronica;
                else if (!houveTrocaDeCodigoIdentificadorDeUrna && item.CodigoIdentificacaoUrnaEletronica != tmpCodigoIdentificacaoUrnaEletronica)
                {
                    houveTrocaDeCodigoIdentificadorDeUrna = true;
                    tmpCodigoIdentificacaoUrnaEletronica = 0;
                }

                if (houveTrocaDeCodigoIdentificadorDeUrna && houveTrocaDeModeloDeUrna)
                    break; // Não é mais necessário conferir os logs
            }

            modeloUrna = houveTrocaDeModeloDeUrna ? (short)0 : tmpModeloUrna;
            codigoIdentificacaoUrnaEletronica = houveTrocaDeCodigoIdentificadorDeUrna ? (short)0 : tmpCodigoIdentificacaoUrnaEletronica;

            return retorno;
        }

        public string CompararLogUrnaComBU(BoletimUrna bu, List<VotosLog> votosLog, bool compararVotos)
        {
            StringBuilder sbRetorno = new StringBuilder();

            // Ver se a urna foi zerada no máximo 2 horas antes do início das eleições
            if (bu.Zeresima == DateTime.MinValue)
                sbRetorno.AppendLine("Não há informação de Zerésima.");
            else if ((bu.AberturaUrnaEletronica - bu.Zeresima).TotalHours > 2)
                sbRetorno.AppendLine($"A Zerésima foi realizada mais de duas horas antes da abertura da Urna. Abertura da urna: {bu.AberturaUrnaEletronica.DataHoraPTBR()} - Zerésima: {bu.Zeresima.DataHoraPTBR()}");

            if (compararVotos)
            {
                // Ver se há votos antes da abertura ou depois do fechamento da urna
                var votosAntesAbertura = votosLog.Where(x => x.InicioVoto < bu.AberturaUrnaEletronica && x.VotoComputado).OrderBy(x => x.InicioVoto).ToList();
                if (votosAntesAbertura.Any())
                    sbRetorno.AppendLine($"Existem {votosAntesAbertura.Count} votos computados antes do início da votação. Votação iniciou em {bu.AberturaUrnaEletronica.DataHoraPTBR()} e o primeiro voto foi em {votosAntesAbertura.First().InicioVoto.DataHoraPTBR()}");

                var votosAposFechamento = votosLog.Where(x => x.InicioVoto > bu.FechamentoUrnaEletronica && x.VotoComputado).OrderBy(x => x.FimVoto).ToList();
                if (votosAposFechamento.Any())
                    sbRetorno.AppendLine($"Existem {votosAposFechamento.Count} votos computados após do final da votação. Votação terminou em {bu.FechamentoUrnaEletronica.DataHoraPTBR()} e o último voto foi em {votosAposFechamento.Last().InicioVoto.DataHoraPTBR()}");

                // Ver se a quantidade de votos para cada cargo corresponde a quantidade do log
                int qtdVotosLog = 0;
                int qtdVotosBU = 0;

                qtdVotosLog = votosLog.Count(x => (x.VotouDF || x.VotoNuloSuspensaoDF) && x.VotoComputado);
                qtdVotosBU = bu.DF_Total;
                if (qtdVotosLog != bu.DF_Total)
                    sbRetorno.AppendLine($"Quantidade de votos para Dep Federal é diferente no BU ({qtdVotosBU}) e no Log ({qtdVotosLog})");

                qtdVotosLog = votosLog.Count(x => (x.VotouDE || x.VotoNuloSuspensaoDE) && x.VotoComputado);
                qtdVotosBU = bu.DE_Total;
                if (qtdVotosLog != bu.DE_Total)
                    sbRetorno.AppendLine($"Quantidade de votos para Dep Est/Dist é diferente no BU ({qtdVotosBU}) e no Log ({qtdVotosLog})");

                qtdVotosLog = votosLog.Count(x => (x.VotouSE || x.VotoNuloSuspensaoSE) && x.VotoComputado);
                qtdVotosBU = bu.SE_Total;
                if (qtdVotosLog != bu.SE_Total)
                    sbRetorno.AppendLine($"Quantidade de votos para Senador é diferente no BU ({qtdVotosBU}) e no Log ({qtdVotosLog})");

                qtdVotosLog = votosLog.Count(x => (x.VotouGO || x.VotoNuloSuspensaoGO) && x.VotoComputado);
                qtdVotosBU = bu.GO_Total;
                if (qtdVotosLog != bu.GO_Total)
                    sbRetorno.AppendLine($"Quantidade de votos para Governador é diferente no BU ({qtdVotosBU}) e no Log ({qtdVotosLog})");

                qtdVotosLog = votosLog.Count(x => (x.VotouPR || x.VotoNuloSuspensaoPR) && x.VotoComputado);
                qtdVotosBU = bu.PR_Total;
                if (qtdVotosLog != bu.PR_Total)
                    sbRetorno.AppendLine($"Quantidade de votos para Presidente é diferente no BU ({qtdVotosBU}) e no Log ({qtdVotosLog})");
            }

            return sbRetorno.ToString();
        }

        private decimal CompararLogs(List<string> arquivo1, List<string> arquivo2)
        {
            int linhasIguais = 0;
            foreach (var linha1 in arquivo1)
            {
                foreach (var linha2 in arquivo2)
                {
                    if (linha1 == linha2)
                    {
                        linhasIguais++;
                    }
                }
            }

            if (arquivo1.Count() == 0)
                return 0;
            else
                return (linhasIguais / arquivo1.Count()) * 100;
        }


        private DateTime ObterDataLinha(string linha)
        {
            var strData = linha.Substring(0, 19);
            var dataRetorno = DateTime.ParseExact(strData, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return dataRetorno;
        }
    }
}
