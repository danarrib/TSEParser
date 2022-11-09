﻿using Lexical.FileProvider.PackageLoader;
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
            out DateTime dhZeresima, out string mensagens, out short modeloUrna, bool segundoTurno, string arquivoLogSA)
        {
            string descricaoSecao = $"UF {UF}, Município {codMunicipio} {nomeMunicipio}, Zona {codZona}, Seção {codSecao}";
            List<VotosLog> retorno = new List<VotosLog>();
            List<string> arrTextoLog = new List<string>();
            List<LogDeUrna> arquivosLog = new List<LogDeUrna>();
            mensagens = string.Empty;

            // O arquivo .logjez é um arquivo compactado. Precisa descompactar para pegar o texto.
            try
            {
                using (var zip = SevenZipArchive.Open(arquivoLog))
                {
                    /*
                    if (zip.Entries.Count > 2)
                    {
                        var tmpArquivos = string.Empty;
                        foreach (var arquivo in zip.Entries)
                        {
                            tmpArquivos += (tmpArquivos.Length > 0 ? ", " : "") + arquivo.Key;
                        }
                        mensagens += $"O arquivo .logjez possui {zip.Entries.Count} arquivos dentro: {tmpArquivos}\n";
                    }
                    */

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
                                //arrTextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + zip2Entry.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

                                var novoLog = new LogDeUrna(zip2Entry.Key, arquivo.Key, zip2Entry.LastModifiedTime.Value);
                                novoLog.TextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + zip2Entry.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
                                arquivosLog.Add(novoLog);

                                File.Delete(diretorioHash + @"\" + zip2Entry.Key);
                            }
                        }
                        else if (arquivo.Key.ToLower() == "logd.dat")
                        {
                            arquivo.WriteToFile(diretorioHash + @"\" + arquivo.Key);
                            //arrTextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + arquivo.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

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
                dhZeresima = DateTime.MinValue;
                modeloUrna = 0;
                return retorno;
            }

            // O arquivo .logsajez é um arquivo compactado. Precisa descompactar para pegar o texto.
            if (!string.IsNullOrWhiteSpace(arquivoLogSA))
            {
                try
                {
                    using (var zip = SevenZipArchive.Open(arquivoLogSA))
                    {
                        /*
                        if (zip.Entries.Count > 2)
                        {
                            var tmpArquivos = string.Empty;
                            foreach (var arquivo in zip.Entries)
                            {
                                tmpArquivos += (tmpArquivos.Length > 0 ? ", " : "") + arquivo.Key;
                            }
                            mensagens += $"O arquivo .logjez possui {zip.Entries.Count} arquivos dentro: {tmpArquivos}\n";
                        }
                        */

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
                                    //arrTextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + zip2Entry.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

                                    var novoLog = new LogDeUrna(zip2Entry.Key, arquivo.Key, zip2Entry.LastModifiedTime.Value);
                                    novoLog.TextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + zip2Entry.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
                                    arquivosLog.Add(novoLog);

                                    File.Delete(diretorioHash + @"\" + zip2Entry.Key);
                                }
                            }
                            else if (arquivo.Key.ToLower() == "logd.dat")
                            {
                                arquivo.WriteToFile(diretorioHash + @"\" + arquivo.Key);
                                //arrTextoLog.AddRange(File.ReadAllText(diretorioHash + @"\" + arquivo.Key, Encoding.UTF7).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

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
                    dhZeresima = DateTime.MinValue;
                    modeloUrna = 0;
                    return retorno;
                }
            }

            // Agora temos todos os logs em memória. Decidir quais serão usados.
            if (arquivosLog.Count == 1)
            {
                // Só tem um log, então é esse mesmo.
                arrTextoLog = arquivosLog.First().TextoLog;
            }
            else
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

                // Selecionamos os arquivos. Agora montar o arquivo final
                foreach (var item in arquivosLog)
                {
                    arrTextoLog.AddRange(item.TextoLog);
                }
            }

            int linhaAtual = 0;
            var dataInicioSegundoTurno = DateTime.MinValue;
            if (segundoTurno)
            {
                // Precisa percorrer o log inteiro até encontrar onde começa o segundo turno.
                // Tudo o que vier antes disso é relacionado ao primeiro turno e deve ser ignorado.
                foreach (var linha in arrTextoLog)
                {
                    if (linha.ToLower().Contains("Iniciando aplicação - Oficial - 2º turno".ToLower()))
                    {
                        dataInicioSegundoTurno = ObterDataLinha(linha);
                        break;
                    }
                }

                // Remover do log todas as linhas que são anteriores ao início do segundo turno
                List<string> novoArrTextoLog = new List<string>();
                foreach (var linha in arrTextoLog)
                {
                    if (linha.Length > 19)
                    {
                        try
                        {
                            var dataLinha = ObterDataLinha(linha);
                            if (dataLinha >= dataInicioSegundoTurno)
                            {
                                novoArrTextoLog.Add(linha);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Não fazer nada
                        }
                    }
                }
                arrTextoLog = novoArrTextoLog;
            }

            // Começar a processar os logs
            var urnaProntaParaReceberVotos = false;
            var urnaEncerrada = false;
            var estaVotando = false;
            var dhAberturaUrna = DateTime.MinValue;
            var dhFechamentoUrna = DateTime.MinValue;
            short qtdJaVotou = 0;
            short qtdJustificativas = 0;
            dhZeresima = DateTime.MinValue;
            short numeroVoto = 0;
            modeloUrna = 0;

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

            foreach (var linha in arrTextoLog)
            {
                linhaAtual++;

                if (!urnaProntaParaReceberVotos && linha.ToLower().Contains("Urna pronta para receber votos".ToLower()))
                {
                    urnaProntaParaReceberVotos = true;
                    dhAberturaUrna = ObterDataLinha(linha);
                }
                else if (!estaVotando &&
                    (linha.ToLower().Contains("Imprimindo relatório [ZERÉSIMA]".ToLower())
                    || linha.ToLower().Contains("Imprimindo relatório [ZERÉSIMA DE SEÇÃO]".ToLower()))
                    )
                {
                    var tmpZeresima = ObterDataLinha(linha);
                    if (tmpZeresima > dhZeresima)
                        dhZeresima = tmpZeresima;
                }
                else if (!estaVotando && linha.ToLower().Contains("Identificação do Modelo de Urna".ToLower()))
                {
                    var chave = "Identificação do Modelo de Urna: UE";
                    var tmp = linha.Substring(linha.IndexOf(chave) + chave.Length);
                    tmp = tmp.Substring(0, tmp.IndexOf("\t"));
                    var tmpModeloUrna = tmp.ToShort();
                    /*
                    if (modeloUrna > 0 && modeloUrna != tmpModeloUrna)
                        mensagens += $"O modelo da UE mudou no meio do log. Antes era {modeloUrna} e agora é {tmpModeloUrna}.\n";
                    */
                    modeloUrna = tmpModeloUrna;
                }
                else if (!estaVotando && linha.ToLower().Contains("Título digitado pelo mesário".ToLower()))
                {
                    estaVotando = true;
                    numeroVoto++;
                    dhInicioVoto = ObterDataLinha(linha);
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
                }
                else if (estaVotando)
                {
                    if (linha.ToLower().Contains("Aguardando digitação do título".ToLower()))
                    {
                        // A votação acabou inesperadamente. Salvar o que tiver por enquanto.
                        votoHabilitacaoCancelada = true;
                        dhFimVoto = ObterDataLinha(linha);
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
                            mensagens += $"Linha {linhaAtual} - O votação que iniciou na linha {votoLinha} não foi computada.\n";
                    }
                    else if (linha.ToLower().Contains("Justificativa recebida".ToLower()))
                    {
                        qtdJustificativas++;
                        estaVotando = false;
                    }
                    else if (linha.ToLower().Contains("O eleitor identificado já votou".ToLower()))
                    {
                        qtdJaVotou++;
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
                        dhHabilitacaoUrna = ObterDataLinha(linha);
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
                        dhFimVoto = ObterDataLinha(linha);
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
                        };
                        retorno.Add(voto);

                        estaVotando = false;
                    }
                }
                else if (!urnaEncerrada && linha.ToLower().Contains("Procedimento de encerramento confirmado".ToLower()))
                {
                    urnaEncerrada = true;
                    dhFechamentoUrna = ObterDataLinha(linha);
                }
            }

            // VerificarSanidade(arrTextoLog, retorno, descricaoSecao);

            return retorno;
        }

        public void VerificarSanidade(List<string> arrLog, List<VotosLog> votosLog, string descricaoSecao)
        {
            // Procurar pela string "Voto confirmado" no texto, porém apenas nas linhas que não estão presentes nos votos processados
            int numLinha = 0;
            foreach (var linha in arrLog)
            {
                numLinha++;

                if (linha.ToLower().Contains("Voto confirmado para".ToLower()))
                {
                    var voto = votosLog.Where(x => x.LinhaLog < numLinha && x.LinhaLogFim > numLinha).ToList();
                    if (voto == null || voto.Count == 0)
                    {
                        // Não encontrou voto processado para essa linha. Parar aqui e ver o que aconteceu no arquivo
                        Console.WriteLine($"{descricaoSecao} - Teste de sanidade falhou.");
                    }
                }
            }
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
