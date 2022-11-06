using org.bn;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using TSEBU;

namespace TSEParser
{
    public class BoletimUrnaServico : IDisposable
    {
        public BoletimUrna ProcessarBoletimUrna(string arquivoBoletim)
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
            string ultimoNomePartido = string.Empty;
            string nomeTemporario = string.Empty;
            bool SecaoJuntaTurma = false;
            bool dataEmissaoEmVezDeAbertura = false;

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
                        // Esta é uma seção que não tem Local de Votação. Continuar a partir da Seção Eleitoral
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
                        habilitadosPorAnodeNascimentoLido = true;
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
                    // Se este boletim foi gerado pelo sistema recuperador de dados, não vai ter a data de abertura e fechamento
                    // Vai ter apenas a data da emissão.
                    if (linhaBU.ToLower().Contains("Data da emissão".ToLower()))
                    {
                        dataEmissaoEmVezDeAbertura = true;
                    }

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

                    if (dataEmissaoEmVezDeAbertura)
                    {
                        BU.FechamentoUrnaEletronica = BU.AberturaUrnaEletronica;
                    }
                }
                else if (BU.FechamentoUrnaEletronica == DateTime.MinValue && !SecaoJuntaTurma && !dataEmissaoEmVezDeAbertura)
                {
                    var tmpData = ObterValorDePar(linhaBU, "Data de fechamento da UE");
                    if (DateTime.TryParseExact(tmpData, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }
                }
                else if (BU.FechamentoUrnaEletronica.Hour == 0 && BU.FechamentoUrnaEletronica.Minute == 0 && BU.FechamentoUrnaEletronica.Second == 0 && !SecaoJuntaTurma && !dataEmissaoEmVezDeAbertura)
                {
                    // Já tem a data, falta só a hora
                    var tmpData = ObterValorDePar(linhaBU, "Horário de fechamento");
                    var tmpDataHora = BU.FechamentoUrnaEletronica.ToString("d/M/yyyy") + " " + tmpData;
                    if (DateTime.TryParseExact(tmpDataHora, "d/M/yyyy H:m:s", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }
                }
                else if (BU.FechamentoUrnaEletronica == DateTime.MinValue && SecaoJuntaTurma && !dataEmissaoEmVezDeAbertura)
                {
                    var tmpData = ObterValorDePar(linhaBU, "Data da emissão");
                    if (DateTime.TryParseExact(tmpData, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmpData2))
                    {
                        BU.FechamentoUrnaEletronica = tmpData2;
                    }
                }
                else if (BU.FechamentoUrnaEletronica.Hour == 0 && BU.FechamentoUrnaEletronica.Minute == 0 && BU.FechamentoUrnaEletronica.Second == 0 && SecaoJuntaTurma && !dataEmissaoEmVezDeAbertura)
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
                    if (linhaBU.ToLower().Contains("--DEPUTADO FEDERAL--".ToLower()))
                    {
                        encontrouBlocoDeputadoFederal = true;
                    }
                    if (linhaBU.ToLower().Contains("--DEPUTADO ESTADUAL--".ToLower())
                        || linhaBU.ToLower().Contains("--DEPUTADO DISTRITAL--".ToLower()))
                    {
                        encontrouBlocoDeputadoFederal = true;
                        encontrouBlocoDeputadoEstadual = true;
                    }
                    if (linhaBU.ToLower().Contains("--SENADOR--".ToLower()))
                    {
                        encontrouBlocoDeputadoFederal = true;
                        encontrouBlocoDeputadoEstadual = true;
                        encontrouBlocoSenador = true;
                    }
                    if (linhaBU.ToLower().Contains("--GOVERNADOR--".ToLower()))
                    {
                        encontrouBlocoDeputadoFederal = true;
                        encontrouBlocoDeputadoEstadual = true;
                        encontrouBlocoSenador = true;
                        encontrouBlocoGovernador = true;
                    }
                    if (linhaBU.ToLower().Contains("--PRESIDENTE--".ToLower()))
                    {
                        encontrouBlocoDeputadoFederal = true;
                        encontrouBlocoDeputadoEstadual = true;
                        encontrouBlocoSenador = true;
                        encontrouBlocoGovernador = true;
                        encontrouBlocoPresidente = true;
                    }
                }
                else if (encontrouBlocoDeputadoFederal && !encontrouBlocoDeputadoEstadual)
                {
                    encontrouBlocoDeputadoEstadual = ProcessarBlocoVotos(linhaBU, BU, Cargos.DeputadoFederal, ref ultimoNumeroPartido, ref ultimoNomePartido, ref nomeTemporario);
                }
                else if (encontrouBlocoDeputadoEstadual && !encontrouBlocoSenador)
                {
                    encontrouBlocoSenador = ProcessarBlocoVotos(linhaBU, BU, Cargos.DeputadoEstadual, ref ultimoNumeroPartido, ref ultimoNomePartido, ref nomeTemporario);
                }
                else if (encontrouBlocoSenador && !encontrouBlocoGovernador)
                {
                    encontrouBlocoGovernador = ProcessarBlocoVotos(linhaBU, BU, Cargos.Senador, ref ultimoNumeroPartido, ref ultimoNomePartido, ref nomeTemporario);
                }
                else if (encontrouBlocoGovernador && !encontrouBlocoPresidente)
                {
                    encontrouBlocoPresidente = ProcessarBlocoVotos(linhaBU, BU, Cargos.Governador, ref ultimoNumeroPartido, ref ultimoNomePartido, ref nomeTemporario);
                }
                else if (encontrouBlocoPresidente)
                {
                    var retorno = ProcessarBlocoVotos(linhaBU, BU, Cargos.Presidente, ref ultimoNumeroPartido, ref ultimoNomePartido, ref nomeTemporario);
                    if (retorno)
                        break;
                }
            }

            return BU;
        }

        private bool ProcessarBlocoVotos(string linhaBU, BoletimUrna BU, Cargos cargo, ref byte ultimoNumeroPartido, ref string ultimoNomePartido, ref string nomeTemporario)
        {
            bool encontrouProximoBloco = false;

            if (cargo == Cargos.Presidente && linhaBU.ToLower().Contains("Código Verificador".ToLower()))
            {
                // O arquivo acabou. Sair do processo agora.
                return true;
            }

            if (linhaBU.ToLower().Contains("Nome do candidato".ToLower())
                || linhaBU.ToLower().Contains("Total do partido".ToLower())
                || linhaBU.ToLower().Contains("Código Verificador".ToLower())
                || linhaBU.ToLower().Contains("Não há votos nominais".ToLower())
                || linhaBU.ToLower().Contains("Eleição Geral Federal".ToLower())
                )
            {
                // Ignorar cabeçalho
                return false;
            }

            if (linhaBU.ToLower().Contains("Partido:".ToLower()))
            {
                // Iniciando um Partido novo
                var numeroPartido = linhaBU.Substring("Partido:".Length, 4).Trim();
                ultimoNumeroPartido = byte.Parse(numeroPartido);
                var nomePartido = linhaBU.Substring(linhaBU.IndexOf("-") + 1).Trim();
                ultimoNomePartido = nomePartido;
                return false;
            }

            if (linhaBU.ToLower().Contains("----DEPUTADO ESTADUAL----".ToLower())
                || linhaBU.ToLower().Contains("----DEPUTADO DISTRITAL----".ToLower())
                || linhaBU.ToLower().Contains("----SENADOR----".ToLower())
                || linhaBU.ToLower().Contains("----GOVERNADOR----".ToLower())
                || linhaBU.ToLower().Contains("----PRESIDENTE----".ToLower())
                )
            {
                // Encontrou o próximo bloco de votos. Então deve finalizar este.
                return true;
            }

            if (linhaBU.ToLower().Contains("Eleitores Aptos".ToLower()))
            {
                switch (cargo)
                {
                    case Cargos.DeputadoFederal:
                        BU.DF_EleitoresAptos = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        break;
                    case Cargos.DeputadoEstadual:
                        BU.DE_EleitoresAptos = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        break;
                    case Cargos.Senador:
                        BU.SE_EleitoresAptos = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        break;
                    case Cargos.Governador:
                        BU.GO_EleitoresAptos = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        break;
                    case Cargos.Presidente:
                        BU.PR_EleitoresAptos = short.Parse(ObterValorDePar(linhaBU, "Eleitores Aptos"));
                        break;
                    default:
                        break;
                }

                return false;
            }

            if (linhaBU.ToLower().Contains("Total de votos Nominais".ToLower()))
            {
                switch (cargo)
                {
                    case Cargos.DeputadoFederal:
                        BU.DF_VotosNominais = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        break;
                    case Cargos.DeputadoEstadual:
                        BU.DE_VotosNominais = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        break;
                    case Cargos.Senador:
                        BU.SE_VotosNominais = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        break;
                    case Cargos.Governador:
                        BU.GO_VotosNominais = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        break;
                    case Cargos.Presidente:
                        BU.PR_VotosNominais = short.Parse(ObterValorDePar(linhaBU, "Total de votos Nominais"));
                        break;
                    default:
                        break;
                }

                return false;
            }

            if (linhaBU.ToLower().Contains("Total de votos de Legenda".ToLower()))
            {
                switch (cargo)
                {
                    case Cargos.DeputadoFederal:
                        BU.DF_VotosLegenda = short.Parse(ObterValorDePar(linhaBU, "Total de votos de Legenda"));
                        break;
                    case Cargos.DeputadoEstadual:
                        BU.DE_VotosLegenda = short.Parse(ObterValorDePar(linhaBU, "Total de votos de Legenda"));
                        break;
                    default:
                        break;
                }

                return false;
            }

            if (linhaBU.ToLower().Contains("Brancos".ToLower()))
            {
                switch (cargo)
                {
                    case Cargos.DeputadoFederal:
                        BU.DF_Brancos = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        break;
                    case Cargos.DeputadoEstadual:
                        BU.DE_Brancos = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        break;
                    case Cargos.Senador:
                        BU.SE_Brancos = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        break;
                    case Cargos.Governador:
                        BU.GO_Brancos = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        break;
                    case Cargos.Presidente:
                        BU.PR_Brancos = short.Parse(ObterValorDePar(linhaBU, "Brancos"));
                        break;
                    default:
                        break;
                }

                return false;
            }

            if (linhaBU.ToLower().Contains("Nulos".ToLower()))
            {
                switch (cargo)
                {
                    case Cargos.DeputadoFederal:
                        BU.DF_Nulos = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        break;
                    case Cargos.DeputadoEstadual:
                        BU.DE_Nulos = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        break;
                    case Cargos.Senador:
                        BU.SE_Nulos = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        break;
                    case Cargos.Governador:
                        BU.GO_Nulos = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        break;
                    case Cargos.Presidente:
                        BU.PR_Nulos = short.Parse(ObterValorDePar(linhaBU, "Nulos"));
                        break;
                    default:
                        break;
                }
                return false;
            }

            if (linhaBU.ToLower().Contains("Total Apurado".ToLower()))
            {
                switch (cargo)
                {
                    case Cargos.DeputadoFederal:
                        BU.DF_Total = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        break;
                    case Cargos.DeputadoEstadual:
                        BU.DE_Total = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        break;
                    case Cargos.Senador:
                        BU.SE_Total = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        break;
                    case Cargos.Governador:
                        BU.GO_Total = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        break;
                    case Cargos.Presidente:
                        BU.PR_Total = short.Parse(ObterValorDePar(linhaBU, "Total Apurado"));
                        break;
                    default:
                        break;
                }

                return false;
            }

            if (cargo == Cargos.DeputadoEstadual || cargo == Cargos.DeputadoFederal)
            {
                if (linhaBU.ToLower().Contains("Votos de legenda".ToLower()))
                {
                    // Terminou o partido atual. Vamos contabilizar os votos de legenda
                    var votoLegenda = new Voto();
                    votoLegenda.VotoLegenda = true;
                    var qtdVotosLegenda = ObterValorDePar(linhaBU, "    Votos de legenda");

                    if (qtdVotosLegenda.ToShort() == 0)
                    {
                        // A legenda não tem votos. Não precisamos incluir.
                        return false;
                    }

                    votoLegenda.QtdVotos = qtdVotosLegenda.ToShort();
                    votoLegenda.NumeroPartido = ultimoNumeroPartido;
                    votoLegenda.NumeroCandidato = ultimoNumeroPartido;
                    votoLegenda.NomePartido = ultimoNomePartido;

                    switch (cargo)
                    {
                        case Cargos.DeputadoFederal:
                            BU.VotosDeputadosFederais.Add(votoLegenda);
                            break;
                        case Cargos.DeputadoEstadual:
                            BU.VotosDeputadosEstaduais.Add(votoLegenda);
                            break;
                        default:
                            break;
                    }
                    return false;
                }
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

                return false;
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
            var voto = new Voto();
            voto.NomeCandidato = nomeCandidato;
            voto.NumeroCandidato = int.Parse(numeroCandidato);
            voto.NumeroPartido = byte.Parse(numeroCandidato.Substring(0, 2));
            voto.NomePartido = ultimoNomePartido;
            voto.QtdVotos = qtdVotos.ToShort();
            voto.VotoLegenda = false;

            switch (cargo)
            {
                case Cargos.DeputadoFederal:
                    BU.VotosDeputadosFederais.Add(voto);
                    break;
                case Cargos.DeputadoEstadual:
                    BU.VotosDeputadosEstaduais.Add(voto);
                    break;
                case Cargos.Senador:
                    BU.VotosSenador.Add(voto);
                    break;
                case Cargos.Governador:
                    BU.VotosGovernador.Add(voto);
                    break;
                case Cargos.Presidente:
                    BU.VotosPresidente.Add(voto);
                    break;
                default:
                    break;
            }

            return encontrouProximoBloco;
        }

        private string ObterValorDePar(string parChaveValor, string chave)
        {
            string retorno = parChaveValor.Substring(chave.Length).Trim();

            return retorno;
        }

        private string DefinirValorDePar(string parChaveValor, string chave)
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

        public EntidadeBoletimUrna DecodificarArquivoBU(string arquivoBU)
        {
            IDecoder decoder = CoderFactory.getInstance().newDecoder("DER");
            EntidadeBoletimUrna retorno = null;
            var arquivoBUBytes = File.ReadAllBytes(arquivoBU);
            using (MemoryStream memoryStream = new MemoryStream(arquivoBUBytes))
            {
                var envelope = decoder.decode<EntidadeEnvelopeGenerico>(memoryStream);

                using (MemoryStream ms2 = new MemoryStream(envelope.Conteudo))
                {
                    retorno = decoder.decode<EntidadeBoletimUrna>(ms2);
                }
            }
            /*
            var jsonBU = JsonSerializer.Serialize(retorno, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            });
            */
            return retorno;
        }

        public BoletimUrna ProcessarArquivoBU(EntidadeBoletimUrna EBU)
        {
            var BU = new BoletimUrna();

            //BU.UF
            //BU.NomeEleicao
            //BU.TurnoEleicao

            BU.CodigoMunicipio = EBU.IdentificacaoSecao.MunicipioZona.Municipio.Value.LeadZeros(5);
            BU.ZonaEleitoral = EBU.IdentificacaoSecao.MunicipioZona.Zona.Value.LeadZeros(4);
            BU.LocalVotacao = EBU.IdentificacaoSecao.Local.Value.LeadZeros(4);
            BU.SecaoEleitoral = EBU.IdentificacaoSecao.Secao.Value.LeadZeros(4);
            BU.CodigoIdentificacaoUrnaEletronica = EBU.Urna.CorrespondenciaResultado.Carga.NumeroInternoUrna.Value.LeadZeros(8);

            if (EBU.DadosSecaoSA.DadosSecao != null)
            {
                BU.AberturaUrnaEletronica = DateTime.ParseExact(EBU.DadosSecaoSA.DadosSecao.DataHoraAbertura.Value.Replace("T", ""), "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
                BU.FechamentoUrnaEletronica = DateTime.ParseExact(EBU.DadosSecaoSA.DadosSecao.DataHoraEncerramento.Value.Replace("T", ""), "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            else
            {
                BU.AberturaUrnaEletronica = DateTime.ParseExact(EBU.Cabecalho.DataGeracao.Value.Replace("T", ""), "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
                BU.FechamentoUrnaEletronica = BU.AberturaUrnaEletronica;

            }
            if (EBU.QtdEleitoresLibCodigo != null)
                BU.HabilitadosPorAnoNascimento = EBU.QtdEleitoresLibCodigo.Value.ToShort();

            // Obter os votos para deputado federal
            foreach (var Resultado in EBU.ResultadosVotacaoPorEleicao)
            {
                foreach (var resultadoVotacao in Resultado.ResultadosVotacao)
                {
                    foreach (var totais in resultadoVotacao.TotaisVotosCargo)
                    {
                        var listaVotos = ProcessarVotosBU(totais.VotosVotaveis, out short votosBrancos, out short votosNulos, out short votosLegenda, out short votosNominais);
                        short aptos = 0;
                        if (Resultado.QtdEleitoresAptos != null)
                            aptos = Resultado.QtdEleitoresAptos.Value.ToShort();

                        if (resultadoVotacao.TipoCargo.Value == TipoCargoConsulta.EnumType.proporcional
                            && totais.CodigoCargo.CargoConstitucional.Value == CargoConstitucional.EnumType.deputadoFederal)
                        {
                            BU.DF_Brancos = votosBrancos;
                            BU.DF_Nulos = votosNulos;
                            BU.DF_EleitoresAptos = aptos;
                            BU.DF_VotosLegenda = votosLegenda;
                            BU.DF_VotosNominais = votosNominais;
                            BU.DF_Total = (votosNominais + votosLegenda + votosBrancos + votosNulos).ToShort();
                            BU.VotosDeputadosFederais = listaVotos;
                        }
                        else if (resultadoVotacao.TipoCargo.Value == TipoCargoConsulta.EnumType.proporcional
                            && (totais.CodigoCargo.CargoConstitucional.Value == CargoConstitucional.EnumType.deputadoEstadual
                                || totais.CodigoCargo.CargoConstitucional.Value == CargoConstitucional.EnumType.deputadoDistrital)
                            )
                        {
                            BU.DE_Brancos = votosBrancos;
                            BU.DE_Nulos = votosNulos;
                            BU.DE_EleitoresAptos = aptos;
                            BU.DE_VotosLegenda = votosLegenda;
                            BU.DE_VotosNominais = votosNominais;
                            BU.DE_Total = (votosNominais + votosLegenda + votosBrancos + votosNulos).ToShort();
                            BU.VotosDeputadosEstaduais = listaVotos;
                        }
                        else if (resultadoVotacao.TipoCargo.Value == TipoCargoConsulta.EnumType.majoritario
                            && totais.CodigoCargo.CargoConstitucional.Value == CargoConstitucional.EnumType.senador)
                        {
                            BU.SE_Brancos = votosBrancos;
                            BU.SE_Nulos = votosNulos;
                            BU.SE_EleitoresAptos = aptos;
                            BU.SE_VotosNominais = votosNominais;
                            BU.SE_Total = (votosNominais + votosLegenda + votosBrancos + votosNulos).ToShort();
                            BU.VotosSenador = listaVotos;
                        }
                        else if (resultadoVotacao.TipoCargo.Value == TipoCargoConsulta.EnumType.majoritario
                            && totais.CodigoCargo.CargoConstitucional.Value == CargoConstitucional.EnumType.governador)
                        {
                            BU.GO_Brancos = votosBrancos;
                            BU.GO_Nulos = votosNulos;
                            BU.GO_EleitoresAptos = aptos;
                            BU.GO_VotosNominais = votosNominais;
                            BU.GO_Total = (votosNominais + votosLegenda + votosBrancos + votosNulos).ToShort();
                            BU.VotosGovernador = listaVotos;
                        }
                        else if (resultadoVotacao.TipoCargo.Value == TipoCargoConsulta.EnumType.majoritario
                            && totais.CodigoCargo.CargoConstitucional.Value == CargoConstitucional.EnumType.presidente)
                        {
                            BU.PR_Brancos = votosBrancos;
                            BU.PR_Nulos = votosNulos;
                            BU.PR_EleitoresAptos = aptos;
                            BU.PR_VotosNominais = votosNominais;
                            BU.PR_Total = (votosNominais + votosLegenda + votosBrancos + votosNulos).ToShort();
                            BU.VotosPresidente = listaVotos;

                            // No bloco de presidente, também preencher EleitoresAptos, Comparecimento e EleitoresFaltosos
                            short comparecimento = 0;
                            if (resultadoVotacao.QtdComparecimento != null)
                                comparecimento = resultadoVotacao.QtdComparecimento.Value.ToShort();

                            BU.EleitoresAptos = aptos;
                            BU.Comparecimento = comparecimento;
                            BU.EleitoresFaltosos = (BU.PR_EleitoresAptos - comparecimento).ToShort();
                        }
                    }
                }
            }

            return BU;
        }

        public List<Voto> ProcessarVotosBU(ICollection<TotalVotosVotavel> lstVotos, out short votosBrancos, out short votosNulos, out short votosLegenda, out short votosNominais)
        {
            List<Voto> votos = new List<Voto>();

            votosBrancos = 0;
            votosNulos = 0;
            votosLegenda = 0;
            votosNominais = 0;

            foreach (var Evoto in lstVotos)
            {
                if (Evoto.TipoVoto.Value == TipoVoto.EnumType.nominal)
                {
                    var voto = new Voto();
                    voto.NomeCandidato = $"Candidato {Evoto.IdentificacaoVotavel.Codigo.Value}";
                    voto.NumeroCandidato = Evoto.IdentificacaoVotavel.Codigo.Value;
                    voto.NumeroPartido = Evoto.IdentificacaoVotavel.Partido.Value.ToByte();
                    voto.QtdVotos = Evoto.QuantidadeVotos.Value.ToShort();
                    votosNominais += voto.QtdVotos;
                    voto.VotoLegenda = false;
                    votos.Add(voto);
                }
                else if (Evoto.TipoVoto.Value == TipoVoto.EnumType.legenda)
                {
                    var voto = new Voto();
                    voto.NomeCandidato = $"Legenda";
                    voto.NumeroCandidato = Evoto.IdentificacaoVotavel.Partido.Value;
                    voto.NumeroPartido = Evoto.IdentificacaoVotavel.Partido.Value.ToByte();
                    voto.QtdVotos = Evoto.QuantidadeVotos.Value.ToShort();
                    votosLegenda += voto.QtdVotos;
                    voto.VotoLegenda = true;
                    votos.Add(voto);
                }
                else if (Evoto.TipoVoto.Value == TipoVoto.EnumType.branco)
                {
                    votosBrancos += Evoto.QuantidadeVotos.Value.ToShort();
                }
                else if (Evoto.TipoVoto.Value == TipoVoto.EnumType.nulo)
                {
                    votosNulos += Evoto.QuantidadeVotos.Value.ToShort();
                }
            }

            return votos;
        }

        public List<string> CompararBoletins(BoletimUrna buA, BoletimUrna buB)
        {
            List<string> inconsistencias = new List<string>();

            CompararValores(inconsistencias, buA.CodigoMunicipio, buB.CodigoMunicipio, "Município");
            CompararValores(inconsistencias, buA.ZonaEleitoral, buB.ZonaEleitoral, "Zona Eleitoral");
            CompararValores(inconsistencias, buA.SecaoEleitoral, buB.SecaoEleitoral, "Seção Eleitoral");
            CompararValores(inconsistencias, buA.LocalVotacao, buB.LocalVotacao, "Local de votação");
            CompararValores(inconsistencias, buA.EleitoresAptos, buB.EleitoresAptos, "Eleitores aptos");
            CompararValores(inconsistencias, buA.EleitoresFaltosos, buB.EleitoresFaltosos, "Eleitores faltosos");
            CompararValores(inconsistencias, buA.HabilitadosPorAnoNascimento, buB.HabilitadosPorAnoNascimento, "Habilitados por ano de nascimento");
            CompararValores(inconsistencias, buA.Comparecimento, buB.Comparecimento, "Comparecimento");
            CompararValores(inconsistencias, buA.AberturaUrnaEletronica, buB.AberturaUrnaEletronica, "Data e Hora de Abertura da UE");
            CompararValores(inconsistencias, buA.FechamentoUrnaEletronica, buB.FechamentoUrnaEletronica, "Data e Hora de Fechamento da UE");
            CompararValores(inconsistencias, buA.CodigoIdentificacaoUrnaEletronica, buB.CodigoIdentificacaoUrnaEletronica, "Código de identificação UE");

            CompararValores(inconsistencias, buA.DF_EleitoresAptos, buB.DF_EleitoresAptos, "Eleitores Aptos Dep Federal");
            CompararValores(inconsistencias, buA.DF_Brancos, buB.DF_Brancos, "Votos Brancos Dep Federal");
            CompararValores(inconsistencias, buA.DF_Nulos, buB.DF_Nulos, "Votos Nulos Dep Federal");
            CompararValores(inconsistencias, buA.DF_VotosNominais, buB.DF_VotosNominais, "Votos Nominais Dep Federal");
            CompararValores(inconsistencias, buA.DF_VotosLegenda, buB.DF_VotosLegenda, "Votos Legenda Dep Federal");
            CompararValores(inconsistencias, buA.DF_Total, buB.DF_Total, "Votos Totais Dep Federal");

            CompararValores(inconsistencias, buA.DE_EleitoresAptos, buB.DE_EleitoresAptos, "Eleitores Aptos Dep Est/Distr");
            CompararValores(inconsistencias, buA.DE_Brancos, buB.DE_Brancos, "Votos Brancos Dep Est/Distr");
            CompararValores(inconsistencias, buA.DE_Nulos, buB.DE_Nulos, "Votos Nulos Dep Est/Distr");
            CompararValores(inconsistencias, buA.DE_VotosNominais, buB.DE_VotosNominais, "Votos Nominais Dep Est/Distr");
            CompararValores(inconsistencias, buA.DE_VotosLegenda, buB.DE_VotosLegenda, "Votos Legenda Dep Est/Distr");
            CompararValores(inconsistencias, buA.DE_Total, buB.DE_Total, "Votos Totais Dep Est/Distr");

            CompararValores(inconsistencias, buA.SE_EleitoresAptos, buB.SE_EleitoresAptos, "Eleitores Aptos Senador");
            CompararValores(inconsistencias, buA.SE_Brancos, buB.SE_Brancos, "Votos Brancos Senador");
            CompararValores(inconsistencias, buA.SE_Nulos, buB.SE_Nulos, "Votos Nulos Senador");
            CompararValores(inconsistencias, buA.SE_VotosNominais, buB.SE_VotosNominais, "Votos Nominais Senador");
            CompararValores(inconsistencias, buA.SE_Total, buB.SE_Total, "Votos Totais Senador");

            CompararValores(inconsistencias, buA.GO_EleitoresAptos, buB.GO_EleitoresAptos, "Eleitores Aptos Governador");
            CompararValores(inconsistencias, buA.GO_Brancos, buB.GO_Brancos, "Votos Brancos Governador");
            CompararValores(inconsistencias, buA.GO_Nulos, buB.GO_Nulos, "Votos Nulos Governador");
            CompararValores(inconsistencias, buA.GO_VotosNominais, buB.GO_VotosNominais, "Votos Nominais Governador");
            CompararValores(inconsistencias, buA.GO_Total, buB.GO_Total, "Votos Totais Governador");

            CompararValores(inconsistencias, buA.PR_EleitoresAptos, buB.PR_EleitoresAptos, "Eleitores Aptos Presidente");
            CompararValores(inconsistencias, buA.PR_Brancos, buB.PR_Brancos, "Votos Brancos Presidente");
            CompararValores(inconsistencias, buA.PR_Nulos, buB.PR_Nulos, "Votos Nulos Presidente");
            CompararValores(inconsistencias, buA.PR_VotosNominais, buB.PR_VotosNominais, "Votos Nominais Presidente");
            CompararValores(inconsistencias, buA.PR_Total, buB.PR_Total, "Votos Totais Presidente");

            // Comparar as listas de votos
            CompararListasDeVotos(inconsistencias, buA.VotosDeputadosFederais, buB.VotosDeputadosFederais, "Dep Federal");
            CompararListasDeVotos(inconsistencias, buA.VotosDeputadosEstaduais, buB.VotosDeputadosEstaduais, "Dep Est/Distr");
            CompararListasDeVotos(inconsistencias, buA.VotosSenador, buB.VotosSenador, "Senador");
            CompararListasDeVotos(inconsistencias, buA.VotosGovernador, buB.VotosGovernador, "Governador");
            CompararListasDeVotos(inconsistencias, buA.VotosPresidente, buB.VotosPresidente, "Presidente");

            return inconsistencias;
        }

        private void CompararValores(List<string> inconsistencias, string valorA, string valorB, string nomeCampo)
        {
            if (valorA != valorB)
                inconsistencias.Add($"{nomeCampo} são diferentes. Valor A: {valorA}, Valor B: {valorB}");
        }
        private void CompararValores(List<string> inconsistencias, DateTime valorA, DateTime valorB, string nomeCampo)
        {
            if (valorA != valorB)
                inconsistencias.Add($"{nomeCampo} são diferentes. Valor A: {valorA}, Valor B: {valorB}");
        }
        private void CompararValores(List<string> inconsistencias, short valorA, short valorB, string nomeCampo)
        {
            if (valorA != valorB)
                inconsistencias.Add($"{nomeCampo} são diferentes. Valor A: {valorA}, Valor B: {valorB}");
        }

        private void CompararListasDeVotos(List<string> inconsistencias, List<Voto> votosA, List<Voto> votosB, string cargo)
        {
            foreach (var votoA in votosA)
            {
                var votoB = votosB.Find(x => x.NumeroCandidato == votoA.NumeroCandidato && x.NumeroPartido == votoA.NumeroPartido && x.VotoLegenda == votoA.VotoLegenda);
                if (votoB == null)
                {
                    inconsistencias.Add($"Voto {cargo} {votoA.NumeroCandidato} existe na lista A mas não existe na lista B.");
                }
            }

            foreach (var votoB in votosB)
            {
                var votoA = votosA.Find(x => x.NumeroCandidato == votoB.NumeroCandidato && x.NumeroPartido == votoB.NumeroPartido && x.VotoLegenda == votoB.VotoLegenda);
                if (votoA == null)
                {
                    inconsistencias.Add($"Voto {cargo} {votoB.NumeroCandidato} existe na lista B mas não existe na lista A.");
                }
            }

            foreach (var votoA in votosA)
            {
                var votoB = votosB.Find(x => x.NumeroCandidato == votoA.NumeroCandidato && x.NumeroPartido == votoA.NumeroPartido && x.VotoLegenda == votoA.VotoLegenda);
                if (votoB != null && votoA.QtdVotos != votoB.QtdVotos)
                {
                    inconsistencias.Add($"Quantidade de votos do candidato {cargo} {votoA.NumeroCandidato} é diferente. Lista A: {votoA.QtdVotos}, Lista B: {votoB.QtdVotos}.");
                }
            }
        }

        public void Dispose()
        {
            // Nada a fazer
        }
    }
}
