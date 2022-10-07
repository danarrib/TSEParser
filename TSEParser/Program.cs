using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace TSEParser
{
    internal class Program
    {
        public const string diretorioLocalDados = @"D:\Downloads\Urnas\";
        public const string urlTSE = @"https://resultados.tse.jus.br/oficial/ele2022/arquivo-urna/406/";

        static void Main(string[] args)
        {
            //var arquivo = @"D:\Downloads\Urnas\AC\01066\0004\0077\395459446c754b34572b56304a706a6a413454646f6f5a6f5664426f5169564241506566444932644f75493d\o00406-0106600040077.imgbu";
            //var bu = ProcessarBoletimUrna(arquivo);

            List<string> UFs = new List<string>();
            UFs.AddRange(new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG" }); //, "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" });

            List<string> UFsJaProcessadas = new List<string>();
            UFsJaProcessadas.AddRange(new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", });

            foreach (var uf in UFsJaProcessadas)
                UFs.Remove(uf);

            foreach (var UF in UFs)
            {
                ProcessarArquivos(UF);
            }
        }

        public static void ProcessarArquivos(string UF)
        {
            // Ler o arquivo de configuração deste estado
            string diretorioUF = diretorioLocalDados + UF;
            if (!Directory.Exists(diretorioUF))
                throw new Exception($"A UF informada ({UF}) não existe no diretório de dados.");

            if (!File.Exists(diretorioUF + @"\config.json"))
                throw new Exception($"O arquivo de configuração da UF informada ({UF}) não existe.");

            var jsonConfiguracaoUF = File.ReadAllText(diretorioUF + @"\config.json");

            CrawlerModels.UFConfig configuracaoUF;
            try
            {
                configuracaoUF = JsonSerializer.Deserialize<CrawlerModels.UFConfig>(jsonConfiguracaoUF);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao interpretar o JSON de configuração da UF " + UF, ex);
            }

            foreach (var abr in configuracaoUF.abr)
            {
                foreach (var municipio in abr.mu)
                {
                    // Criar um diretório para o município
                    string diretorioMunicipio = diretorioUF + @"\" + municipio.cd;
                    if (!Directory.Exists(diretorioMunicipio))
                        throw new Exception($"O diretório do município {municipio.cd} não foi localizado.");

                    // Para cada Zona eleitoral
                    foreach (var zonaEleitoral in municipio.zon)
                    {
                        // Criar um diretório para a zona eleitoral
                        string diretorioZona = diretorioMunicipio + @"\" + zonaEleitoral.cd;
                        if (!Directory.Exists(diretorioZona))
                            throw new Exception($"O diretório da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd} não foi localizado.");

                        // Para cada Seçao desta zona eleitoral
                        foreach (var secao in zonaEleitoral.sec)
                        {
                            Console.WriteLine($"Processando arquivo UF {UF}, Mun {municipio.cd} {municipio.nm}, Zona {zonaEleitoral.cd}, Seção {secao.ns}");
                            // Criar um diretório para a seção eleitoral
                            string diretorioSecao = diretorioZona + @"\" + secao.ns;
                            if (!Directory.Exists(diretorioSecao))
                                throw new Exception($"O diretório da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd} não foi localizado.");

                            if (!File.Exists(diretorioSecao + @"\config.json"))
                                throw new Exception($"O arquivo de configuração da Seção {secao.ns} zona {zonaEleitoral.cd} muncipio {municipio.cd} não foi localizado.");

                            // O arquivo de configuração já existe. Então usar ele.
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

                            // Agora já temos a configuração da seção. Basta baixar os arquivos
                            foreach (var objHash in boletimUrna.hashes)
                            {
                                // Criar um diretório para o hash
                                string diretorioHash = diretorioSecao + @"\" + objHash.hash;
                                if (!Directory.Exists(diretorioHash))
                                    throw new Exception($"O diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd} não foi localizado.");

                                // Obter o arquivo imgbu
                                var arquivo = objHash.nmarq.Find(x => x.Contains(".imgbu"));

                                if (!string.IsNullOrWhiteSpace(arquivo))
                                {
                                    if (!File.Exists(diretorioHash + @"\" + arquivo))
                                        throw new Exception($"O arquivo {arquivo} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {secao.ns} da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd}.");

                                    var bu = ProcessarBoletimUrna(diretorioHash + @"\" + arquivo);
                                    if (BoletimEstaCorrompido(bu))
                                    {
                                        // O arquivo deve estar corrompido. Tentar baixar novamente do TSE para processar
                                        Console.WriteLine("O arquivo atual está corrompido. Tentando baixar novamente do TSE.");
                                        string urlArquivoABaixar = urlTSE + @"dados/" + UF.ToLower() + @"/" + municipio.cd + @"/" + zonaEleitoral.cd + @"/" + secao.ns + @"/" + objHash.hash + @"/" + arquivo;
                                        try
                                        {
                                            BaixarArquivo(urlArquivoABaixar, diretorioHash + @"\" + arquivo);
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new Exception("Erro ao baixar o arquivo " + arquivo + " da " + secao.ns + ", zona " + zonaEleitoral.cd + ", município " + municipio.cd + ", UF " + UF, ex);
                                        }
                                        bu = ProcessarBoletimUrna(diretorioHash + @"\" + arquivo);

                                        if (BoletimEstaCorrompido(bu))
                                        {
                                            // Mesmo depois de baixar novamente o arquivo, ele continua corrompido. Isso não deveria acontecer.
                                            // Precisamos gravar essa falha

                                            EscreverLog($"UF {UF} MUN {municipio.cd} ZN {zonaEleitoral.cd} SE {secao.ns} - Arquivo corrompido. Re-tentativa não fuincionou. {urlArquivoABaixar} ");
                                        }
                                    }

                                    try
                                    {
                                        SalvarBoletimUrna(bu);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Tentar processar novamente, desta vez debugando
                                        bu = ProcessarBoletimUrna(diretorioHash + @"\" + arquivo);
                                        SalvarBoletimUrna(bu);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Para esta seção, não há arquivo. UF {UF}, Município {municipio.cd}, Zona {zonaEleitoral.cd}, Seção {secao.ns}");
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool BoletimEstaCorrompido(BoletimUrna bu)
        {
            if (string.IsNullOrWhiteSpace(bu.UF))
                return true;
            if (string.IsNullOrWhiteSpace(bu.CodigoMunicipio))
                return true;
            if (string.IsNullOrWhiteSpace(bu.SecaoEleitoral))
                return true;

            return false;
        }

        public static void BaixarArquivo(string urlArquivo, string arquivoLocal)
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

        public static void EscreverLog(string mensagem)
        {
            string arquivoLog = diretorioLocalDados + "TSEParser.log";
            File.AppendAllText(arquivoLog, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + mensagem + "\n");
        }

        public static void SalvarBoletimUrna(BoletimUrna bu)
        {
            using (var context = new TSEContext())
            {
                var uf = context.UnidadeFederativa.Find(bu.UF);
                if (uf == null)
                {
                    uf = new UnidadeFederativa() { Sigla = bu.UF, Nome = bu.UF, };
                    context.UnidadeFederativa.Add(uf);
                }

                var municipio = context.Municipio.Find(int.Parse(bu.CodigoMunicipio));
                if (municipio == null)
                {
                    municipio = new Municipio() { UF = uf, UFSigla = uf.Sigla, Nome = bu.NomeMunicipio, Codigo = bu.CodigoMunicipio.ToInt(), };
                    context.Municipio.Add(municipio);
                }

                var secao = new SecaoEleitoral();
                #region Seção
                secao.Municipio = municipio;
                secao.MunicipioCodigo = municipio.Codigo;

                secao.CodigoZonaEleitoral = bu.ZonaEleitoral.ToShort();
                secao.CodigoSecao = bu.SecaoEleitoral.ToShort();
                secao.CodigoLocalVotacao = bu.LocalVotacao.ToShort();

                secao.EleitoresAptos = bu.EleitoresAptos;
                secao.Comparecimento = bu.Comparecimento;
                secao.EleitoresFaltosos = bu.EleitoresFaltosos;
                secao.HabilitadosPorAnoNascimento = bu.HabilitadosPorAnoNascimento;

                secao.CodigoIdentificacaoUrnaEletronica = bu.CodigoIdentificacaoUrnaEletronica.ToInt();
                secao.AberturaUrnaEletronica = bu.AberturaUrnaEletronica;
                secao.FechamentoUrnaEletronica = bu.FechamentoUrnaEletronica;

                secao.DF_EleitoresAptos = bu.EleitoresAptosDeputadoFederal;
                secao.DF_VotosNominais = bu.VotosNominaisDeputadoFederal;
                secao.DF_VotosLegenda = bu.VotosLegendaDeputadoFederal;
                secao.DF_Brancos = bu.BrancosDeputadoFederal;
                secao.DF_Nulos = bu.NulosDeputadoFederal;
                secao.DF_Total = bu.TotalApuradoDeputadoFederal;

                secao.DE_EleitoresAptos = bu.EleitoresAptosDeputadoEstadual;
                secao.DE_VotosNominais = bu.VotosNominaisDeputadoEstadual;
                secao.DE_VotosLegenda = bu.VotosLegendaDeputadoEstadual;
                secao.DE_Brancos = bu.BrancosDeputadoEstadual;
                secao.DE_Nulos = bu.NulosDeputadoEstadual;
                secao.DE_Total = bu.TotalApuradoDeputadoEstadual;

                secao.SE_EleitoresAptos = bu.EleitoresAptosSenador;
                secao.SE_VotosNominais = bu.VotosNominaisSenador;
                secao.SE_Brancos = bu.BrancosSenador;
                secao.SE_Nulos = bu.NulosSenador;
                secao.SE_Total = bu.TotalApuradoSenador;

                secao.GO_EleitoresAptos = bu.EleitoresAptosGovernador;
                secao.GO_VotosNominais = bu.VotosNominaisGovernador;
                secao.GO_Brancos = bu.BrancosGovernador;
                secao.GO_Nulos = bu.NulosGovernador;
                secao.GO_Total = bu.TotalApuradoGovernador;

                secao.PR_EleitoresAptos = bu.EleitoresAptosPresidente;
                secao.PR_VotosNominais = bu.VotosNominaisPresidente;
                secao.PR_Brancos = bu.BrancosPresidente;
                secao.PR_Nulos = bu.NulosPresidente;
                secao.PR_Total = bu.TotalApuradoPresidente;

                context.SecaoEleitoral.Add(secao);
                #endregion

                SalvarVotosEstaduais(bu.VotosDeputadosFederais, context, Cargos.DeputadoFederal, secao);
                SalvarVotosEstaduais(bu.VotosDeputadosEstaduais, context, Cargos.DeputadoEstadual, secao);
                SalvarVotosEstaduais(bu.VotosSenador, context, Cargos.Senador, secao);
                SalvarVotosEstaduais(bu.VotosGovernador, context, Cargos.Governador, secao);
                SalvarVotosFederais(bu.VotosPresidente, context, secao);

                context.SaveChanges();
            }

        }



        public static void SalvarVotosEstaduais(List<VotoEstadual> listaVotos, TSEContext context, Cargos cargo, SecaoEleitoral secao)
        {
            foreach (var votobu in listaVotos)
            {
                var candidato = context.Candidato.Find(cargo, votobu.NumeroCandidato, secao.Municipio.UFSigla);
                if (candidato == null)
                {
                    var partido = context.Partido.Find(votobu.NumeroPartido);
                    if (partido == null)
                    {
                        partido = new Partido()
                        {
                            Nome = "Partido " + votobu.NumeroPartido.ToString(),
                            Numero = votobu.NumeroPartido
                        };

                        context.Partido.Add(partido);
                    }

                    candidato = new Candidato()
                    {
                        Nome = votobu.VotoLegenda ? "Legenda" : votobu.NomeCandidato,
                        NumeroCandidato = votobu.NumeroCandidato,
                        Cargo = cargo,
                        UF = secao.Municipio.UF,
                        UFSigla = secao.Municipio.UFSigla,
                    };

                    context.Candidato.Add(candidato);
                }

                var voto = new DetalheVoto()
                {
                    SecaoEleitoral = secao,
                    SecaoEleitoralMunicipioCodigo = secao.MunicipioCodigo,
                    SecaoEleitoralCodigoZonaEleitoral = secao.CodigoZonaEleitoral,
                    SecaoEleitoralCodigoSecao = secao.CodigoSecao,
                    Cargo = cargo,
                    NumeroCandidato = candidato.NumeroCandidato,
                    QtdVotos = votobu.QtdVotos,
                    VotoLegenda = votobu.VotoLegenda,
                };

                context.DetalheVoto.Add(voto);
            }

        }

        public static void SalvarVotosFederais(List<VotoFederal> listaVotos, TSEContext context, SecaoEleitoral secao)
        {
            var cargo = Cargos.Presidente;
            var uf = context.UnidadeFederativa.Find("BR");
            if (uf == null)
            {
                uf = new UnidadeFederativa() { Sigla = "BR", Nome = "FED - BRASIL", };
                context.UnidadeFederativa.Add(uf);
            }

            foreach (var votobu in listaVotos)
            {
                var candidato = context.Candidato.Find(cargo, votobu.Numero.ToInt(), uf.Sigla);
                if (candidato == null)
                {
                    var partido = context.Partido.Find(votobu.Numero);
                    if (partido == null)
                    {
                        partido = new Partido()
                        {
                            Nome = "Partido " + votobu.Numero.ToString(),
                            Numero = votobu.Numero
                        };

                        context.Partido.Add(partido);
                    }

                    candidato = new Candidato()
                    {
                        Nome = votobu.NomeCandidato,
                        NumeroCandidato = votobu.Numero,
                        Cargo = cargo,
                        UF = uf,
                        UFSigla = uf.Sigla,
                    };

                    context.Candidato.Add(candidato);
                }

                var voto = new DetalheVoto()
                {
                    SecaoEleitoral = secao,
                    SecaoEleitoralMunicipioCodigo = secao.MunicipioCodigo,
                    SecaoEleitoralCodigoZonaEleitoral = secao.CodigoZonaEleitoral,
                    SecaoEleitoralCodigoSecao = secao.CodigoSecao,
                    Cargo = cargo,
                    NumeroCandidato = candidato.NumeroCandidato,
                    QtdVotos = votobu.QtdVotos,
                };

                context.DetalheVoto.Add(voto);
            }

        }

        public static BoletimUrna ProcessarBoletimUrna(string arquivoBoletim)
        {
            BoletimUrna BU = new BoletimUrna();
            int linhaAtual = 0;
            bool encontrouCabecalho = false;
            bool eleitoresAptosLido = false;
            bool comparecimentoLido = false;
            bool eleitoresFaltososLido = false;
            bool habilitadosPorAnodeNascimentoLido = false;
            bool encontrouResumoDaCorrespondencia = false;
            bool encontrouBlocoDeputadoFederal = false;
            bool encontrouBlocoDeputadoEstadual = false;
            bool encontrouBlocoSenador = false;
            bool encontrouBlocoGovernador = false;
            bool encontrouBlocoPresidente = false;
            byte ultimoNumeroPartido = 0;
            string nomeTemporario = string.Empty;
            bool SecaoJuntaTurma = false;

            // Ler o arquivo do boletim linha-a-linha
            var linhasBU = File.ReadLines(arquivoBoletim, System.Text.Encoding.UTF7);
            foreach (var linhaBU in linhasBU)
            {
                linhaAtual++;

                // Linhas em branco não precisam ser processadas
                if (string.IsNullOrWhiteSpace(linhaBU)
                    || linhaBU == "--------------------------------------"
                    || linhaBU == "======================================"
                    )
                    continue;

                if (!encontrouCabecalho)
                {
                    if (linhaBU.Trim().ToLower().Contains("Tribunal Regional Eleitoral".ToLower()))
                    {
                        var UF = linhaBU.Substring(linhaBU.IndexOf("[") + 1, 2);
                        BU.UF = UF;
                    }

                    if (linhaBU.Trim().ToLower().Contains("Boletim de Urna".ToLower()))
                    {
                        encontrouCabecalho = true;
                    }
                }
                else if (string.IsNullOrWhiteSpace(BU.NomeEleicao))
                {
                    if (linhaBU.Trim().ToLower().Contains("eleições"))
                    {
                        BU.NomeEleicao = linhaBU.Trim();
                    }
                }
                else if (BU.TurnoEleicao == byte.MinValue)
                {
                    if (linhaBU.Trim().ToLower().Contains("turno"))
                    {
                        BU.TurnoEleicao = Convert.ToByte(linhaBU.Trim().Substring(0, 1));
                    }
                }
                else if (BU.DataEleicao == DateTime.MinValue)
                {
                    var tmpData = linhaBU.Trim().Replace("(", "").Replace(")", "");
                    if (DateTime.TryParseExact(tmpData, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.DataEleicao = tmpData2;
                    }

                }
                else if (string.IsNullOrWhiteSpace(BU.CodigoMunicipio))
                {
                    BU.CodigoMunicipio = DefinirValorDePar(linhaBU, "Município");
                }
                else if (string.IsNullOrWhiteSpace(BU.NomeMunicipio))
                {
                    // A próxima linha após o código do município é sempre o nome do município
                    if (!string.IsNullOrWhiteSpace(linhaBU))
                    {
                        BU.NomeMunicipio = linhaBU.Trim();
                    }
                }
                else if (string.IsNullOrWhiteSpace(BU.ZonaEleitoral))
                {
                    BU.ZonaEleitoral = DefinirValorDePar(linhaBU, "Zona Eleitoral");
                }
                else if (string.IsNullOrWhiteSpace(BU.LocalVotacao))
                {
                    if (linhaBU.ToLower().Contains("Seção Eleitoral".ToLower()))
                    {
                        // Esta é uma urna que não tem Local de Votação. Continuar a partir da Seção Eleitoral
                        SecaoJuntaTurma = true;
                        BU.SecaoEleitoral = DefinirValorDePar(linhaBU, "Seção Eleitoral");
                        BU.LocalVotacao = "0000";
                    }
                    else
                    {
                        BU.LocalVotacao = DefinirValorDePar(linhaBU, "Local de Votação");
                    }
                }
                else if (string.IsNullOrWhiteSpace(BU.SecaoEleitoral))
                {
                    BU.SecaoEleitoral = DefinirValorDePar(linhaBU, "Seção Eleitoral");
                }
                else if (!eleitoresAptosLido && !SecaoJuntaTurma)
                {
                    if (linhaBU.ToLower().Contains("Eleitores aptos".ToLower()))
                    {
                        eleitoresAptosLido = true;
                        BU.EleitoresAptos = short.Parse(ObterValorDePar(linhaBU, "Eleitores aptos"));
                    }
                }
                else if (!comparecimentoLido && !SecaoJuntaTurma)
                {
                    if (linhaBU.ToLower().Contains("Comparecimento".ToLower()))
                    {
                        comparecimentoLido = true;
                        BU.Comparecimento = short.Parse(ObterValorDePar(linhaBU, "Comparecimento"));
                    }
                }
                else if (!eleitoresFaltososLido && !SecaoJuntaTurma)
                {
                    if (linhaBU.ToLower().Contains("Eleitores faltosos".ToLower()))
                    {
                        eleitoresFaltososLido = true;
                        BU.EleitoresFaltosos = short.Parse(ObterValorDePar(linhaBU, "Eleitores faltosos"));
                    }
                }
                else if (!habilitadosPorAnodeNascimentoLido && !SecaoJuntaTurma)
                {
                    if (linhaBU.ToLower().Contains("Código identificação UE".ToLower()))
                    {
                        // Habilitados por ano de nascimento nem sempre está presente.
                        BU.CodigoIdentificacaoUrnaEletronica = DefinirValorDePar(linhaBU, "Código identificação UE");
                    }

                    if (linhaBU.ToLower().Contains("Habilitados por ano nascimento".ToLower()))
                    {
                        habilitadosPorAnodeNascimentoLido = true;
                        BU.HabilitadosPorAnoNascimento = short.Parse(ObterValorDePar(linhaBU, "Habilitados por ano nascimento"));
                    }
                }
                else if (string.IsNullOrWhiteSpace(BU.CodigoIdentificacaoUrnaEletronica))
                {
                    BU.CodigoIdentificacaoUrnaEletronica = DefinirValorDePar(linhaBU, "Código identificação UE");
                }
                else if (BU.AberturaUrnaEletronica == DateTime.MinValue && !SecaoJuntaTurma)
                {
                    var tmpData = ObterValorDePar(linhaBU, "Data de abertura da UE");
                    if (DateTime.TryParseExact(tmpData, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.AberturaUrnaEletronica = tmpData2;
                    }

                }
                else if (BU.AberturaUrnaEletronica.Hour == 0 && BU.AberturaUrnaEletronica.Minute == 0 && BU.AberturaUrnaEletronica.Second == 0 && !SecaoJuntaTurma)
                {
                    // Já tem a data, falta só a hora
                    var tmpData = ObterValorDePar(linhaBU, "Horário de abertura");
                    var tmpDataHora = BU.AberturaUrnaEletronica.ToString("d/M/yyyy") + " " + tmpData;
                    if (DateTime.TryParseExact(tmpDataHora, "d/M/yyyy H:m:s", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.AberturaUrnaEletronica = tmpData2;
                    }

                }
                else if (BU.FechamentoUrnaEletronica == DateTime.MinValue && !SecaoJuntaTurma)
                {
                    var tmpData = ObterValorDePar(linhaBU, "Data de fechamento da UE");
                    if (DateTime.TryParseExact(tmpData, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }

                }
                else if (BU.FechamentoUrnaEletronica.Hour == 0 && BU.FechamentoUrnaEletronica.Minute == 0 && BU.FechamentoUrnaEletronica.Second == 0 && !SecaoJuntaTurma)
                {
                    // Já tem a data, falta só a hora
                    var tmpData = ObterValorDePar(linhaBU, "Horário de fechamento");
                    var tmpDataHora = BU.FechamentoUrnaEletronica.ToString("d/M/yyyy") + " " + tmpData;
                    if (DateTime.TryParseExact(tmpDataHora, "d/M/yyyy H:m:s", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }
                }
                else if (BU.FechamentoUrnaEletronica == DateTime.MinValue && SecaoJuntaTurma)
                {
                    var tmpData = ObterValorDePar(linhaBU, "Data da emissão");
                    if (DateTime.TryParseExact(tmpData, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }

                }
                else if (BU.FechamentoUrnaEletronica.Hour == 0 && BU.FechamentoUrnaEletronica.Minute == 0 && BU.FechamentoUrnaEletronica.Second == 0 && SecaoJuntaTurma)
                {
                    // Já tem a data, falta só a hora
                    var tmpData = ObterValorDePar(linhaBU, "Hora da emissão");
                    var tmpDataHora = BU.FechamentoUrnaEletronica.ToString("d/M/yyyy") + " " + tmpData;
                    if (DateTime.TryParseExact(tmpDataHora, "d/M/yyyy H:m:s", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }
                    BU.AberturaUrnaEletronica = BU.FechamentoUrnaEletronica;
                }
                else if (!encontrouResumoDaCorrespondencia)
                {
                    if (linhaBU.ToLower().Contains("RESUMO DA CORRESPONDÊNCIA".ToLower()))
                    {
                        encontrouResumoDaCorrespondencia = true;
                    }
                }
                else if (encontrouResumoDaCorrespondencia && string.IsNullOrWhiteSpace(BU.ResumoDaCorrespondencia))
                {
                    // Essa é a próxima linha depois do Resumo da Correspondência
                    BU.ResumoDaCorrespondencia = linhaBU.Replace("\u0013", "").Trim();
                }
                else if (string.IsNullOrWhiteSpace(BU.CodigoVerificador))
                {
                    BU.CodigoVerificador = DefinirValorDePar(linhaBU, "\u0015Código Verificador:");
                }
                else if (!encontrouBlocoDeputadoFederal)
                {
                    if (linhaBU.ToLower().Contains("DEPUTADO FEDERAL".ToLower()))
                    {
                        encontrouBlocoDeputadoFederal = true;
                    }
                }
                else if (encontrouBlocoDeputadoFederal && !encontrouBlocoDeputadoEstadual)
                {
                    // Ainda estamos no bloco de deputado federal
                    if (linhaBU.ToLower().Contains("Nome do candidato".ToLower())
                        || linhaBU.ToLower().Contains("Total do partido".ToLower())
                        || linhaBU.ToLower().Contains("Código Verificador".ToLower())
                        || linhaBU.ToLower().Contains("Não há votos nominais".ToLower())
                        )
                    {
                        // Ignorar cabeçalho
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Partido:".ToLower()))
                    {
                        // Iniciando um Partido novo
                        var numeroPartido = linhaBU.Substring("Partido:".Length, 4).Trim();
                        ultimoNumeroPartido = byte.Parse(numeroPartido);
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("DEPUTADO ESTADUAL".ToLower()) 
                        || linhaBU.ToLower().Contains("DEPUTADO DISTRITAL".ToLower()))
                    {
                        encontrouBlocoDeputadoEstadual = true;
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Eleitores Aptos".ToLower()))
                    {
                        BU.EleitoresAptosDeputadoFederal = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos Nominais".ToLower()))
                    {
                        BU.VotosNominaisDeputadoFederal = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos de Legenda".ToLower()))
                    {
                        BU.VotosLegendaDeputadoFederal = short.Parse(ObterValorDePar(linhaBU, "Total de votos de Legenda"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Brancos".ToLower()))
                    {
                        BU.BrancosDeputadoFederal = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Nulos".ToLower()))
                    {
                        BU.NulosDeputadoFederal = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total Apurado".ToLower()))
                    {
                        BU.TotalApuradoDeputadoFederal = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Votos de legenda".ToLower()))
                    {
                        // Terminou o partido atual. Vamos contabilizar os votos de legenda
                        var votoLegenda = new VotoEstadual();
                        votoLegenda.VotoLegenda = true;
                        var qtdVotosLegenda = ObterValorDePar(linhaBU, "    Votos de legenda");
                        votoLegenda.QtdVotos = qtdVotosLegenda.ToShort();
                        votoLegenda.NumeroPartido = ultimoNumeroPartido;
                        votoLegenda.NumeroCandidato = ultimoNumeroPartido;
                        BU.VotosDeputadosFederais.Add(votoLegenda);
                        continue;
                    }

                    // A partir daqui, tudo indica que é uma linha de candidato
                    if (!int.TryParse(linhaBU.Substring(27, 5), out int tmpNumCand))
                    {
                        // É uma linha de voto dupla (pois o lugar onde deveria ter o número candidato não tem)
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                            nomeTemporario += linhaBU.Substring(0, finaltexto).Trim();
                        else
                            nomeTemporario += linhaBU.Trim();

                        continue;
                    }

                    string nomeCandidato = string.Empty;
                    string numeroCandidato = string.Empty;
                    string qtdVotos = string.Empty;

                    if (!string.IsNullOrWhiteSpace(nomeTemporario))
                    {
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                        {
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;

                        }
                        else
                        {
                            finaltexto = linhaBU.IndexOf(">");
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;
                        }

                        nomeCandidato = nomeTemporario;
                        nomeTemporario = string.Empty;
                    }
                    else
                    {
                        nomeCandidato = linhaBU.Substring(0, 27).Trim();
                    }
                    numeroCandidato = linhaBU.Substring(27, 5).Trim();
                    qtdVotos = linhaBU.Substring(32).Trim();

                    // Se passou por todas essas condições, então é porque essa linha é um candidato.
                    VotoEstadual voto = new VotoEstadual();
                    voto.NomeCandidato = nomeCandidato;
                    voto.NumeroCandidato = int.Parse(numeroCandidato);
                    voto.NumeroPartido = byte.Parse(numeroCandidato.Substring(0, 2));
                    voto.QtdVotos = qtdVotos.ToShort();
                    voto.VotoLegenda = false;
                    BU.VotosDeputadosFederais.Add(voto);
                }
                else if (encontrouBlocoDeputadoEstadual && !encontrouBlocoSenador)
                {
                    // Ainda estamos no bloco de deputado estadual
                    if (linhaBU.ToLower().Contains("Nome do candidato".ToLower())
                        || linhaBU.ToLower().Contains("Total do partido".ToLower())
                        || linhaBU.ToLower().Contains("Código Verificador".ToLower())
                        || linhaBU.ToLower().Contains("Não há votos nominais".ToLower())
                        )
                    {
                        // Ignorar cabeçalho
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Partido:".ToLower()))
                    {
                        // Iniciando um Partido novo
                        var numeroPartido = linhaBU.Substring("Partido:".Length, 4).Trim();
                        ultimoNumeroPartido = byte.Parse(numeroPartido);
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("---------SENADOR-------".ToLower()))
                    {
                        encontrouBlocoSenador = true;
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Eleitores Aptos".ToLower()))
                    {
                        BU.EleitoresAptosDeputadoEstadual = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos Nominais".ToLower()))
                    {
                        BU.VotosNominaisDeputadoEstadual = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos de Legenda".ToLower()))
                    {
                        BU.VotosLegendaDeputadoEstadual = short.Parse(ObterValorDePar(linhaBU, "Total de votos de Legenda"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Brancos".ToLower()))
                    {
                        BU.BrancosDeputadoEstadual = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Nulos".ToLower()))
                    {
                        BU.NulosDeputadoEstadual = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total Apurado".ToLower()))
                    {
                        BU.TotalApuradoDeputadoEstadual = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Votos de legenda".ToLower()))
                    {
                        // Terminou o partido atual. Vamos contabilizar os votos de legenda
                        var votoLegenda = new VotoEstadual();
                        votoLegenda.VotoLegenda = true;
                        var qtdVotosLegenda = ObterValorDePar(linhaBU, "    Votos de legenda");
                        votoLegenda.QtdVotos = qtdVotosLegenda.ToShort();
                        votoLegenda.NumeroPartido = ultimoNumeroPartido;
                        votoLegenda.NumeroCandidato = ultimoNumeroPartido;
                        BU.VotosDeputadosEstaduais.Add(votoLegenda);
                        continue;
                    }

                    // A partir daqui, tudo indica que é uma linha de candidato
                    if (!int.TryParse(linhaBU.Substring(27, 5), out int tmpNumCand))
                    {
                        // É uma linha de voto dupla (pois o lugar onde deveria ter o número candidato não tem)
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                            nomeTemporario += linhaBU.Substring(0, finaltexto).Trim();
                        else
                            nomeTemporario += linhaBU.Trim();

                        continue;
                    }

                    string nomeCandidato = string.Empty;
                    string numeroCandidato = string.Empty;
                    string qtdVotos = string.Empty;

                    if (!string.IsNullOrWhiteSpace(nomeTemporario))
                    {
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                        {
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;

                        }
                        else
                        {
                            finaltexto = linhaBU.IndexOf(">");
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;
                        }

                        nomeCandidato = nomeTemporario;
                        nomeTemporario = string.Empty;
                    }
                    else
                    {
                        nomeCandidato = linhaBU.Substring(0, 27).Trim();
                    }
                    numeroCandidato = linhaBU.Substring(27, 5).Trim();
                    qtdVotos = linhaBU.Substring(32).Trim();

                    // Se passou por todas essas condições, então é porque essa linha é um candidato.
                    VotoEstadual voto = new VotoEstadual();
                    voto.NomeCandidato = nomeCandidato;
                    voto.NumeroCandidato = int.Parse(numeroCandidato);
                    voto.NumeroPartido = byte.Parse(numeroCandidato.Substring(0, 2));
                    voto.QtdVotos = qtdVotos.ToShort();
                    voto.VotoLegenda = false;
                    BU.VotosDeputadosEstaduais.Add(voto);
                }
                else if (encontrouBlocoSenador && !encontrouBlocoGovernador)
                {
                    // Ainda estamos no bloco de Senador
                    if (linhaBU.ToLower().Contains("Nome do candidato".ToLower())
                        || linhaBU.ToLower().Contains("Código Verificador".ToLower())
                        )
                    {
                        // Ignorar cabeçalho
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("----GOVERNADOR----".ToLower()))
                    {
                        encontrouBlocoGovernador = true;
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Eleitores Aptos".ToLower()))
                    {
                        BU.EleitoresAptosSenador = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos Nominais".ToLower()))
                    {
                        BU.VotosNominaisSenador = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Brancos".ToLower()))
                    {
                        BU.BrancosSenador = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Nulos".ToLower()))
                    {
                        BU.NulosSenador = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total Apurado".ToLower()))
                    {
                        BU.TotalApuradoSenador = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        continue;
                    }

                    // A partir daqui, tudo indica que é uma linha de candidato
                    if (!int.TryParse(linhaBU.Substring(27, 5), out int tmpNumCand))
                    {
                        // É uma linha de voto dupla (pois o lugar onde deveria ter o número candidato não tem)
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                            nomeTemporario += linhaBU.Substring(0, finaltexto).Trim();
                        else
                            nomeTemporario += linhaBU.Trim();

                        continue;
                    }

                    string nomeCandidato = string.Empty;
                    string numeroCandidato = string.Empty;
                    string qtdVotos = string.Empty;

                    if (!string.IsNullOrWhiteSpace(nomeTemporario))
                    {
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                        {
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;

                        }
                        else
                        {
                            finaltexto = linhaBU.IndexOf(">");
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;
                        }

                        nomeCandidato = nomeTemporario;
                        nomeTemporario = string.Empty;
                    }
                    else
                    {
                        nomeCandidato = linhaBU.Substring(0, 27).Trim();
                    }
                    numeroCandidato = linhaBU.Substring(27, 5).Trim();
                    qtdVotos = linhaBU.Substring(32).Trim();

                    // Se passou por todas essas condições, então é porque essa linha é um candidato.
                    VotoEstadual voto = new VotoEstadual();
                    voto.NomeCandidato = nomeCandidato;
                    voto.NumeroCandidato = int.Parse(numeroCandidato);
                    voto.NumeroPartido = byte.Parse(numeroCandidato.Substring(0, 2));
                    voto.QtdVotos = qtdVotos.ToShort();
                    voto.VotoLegenda = false;
                    BU.VotosSenador.Add(voto);
                }
                else if (encontrouBlocoGovernador && !encontrouBlocoPresidente)
                {
                    // Ainda estamos no bloco de Governador
                    if (linhaBU.ToLower().Contains("Nome do candidato".ToLower())
                        || linhaBU.ToLower().Contains("Código Verificador".ToLower())
                        || linhaBU.ToLower().Contains("Eleição Geral Federal".ToLower())
                        )
                    {
                        // Ignorar cabeçalho
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("--PRESIDENTE--".ToLower()))
                    {
                        encontrouBlocoPresidente = true;
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Eleitores Aptos".ToLower()))
                    {
                        BU.EleitoresAptosGovernador = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos Nominais".ToLower()))
                    {
                        BU.VotosNominaisGovernador = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Brancos".ToLower()))
                    {
                        BU.BrancosGovernador = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Nulos".ToLower()))
                    {
                        BU.NulosGovernador = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total Apurado".ToLower()))
                    {
                        BU.TotalApuradoGovernador = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        continue;
                    }

                    // A partir daqui, tudo indica que é uma linha de candidato
                    if (!int.TryParse(linhaBU.Substring(27, 5), out int tmpNumCand))
                    {
                        // É uma linha de voto dupla (pois o lugar onde deveria ter o número candidato não tem)
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                            nomeTemporario += linhaBU.Substring(0, finaltexto).Trim();
                        else
                            nomeTemporario += linhaBU.Trim();

                        continue;
                    }

                    string nomeCandidato = string.Empty;
                    string numeroCandidato = string.Empty;
                    string qtdVotos = string.Empty;

                    if (!string.IsNullOrWhiteSpace(nomeTemporario))
                    {
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                        {
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;

                        }
                        else
                        {
                            finaltexto = linhaBU.IndexOf(">");
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;
                        }

                        nomeCandidato = nomeTemporario;
                        nomeTemporario = string.Empty;
                    }
                    else
                    {
                        nomeCandidato = linhaBU.Substring(0, 27).Trim();
                    }
                    numeroCandidato = linhaBU.Substring(27, 5).Trim();
                    qtdVotos = linhaBU.Substring(32).Trim();

                    // Se passou por todas essas condições, então é porque essa linha é um candidato.
                    VotoEstadual voto = new VotoEstadual();
                    voto.NomeCandidato = nomeCandidato;
                    voto.NumeroCandidato = int.Parse(numeroCandidato);
                    voto.NumeroPartido = byte.Parse(numeroCandidato.Substring(0, 2));
                    voto.QtdVotos = qtdVotos.ToShort();
                    voto.VotoLegenda = false;
                    BU.VotosGovernador.Add(voto);
                }
                else if (encontrouBlocoPresidente)
                {
                    // Ainda estamos no bloco de Presidente
                    if (linhaBU.ToLower().Contains("Nome do candidato".ToLower()))
                    {
                        // Ignorar cabeçalho
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Código Verificador".ToLower()))
                    {
                        // Acabou a parte interessante do arquivo
                        break;
                    }

                    if (linhaBU.ToLower().Contains("Eleitores Aptos".ToLower()))
                    {
                        BU.EleitoresAptosPresidente = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total de votos Nominais".ToLower()))
                    {
                        BU.VotosNominaisPresidente = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Brancos".ToLower()))
                    {
                        BU.BrancosPresidente = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Nulos".ToLower()))
                    {
                        BU.NulosPresidente = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        continue;
                    }

                    if (linhaBU.ToLower().Contains("Total Apurado".ToLower()))
                    {
                        BU.TotalApuradoPresidente = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        continue;
                    }

                    // A partir daqui, tudo indica que é uma linha de candidato
                    if (!int.TryParse(linhaBU.Substring(27, 5), out int tmpNumCand))
                    {
                        // É uma linha de voto dupla (pois o lugar onde deveria ter o número candidato não tem)
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                            nomeTemporario += linhaBU.Substring(0, finaltexto).Trim();
                        else
                            nomeTemporario += linhaBU.Trim();

                        continue;
                    }

                    string nomeCandidato = string.Empty;
                    string numeroCandidato = string.Empty;
                    string qtdVotos = string.Empty;

                    if (!string.IsNullOrWhiteSpace(nomeTemporario))
                    {
                        var finaltexto = linhaBU.IndexOf("-");
                        if (finaltexto > -1)
                        {
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;

                        }
                        else
                        {
                            finaltexto = linhaBU.IndexOf(">");
                            var complemento = linhaBU.Substring(0, finaltexto).Trim();
                            if (!string.IsNullOrEmpty(complemento))
                                nomeTemporario += " " + complemento;
                        }

                        nomeCandidato = nomeTemporario;
                        nomeTemporario = string.Empty;
                    }
                    else
                    {
                        nomeCandidato = linhaBU.Substring(0, 27).Trim();
                    }
                    numeroCandidato = linhaBU.Substring(27, 5).Trim();
                    qtdVotos = linhaBU.Substring(32).Trim();

                    // Se passou por todas essas condições, então é porque essa linha é um candidato.
                    VotoFederal voto = new VotoFederal();
                    voto.NomeCandidato = nomeCandidato;
                    voto.Numero = byte.Parse(numeroCandidato);
                    voto.QtdVotos = qtdVotos.ToShort();
                    BU.VotosPresidente.Add(voto);
                }
            }

            return BU;
        }

        public static string ObterValorDePar(string parChaveValor, string chave)
        {
            string retorno = parChaveValor.Substring(chave.Length).Trim();

            return retorno;
        }

        public static string DefinirValorDePar(string parChaveValor, string chave)
        {
            if (parChaveValor.Trim().ToLower().Contains(chave.ToLower()))
            {
                return ObterValorDePar(parChaveValor, chave);
            }
            else
            {
                return null;
            }
        }

    }

    public class BoletimUrna
    {
        public BoletimUrna()
        {
            VotosDeputadosFederais = new List<VotoEstadual>();
            VotosDeputadosEstaduais = new List<VotoEstadual>();
            VotosSenador = new List<VotoEstadual>();
            VotosGovernador = new List<VotoEstadual>();
            VotosPresidente = new List<VotoFederal>();
        }

        public string UF { get; set; }
        public string NomeEleicao { get; set; }
        public byte TurnoEleicao { get; set; }
        public DateTime DataEleicao { get; set; }
        public string CodigoMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string ZonaEleitoral { get; set; }
        public string LocalVotacao { get; set; }
        public string SecaoEleitoral { get; set; }
        public short EleitoresAptos { get; set; }
        public short Comparecimento { get; set; }
        public short EleitoresFaltosos { get; set; }
        public short HabilitadosPorAnoNascimento { get; set; } // O que é isso?
        public string CodigoIdentificacaoUrnaEletronica { get; set; }
        public DateTime AberturaUrnaEletronica { get; set; }
        public DateTime FechamentoUrnaEletronica { get; set; }
        public string ResumoDaCorrespondencia { get; set; } // O que é isso?
        public string CodigoVerificador { get; set; }
        public List<VotoEstadual> VotosDeputadosFederais { get; set; }
        public short EleitoresAptosDeputadoFederal { get; set; }
        public short VotosNominaisDeputadoFederal { get; set; }
        public short VotosLegendaDeputadoFederal { get; set; }
        public short BrancosDeputadoFederal { get; set; }
        public short NulosDeputadoFederal { get; set; }
        public short TotalApuradoDeputadoFederal { get; set; }
        public List<VotoEstadual> VotosDeputadosEstaduais { get; set; }
        public short EleitoresAptosDeputadoEstadual { get; set; }
        public short VotosNominaisDeputadoEstadual { get; set; }
        public short VotosLegendaDeputadoEstadual { get; set; }
        public short BrancosDeputadoEstadual { get; set; }
        public short NulosDeputadoEstadual { get; set; }
        public short TotalApuradoDeputadoEstadual { get; set; }
        public List<VotoEstadual> VotosSenador { get; set; }
        public short EleitoresAptosSenador { get; set; }
        public short VotosNominaisSenador { get; set; }
        public short BrancosSenador { get; set; }
        public short NulosSenador { get; set; }
        public short TotalApuradoSenador { get; set; }
        public List<VotoEstadual> VotosGovernador { get; set; }
        public short EleitoresAptosGovernador { get; set; }
        public short VotosNominaisGovernador { get; set; }
        public short BrancosGovernador { get; set; }
        public short NulosGovernador { get; set; }
        public short TotalApuradoGovernador { get; set; }
        public List<VotoFederal> VotosPresidente { get; set; }
        public short EleitoresAptosPresidente { get; set; }
        public short VotosNominaisPresidente { get; set; }
        public short BrancosPresidente { get; set; }
        public short NulosPresidente { get; set; }
        public short TotalApuradoPresidente { get; set; }
    }

    public class VotoEstadual
    {
        public byte NumeroPartido { get; set; }
        public int NumeroCandidato { get; set; }
        public string NomeCandidato { get; set; }
        public short QtdVotos { get; set; }
        public bool VotoLegenda { get; set; }
    }

    public class VotoFederal
    {
        public byte Numero { get; set; }
        public string NomeCandidato { get; set; }
        public short QtdVotos { get; set; }

    }

    public class TSEWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            int itimeout = Convert.ToInt32(TimeSpan.FromSeconds(60).TotalMilliseconds);
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = itimeout;
            ((HttpWebRequest)w).ReadWriteTimeout = itimeout;
            return w;
        }
    }


}
