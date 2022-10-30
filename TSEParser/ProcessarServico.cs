using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TSEBU;
using TSEParser.CrawlerModels;

namespace TSEParser
{
    public class ProcessarServico
    {
        private string diretorioLocalDados { get; set; }
        private string urlTSE { get; set; }
        private bool compararIMGBUeBU { get; set; }
        private string connectionString { get; set; }

        /// <summary>
        /// Serviço que processa os arquivos de boletim de urna e salva no banco de dados
        /// </summary>
        /// <param name="diretorio">O diretório local que contém os arquivos de urna</param>
        /// <param name="url">A URL do TSE para baixar arquivos de urna seja caso necessário</param>
        public ProcessarServico(string diretorio, string url, bool _compararIMGBUeBU, string _connectionString)
        {
            diretorioLocalDados = diretorio;
            urlTSE = url;
            compararIMGBUeBU = _compararIMGBUeBU;
            connectionString = _connectionString;
        }

        public void ProcessarUnicaSecao(string UF, string CodMunicipio, string CodZonaEleitoral, string CodSecaoEleitoral)
        {
            string diretorioUF = diretorioLocalDados + UF;
            if (!Directory.Exists(diretorioUF))
                throw new Exception($"A UF informada ({UF}) não existe no diretório de dados.");

            if (!File.Exists(diretorioUF + @"\config.json"))
                throw new Exception($"O arquivo de configuração da UF informada ({UF}) não existe.");

            var jsonConfiguracaoUF = File.ReadAllText(diretorioUF + @"\config.json");

            UFConfig configuracaoUF;
            try
            {
                configuracaoUF = JsonSerializer.Deserialize<CrawlerModels.UFConfig>(jsonConfiguracaoUF);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao interpretar o JSON de configuração da UF " + UF, ex);
            }

            string diretorioMunicipio = diretorioUF + @"\" + CodMunicipio;
            if (!Directory.Exists(diretorioMunicipio))
                throw new Exception($"O diretório do município {CodMunicipio} não foi localizado.");

            var nomeUF = configuracaoUF.abr.Find(x => x.cd == UF).ds;
            var nomeMunicipio = configuracaoUF.abr.Find(x => x.cd == UF).mu.Find(x => x.cd == CodMunicipio).nm;

            string diretorioZona = diretorioMunicipio + @"\" + CodZonaEleitoral;
            if (!Directory.Exists(diretorioZona))
                throw new Exception($"O diretório da zona eleitoral {CodZonaEleitoral} do muncipio {CodMunicipio} ({nomeMunicipio}) não foi localizado.");

            string diretorioSecao = diretorioZona + @"\" + CodSecaoEleitoral;
            if (!Directory.Exists(diretorioSecao))
                throw new Exception($"O diretório da seção eleitoral {CodSecaoEleitoral} da zona eleitoral {CodZonaEleitoral} do muncipio {CodMunicipio} ({nomeMunicipio}) não foi localizado.");

            if (!File.Exists(diretorioSecao + @"\config.json"))
                throw new Exception($"O arquivo de configuração da Seção {CodSecaoEleitoral} zona {CodZonaEleitoral} muncipio {CodMunicipio} ({nomeMunicipio}) não foi localizado.");

            var jsonConfiguracaoSecao = File.ReadAllText(diretorioSecao + @"\config.json");

            CrawlerModels.BoletimUrna boletimUrna;
            try
            {
                boletimUrna = JsonSerializer.Deserialize<CrawlerModels.BoletimUrna>(jsonConfiguracaoSecao);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao interpretar o JSON de boletim de urna da seção " + CodSecaoEleitoral + ", zona " + CodZonaEleitoral + ", município " + CodMunicipio + " (" + nomeMunicipio + "), UF " + UF, ex);
            }

            foreach (var objHash in boletimUrna.hashes)
            {
                if (objHash.st != "Totalizado" && objHash.st != "Recebido")
                {
                    string mensagem = $"UF {UF} MUN {CodMunicipio} ZN {CodZonaEleitoral} SE {CodSecaoEleitoral} - Hash Situação: {objHash.st}. Será ignorado.";
                    Console.WriteLine(mensagem);
                    continue;
                }

                string diretorioHash = diretorioSecao + @"\" + objHash.hash;
                if (!Directory.Exists(diretorioHash))
                    throw new Exception($"O diretório hash {objHash.hash} da seção eleitoral {CodSecaoEleitoral} da zona eleitoral {CodZonaEleitoral} do muncipio {CodMunicipio} ({nomeMunicipio}) não foi localizado.");

                // Obter o arquivo imgbu e o bu
                var arquivo = objHash.nmarq.Find(x => x.Contains(".imgbu"));
                var arquivoBU = objHash.nmarq.Find(x => x.Contains(".bu"));

                if (!string.IsNullOrWhiteSpace(arquivoBU))
                {
                    if (!File.Exists(diretorioHash + @"\" + arquivoBU))
                        throw new Exception($"O arquivo {arquivoBU} não foi localizado no diretório hash {objHash.hash} da seção eleitoral {CodSecaoEleitoral} da zona eleitoral {CodZonaEleitoral} do muncipio {CodMunicipio} ({nomeMunicipio}).");

                    using (var servico = new BoletimUrnaServico())
                    {
                        BoletimUrna bu = null;
                        BoletimUrna bu2 = null;

                        if (!string.IsNullOrWhiteSpace(arquivo) && File.Exists(diretorioHash + @"\" + arquivo))
                            bu = servico.ProcessarBoletimUrna(diretorioHash + @"\" + arquivo);

                        EntidadeBoletimUrna ebu = null;
                        try
                        {
                            ebu = servico.DecodificarArquivoBU(diretorioHash + @"\" + arquivoBU);
                            var jsonBU = JsonSerializer.Serialize(ebu, new JsonSerializerOptions()
                            {
                                MaxDepth = 0,
                                IgnoreNullValues = true,
                                IgnoreReadOnlyProperties = true
                            });
                            bu2 = servico.ProcessarArquivoBU(ebu);
                            bu2.UF = UF;

                            if (bu != null)
                            {
                                var inconsistencias = servico.CompararBoletins(bu, bu2);

                                if (inconsistencias.Count > 0)
                                {
                                    Console.WriteLine($"UF {UF} MUN {CodMunicipio} ({nomeMunicipio}) ZN {CodZonaEleitoral} SE {CodSecaoEleitoral} - Arquivos IMGBU (A) e BU (B) não são iguais.\n" + inconsistencias.Join("\n") + "\n");
                                }
                            }
                        }
                        catch (Exception exbu)
                        {
                            Console.WriteLine($"UF {UF} MUN {CodMunicipio} ({nomeMunicipio}) ZN {CodZonaEleitoral} SE {CodSecaoEleitoral} - Arquivo BU está corrompido e não pode ser decodificado. {exbu.Message}");
                        }

                        using (var context = new TSEContext(connectionString))
                        {
                            // Buscar e excluir a Seção atual, se houver.
                            var votosSecao = context.VotosSecao.AsNoTracking().Where(x => x.SecaoEleitoralMunicipioCodigo == CodMunicipio.ToInt()
                                && x.SecaoEleitoralCodigoZonaEleitoral == CodZonaEleitoral.ToShort()
                                && x.SecaoEleitoralCodigoSecao == CodSecaoEleitoral.ToShort()).ToList();

                            context.VotosSecao.RemoveRange(votosSecao);

                            var secaoEleitoral = context.SecaoEleitoral.AsNoTracking().Where(x => x.MunicipioCodigo == CodMunicipio.ToInt()
                                && x.CodigoZonaEleitoral == CodZonaEleitoral.ToShort()
                                && x.CodigoSecao == CodSecaoEleitoral.ToShort()).ToList();
                            context.SecaoEleitoral.RemoveRange(secaoEleitoral);

                            context.SaveChanges();

                            // Matar objetos para liberar memória
                            votosSecao = null;
                            secaoEleitoral = null;

                            // Salvar o Boletim atual
                            SalvarBoletimUrna(bu2, context);
                            context.SaveChanges();

                            // Buscar os votos de todas as seções deste municipio
                            var votosSecoesMunicipio = context.VotosSecao.AsNoTracking().Where(x => x.SecaoEleitoralMunicipioCodigo == CodMunicipio.ToInt()).ToList();

                            // Montar uma lista com todos os votos deste município
                            var lstVotosMunicipio = new List<VotosMunicipio>();
                            foreach (var voto in votosSecoesMunicipio)
                            {
                                // Encontrar no voto do Municipio este candidato, e somar os votos
                                var votoMunicipio = lstVotosMunicipio.Find(x => x.NumeroCandidato == voto.NumeroCandidato
                                && x.VotoLegenda == voto.VotoLegenda && x.Cargo == voto.Cargo);

                                if (votoMunicipio == null)
                                {
                                    votoMunicipio = new VotosMunicipio();
                                    votoMunicipio.NumeroCandidato = voto.NumeroCandidato;
                                    votoMunicipio.VotoLegenda = voto.VotoLegenda;
                                    votoMunicipio.QtdVotos = voto.QtdVotos;
                                    votoMunicipio.MunicipioCodigo = CodMunicipio.ToInt();
                                    votoMunicipio.Cargo = voto.Cargo;

                                    lstVotosMunicipio.Add(votoMunicipio);
                                }
                                else
                                {
                                    votoMunicipio.QtdVotos += voto.QtdVotos;
                                }
                            }

                            // Excluir os votos desse município
                            var votosMunicipio = context.VotosMunicipio.AsNoTracking().Where(x => x.MunicipioCodigo == CodMunicipio.ToInt()).ToList();
                            context.VotosMunicipio.RemoveRange(votosMunicipio);
                            context.SaveChanges();

                            // Adicionar os novos votos atualizados
                            foreach (var votoMunicio in lstVotosMunicipio)
                            {
                                context.VotosMunicipio.Add(votoMunicio);
                            }
                            context.SaveChanges();
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Para esta seção, não há arquivo. UF {UF}, Município {CodMunicipio}, Zona {CodZonaEleitoral}, Seção {CodSecaoEleitoral}");
                }
            }
        }

        public void ProcessarUF(string UF)
        {
            string diretorioUF = diretorioLocalDados + UF;
            if (!Directory.Exists(diretorioUF))
                throw new Exception($"A UF informada ({UF}) não existe no diretório de dados.");

            if (!File.Exists(diretorioUF + @"\config.json"))
                throw new Exception($"O arquivo de configuração da UF informada ({UF}) não existe.");

            var jsonConfiguracaoUF = File.ReadAllText(diretorioUF + @"\config.json");

            UFConfig configuracaoUF;
            try
            {
                configuracaoUF = JsonSerializer.Deserialize<CrawlerModels.UFConfig>(jsonConfiguracaoUF);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao interpretar o JSON de configuração da UF " + UF, ex);
            }

            // Primeiro levantar a quantidade de seções a processar
            int qtdSecoes = 0;
            foreach (var abr in configuracaoUF.abr)
                foreach (var municipio in abr.mu)
                    foreach (var zonaEleitoral in municipio.zon)
                        foreach (var secao in zonaEleitoral.sec)
                            qtdSecoes++;

            // Agora processar as seções
            int secoesProcessadas = 0;
            foreach (var abr in configuracaoUF.abr)
            {
                int muAtual = 0;
                int muCont = abr.mu.Count();
                foreach (var municipio in abr.mu)
                {
                    using (var context = new TSEContext(connectionString))
                    {
                        var lstVotosMunicipio = new List<VotosMunicipio>();

                        muAtual++;
                        // Criar um diretório para o município
                        string diretorioMunicipio = diretorioUF + @"\" + municipio.cd;
                        if (!Directory.Exists(diretorioMunicipio))
                            throw new Exception($"O diretório do município {municipio.cd} não foi localizado.");

                        int zeAtual = 0;
                        int zeCont = municipio.zon.Count();
                        foreach (var zonaEleitoral in municipio.zon)
                        {
                            zeAtual++;

                            // Criar um diretório para a zona eleitoral
                            string diretorioZona = diretorioMunicipio + @"\" + zonaEleitoral.cd;
                            if (!Directory.Exists(diretorioZona))
                                throw new Exception($"O diretório da zona eleitoral {zonaEleitoral.cd} do muncipio {municipio.cd} não foi localizado.");

                            // Faz o processamento dos boletins em paralelo para agilizar o processo
                            var lstTrabalhos = new List<Trabalhador>();
                            var boletimUrnas = new ConcurrentBag<BoletimUrna>();
                            var votosLog = new ConcurrentBag<VotosLog>();
                            var votosRDV = new ConcurrentBag<VotosSecaoRDV>();
                            var mensagensLog = new ConcurrentBag<string>();
                            foreach (var secao in zonaEleitoral.sec)
                            {
                                var trabalho = new Trabalhador(secao, municipio, zonaEleitoral, UF, diretorioZona, urlTSE, diretorioLocalDados, compararIMGBUeBU);
                                lstTrabalhos.Add(trabalho);
                                secoesProcessadas++;
                            }

                            var processamentoParalelo = true;

                            if (processamentoParalelo)
                            {
                                Parallel.ForEach(lstTrabalhos, trabalhador =>
                                {
                                    Trabalhar(trabalhador, boletimUrnas, mensagensLog, abr.ds, votosLog, votosRDV);
                                });
                            }
                            else
                            {
                                foreach (var trabalhador in lstTrabalhos)
                                {
                                    Trabalhar(trabalhador, boletimUrnas, mensagensLog, abr.ds, votosLog, votosRDV);
                                }
                            }

                            // Gravar as mensagens geradas pelos trabalhadores no log (se houver)
                            foreach (var mensagem in mensagensLog)
                            {
                                string arquivoLog = diretorioLocalDados + "TSEParser.log";
                                File.AppendAllText(arquivoLog, mensagem);
                            }

                            // Atualizar votos do Municipio
                            foreach (var bu in boletimUrnas)
                            {
                                SomaVotosMunicipio(bu.VotosDeputadosFederais, lstVotosMunicipio, Cargos.DeputadoFederal, municipio.cd.ToInt());
                                SomaVotosMunicipio(bu.VotosDeputadosEstaduais, lstVotosMunicipio, Cargos.DeputadoEstadual, municipio.cd.ToInt());
                                SomaVotosMunicipio(bu.VotosSenador, lstVotosMunicipio, Cargos.Senador, municipio.cd.ToInt());
                                SomaVotosMunicipio(bu.VotosGovernador, lstVotosMunicipio, Cargos.Governador, municipio.cd.ToInt());
                                SomaVotosMunicipio(bu.VotosPresidente, lstVotosMunicipio, Cargos.Presidente, municipio.cd.ToInt());
                            }

                            // Tem todos os BUs processados. Agora é só sair salvando tudo
                            var percentualProgresso = (secoesProcessadas.ToDecimal() / qtdSecoes.ToDecimal()) * 100;
                            Console.WriteLine($"{percentualProgresso:N2}% - Municipio {muAtual}/{muCont}, Zona Eleitoral {zeAtual}/{zeCont}. Salvando no banco de dados...");
                            foreach (var bu in boletimUrnas)
                            {
                                try
                                {
                                    SalvarBoletimUrna(bu, context);
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }
                            }

                            foreach (var voto in votosLog)
                            {
                                context.VotosLog.Add(voto);
                            }

                            foreach (var voto in votosRDV)
                            {
                                context.VotosSecaoRDV.Add(voto);
                            }

                            // Salvar zona eleitoral
                            context.SaveChanges();
                        }

                        // Terminou de processar o Município. Salvando os votos consolidados
                        Console.WriteLine($"Salvando votos consolidados - Municipio {muAtual}/{muCont}...");
                        foreach (var votoMunicio in lstVotosMunicipio)
                        {
                            context.VotosMunicipio.Add(votoMunicio);
                        }

                        // Salvar município
                        context.SaveChanges();
                    }
                }
            }
        }

        public void Trabalhar(Trabalhador trabalhador, ConcurrentBag<BoletimUrna> boletimUrnas, ConcurrentBag<string> mensagensLog, string NomeUF, ConcurrentBag<VotosLog> votosLog, ConcurrentBag<VotosSecaoRDV> votosRDV)
        {
            var BUs = trabalhador.ProcessarSecao();
            foreach (var bu in BUs)
            {
                bu.NomeUF = NomeUF;
                boletimUrnas.Add(bu);
            }
            foreach (var votoLog in trabalhador.votosLog)
            {
                votosLog.Add(votoLog);
            }
            foreach (var votoRdv in trabalhador.votosRDV)
            {
                votosRDV.Add(votoRdv);
            }
            var msg = trabalhador.mensagemLog.ToString();
            if (!string.IsNullOrWhiteSpace(msg))
                mensagensLog.Add(msg);
        }

        public void SomaVotosMunicipio(List<Voto> lstVotos, List<VotosMunicipio> lstVotosMunicipio, Cargos cargo, int codigoMunicipio)
        {
            foreach (var voto in lstVotos)
            {
                // Encontrar no voto do Municipio este candidato, e somar os votos
                var votoMunicipio = lstVotosMunicipio.Find(x => x.NumeroCandidato == voto.NumeroCandidato
                && x.VotoLegenda == voto.VotoLegenda && x.Cargo == cargo);

                if (votoMunicipio == null)
                {
                    votoMunicipio = new VotosMunicipio();
                    votoMunicipio.NumeroCandidato = voto.NumeroCandidato;
                    votoMunicipio.VotoLegenda = voto.VotoLegenda;
                    votoMunicipio.QtdVotos = voto.QtdVotos;
                    votoMunicipio.MunicipioCodigo = codigoMunicipio;
                    votoMunicipio.Cargo = cargo;

                    lstVotosMunicipio.Add(votoMunicipio);
                }
                else
                {
                    votoMunicipio.QtdVotos += voto.QtdVotos;
                }
            }
        }

        public void SalvarBoletimUrna(BoletimUrna bu, TSEContext context)
        {
            var uf = context.UnidadeFederativa.Find(bu.UF);
            if (uf == null)
            {
                uf = new UnidadeFederativa() { Sigla = bu.UF, Nome = bu.NomeUF, };
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
            secao.Zeresima = bu.Zeresima;

            secao.DF_EleitoresAptos = bu.DF_EleitoresAptos;
            secao.DF_VotosNominais = bu.DF_VotosNominais;
            secao.DF_VotosLegenda = bu.DF_VotosLegenda;
            secao.DF_Brancos = bu.DF_Brancos;
            secao.DF_Nulos = bu.DF_Nulos;
            secao.DF_Total = bu.DF_Total;

            secao.DE_EleitoresAptos = bu.DE_EleitoresAptos;
            secao.DE_VotosNominais = bu.DE_VotosNominais;
            secao.DE_VotosLegenda = bu.DE_VotosLegenda;
            secao.DE_Brancos = bu.DE_Brancos;
            secao.DE_Nulos = bu.DE_Nulos;
            secao.DE_Total = bu.DE_Total;

            secao.SE_EleitoresAptos = bu.SE_EleitoresAptos;
            secao.SE_VotosNominais = bu.SE_VotosNominais;
            secao.SE_Brancos = bu.SE_Brancos;
            secao.SE_Nulos = bu.SE_Nulos;
            secao.SE_Total = bu.SE_Total;

            secao.GO_EleitoresAptos = bu.GO_EleitoresAptos;
            secao.GO_VotosNominais = bu.GO_VotosNominais;
            secao.GO_Brancos = bu.GO_Brancos;
            secao.GO_Nulos = bu.GO_Nulos;
            secao.GO_Total = bu.GO_Total;

            secao.PR_EleitoresAptos = bu.PR_EleitoresAptos;
            secao.PR_VotosNominais = bu.PR_VotosNominais;
            secao.PR_Brancos = bu.PR_Brancos;
            secao.PR_Nulos = bu.PR_Nulos;
            secao.PR_Total = bu.PR_Total;

            context.SecaoEleitoral.Add(secao);
            #endregion

            SalvarVotosEstaduais(bu.VotosDeputadosFederais, context, Cargos.DeputadoFederal, secao);
            SalvarVotosEstaduais(bu.VotosDeputadosEstaduais, context, Cargos.DeputadoEstadual, secao);
            SalvarVotosEstaduais(bu.VotosSenador, context, Cargos.Senador, secao);
            SalvarVotosEstaduais(bu.VotosGovernador, context, Cargos.Governador, secao);
            SalvarVotosFederais(bu.VotosPresidente, context, secao);
        }

        public void SalvarVotosEstaduais(List<Voto> listaVotos, TSEContext context, Cargos cargo, SecaoEleitoral secao)
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
                            Nome = votobu.NomePartido,
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

                var voto = new VotosSecao()
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

                context.VotosSecao.Add(voto);
            }
        }

        public void SalvarVotosFederais(List<Voto> listaVotos, TSEContext context, SecaoEleitoral secao)
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
                var candidato = context.Candidato.Find(cargo, votobu.NumeroCandidato, uf.Sigla);
                if (candidato == null)
                {
                    var partido = context.Partido.Find(votobu.NumeroCandidato.ToByte());
                    if (partido == null)
                    {
                        partido = new Partido()
                        {
                            Nome = votobu.NomePartido,
                            Numero = votobu.NumeroCandidato.ToByte(),
                        };

                        context.Partido.Add(partido);
                    }

                    candidato = new Candidato()
                    {
                        Nome = votobu.NomeCandidato,
                        NumeroCandidato = votobu.NumeroCandidato,
                        Cargo = cargo,
                        UF = uf,
                        UFSigla = uf.Sigla,
                    };

                    context.Candidato.Add(candidato);
                }

                var voto = new VotosSecao()
                {
                    SecaoEleitoral = secao,
                    SecaoEleitoralMunicipioCodigo = secao.MunicipioCodigo,
                    SecaoEleitoralCodigoZonaEleitoral = secao.CodigoZonaEleitoral,
                    SecaoEleitoralCodigoSecao = secao.CodigoSecao,
                    Cargo = cargo,
                    NumeroCandidato = candidato.NumeroCandidato,
                    QtdVotos = votobu.QtdVotos,
                };

                context.VotosSecao.Add(voto);
            }
        }
    }
}
