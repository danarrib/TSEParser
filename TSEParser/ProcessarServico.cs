using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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

        public void ProcessarUF(string UF)
        {
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
                        var mensagensLog = new ConcurrentBag<string>();
                        foreach (var secao in zonaEleitoral.sec)
                        {
                            var trabalho = new Trabalhador(secao, municipio, zonaEleitoral, UF, diretorioZona, urlTSE, diretorioLocalDados, compararIMGBUeBU);
                            lstTrabalhos.Add(trabalho);
                            secoesProcessadas++;
                        }
                        Parallel.ForEach(lstTrabalhos, trabalhador =>
                        {
                            var BUs = trabalhador.ProcessarSecao();
                            foreach (var bu in BUs)
                            {
                                bu.NomeUF = abr.ds;
                                boletimUrnas.Add(bu);
                            }
                            var msg = trabalhador.mensagemLog.ToString();
                            if (!string.IsNullOrWhiteSpace(msg))
                                mensagensLog.Add(msg);
                        });

                        // Gravar as mensagens geradas pelos trabalhadores no log (se houver)
                        foreach (var mensagem in mensagensLog)
                        {
                            string arquivoLog = diretorioLocalDados + "TSEParser.log";
                            File.AppendAllText(arquivoLog, mensagem);
                        }

                        // Tem todos os BUs processados. Agora é só sair salvando tudo
                        var percentualProgresso = (secoesProcessadas.ToDecimal() / qtdSecoes.ToDecimal()) * 100;
                        Console.WriteLine($"{percentualProgresso:N2}% - Municipio {muAtual}/{muCont}, Zona Eleitoral {zeAtual}/{zeCont}. Salvando no banco de dados...");
                        using (var context = new TSEContext(connectionString))
                        {
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

                            context.SaveChanges();
                        }
                    }
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
                    var partido = context.Partido.Find(votobu.NumeroCandidato);
                    if (partido == null)
                    {
                        partido = new Partido()
                        {
                            Nome = "Partido " + votobu.NumeroCandidato.ToString(),
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

    }
}
