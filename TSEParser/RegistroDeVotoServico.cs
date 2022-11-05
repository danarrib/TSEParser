using Microsoft.EntityFrameworkCore.Internal;
using org.bn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TSERDV;

namespace TSEParser
{
    public class RegistroDeVotoServico : IDisposable
    {

        public EntidadeResultadoRDV DecodificarRegistroVoto(string arquivoRDV)
        {
            IDecoder decoder = CoderFactory.getInstance().newDecoder("DER");
            EntidadeResultadoRDV retorno = null;

            var arquivoRDVBytes = File.ReadAllBytes(arquivoRDV);
            using (MemoryStream memoryStream = new MemoryStream(arquivoRDVBytes))
            {
                retorno = decoder.decode<EntidadeResultadoRDV>(memoryStream);
                var jsonBU = JsonSerializer.Serialize(retorno, new JsonSerializerOptions()
                {
                    MaxDepth = 0,
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true
                });
            }

            return retorno;
        }

        public List<VotosSecaoRDV> ObterVotos(EntidadeResultadoRDV rdv, int codMunicipio, short codZona, short codSecao)
        {
            List<VotosSecaoRDV> votosRDV = new List<VotosSecaoRDV>();
            short idVotoRDV = 0;
            if (rdv.Rdv.Eleicoes.EleicoesVota == null)
                return votosRDV;

            foreach (var eVota in rdv.Rdv.Eleicoes.EleicoesVota)
            {
                foreach (var eleicao in eVota.VotosCargos)
                {
                    Cargos cargo;

                    switch (eleicao.IdCargo.CargoConstitucional.Value)
                    {
                        case CargoConstitucional.EnumType.presidente:
                            cargo = Cargos.Presidente;
                            break;
                        case CargoConstitucional.EnumType.governador:
                            cargo = Cargos.Governador;
                            break;
                        case CargoConstitucional.EnumType.senador:
                            cargo = Cargos.Senador;
                            break;
                        case CargoConstitucional.EnumType.deputadoFederal:
                            cargo = Cargos.DeputadoFederal;
                            break;
                        case CargoConstitucional.EnumType.deputadoEstadual:
                        case CargoConstitucional.EnumType.deputadoDistrital:
                            cargo = Cargos.DeputadoEstadual;
                            break;
                        default:
                            throw new Exception($"Tipo de cargo constitucional inválido ao ler RDV.");
                            break;
                    }
                    foreach (var voto in eleicao.Votos)
                    {
                        var numeroCandidato = 0;
                        var votobranco = false;
                        var votonulo = false;
                        var votolegenda = false;

                        switch (voto.TipoVoto.Value)
                        {
                            case TipoVoto.EnumType.nominal:
                                votobranco = false;
                                votonulo = false;
                                votolegenda = false;
                                break;
                            case TipoVoto.EnumType.legenda:
                                votolegenda = true;
                                break;
                            case TipoVoto.EnumType.branco:
                            case TipoVoto.EnumType.brancoAposSuspensao:
                                votobranco = true;
                                break;
                            case TipoVoto.EnumType.nulo:
                            case TipoVoto.EnumType.nuloAposSuspensao:
                            case TipoVoto.EnumType.nuloPorRepeticao:
                            case TipoVoto.EnumType.nuloCargoSemCandidato:
                            case TipoVoto.EnumType.nuloAposSuspensaoCargoSemCandidato:
                                votonulo = true;
                                break;
                            default:
                                throw new Exception($"Tipo de voto inválido ao ler RDV.");
                                break;
                        }
                        if (voto.Digitacao != null)
                            int.TryParse(voto.Digitacao.Value, out numeroCandidato);

                        var votoRDV = votosRDV.Find(x => x.VotoBranco == votobranco && x.VotoNulo == votonulo && x.VotoLegenda == votolegenda && x.NumeroCandidato == numeroCandidato && x.Cargo == cargo);
                        if (votoRDV == null)
                        {
                            idVotoRDV++;
                            votoRDV = new VotosSecaoRDV()
                            {
                                VotoBranco = votobranco,
                                VotoLegenda = votolegenda,
                                VotoNulo = votonulo,
                                NumeroCandidato = numeroCandidato,
                                Cargo = cargo,
                                QtdVotos = 1,
                                SecaoEleitoralCodigoSecao = codSecao,
                                SecaoEleitoralMunicipioCodigo = codMunicipio,
                                SecaoEleitoralCodigoZonaEleitoral = codZona,
                                IdVotoRDV = idVotoRDV,
                            };
                            votosRDV.Add(votoRDV);
                        }
                        else
                        {
                            votoRDV.QtdVotos++;
                        }
                    }
                }
            }

            return votosRDV;
        }

        public void CompararBUeRDV(BoletimUrna bu, List<VotosSecaoRDV> RDVs, out string mensagem)
        {
            List<string> inconsistencias = new List<string>();

            // Comparar votos Brancos de cada cargo
            ContarVotosBrancos(RDVs, Cargos.DeputadoFederal, bu.DF_Brancos, inconsistencias);
            ContarVotosBrancos(RDVs, Cargos.DeputadoEstadual, bu.DE_Brancos, inconsistencias);
            ContarVotosBrancos(RDVs, Cargos.Senador, bu.SE_Brancos, inconsistencias);
            ContarVotosBrancos(RDVs, Cargos.Governador, bu.GO_Brancos, inconsistencias);
            ContarVotosBrancos(RDVs, Cargos.Presidente, bu.PR_Brancos, inconsistencias);

            // Comparar votos Nulos
            ContarVotosNulos(RDVs, Cargos.DeputadoFederal, bu.DF_Nulos, inconsistencias);
            ContarVotosNulos(RDVs, Cargos.DeputadoEstadual, bu.DE_Nulos, inconsistencias);
            ContarVotosNulos(RDVs, Cargos.Senador, bu.SE_Nulos, inconsistencias);
            ContarVotosNulos(RDVs, Cargos.Governador, bu.GO_Nulos, inconsistencias);
            ContarVotosNulos(RDVs, Cargos.Presidente, bu.PR_Nulos, inconsistencias);

            // Comparar votos de Legenda
            CompararVotosLegenda(RDVs, Cargos.DeputadoFederal, inconsistencias, bu.VotosDeputadosFederais);
            CompararVotosLegenda(RDVs, Cargos.DeputadoEstadual, inconsistencias, bu.VotosDeputadosEstaduais);
            CompararVotosLegenda(RDVs, Cargos.Senador, inconsistencias, bu.VotosSenador);
            CompararVotosLegenda(RDVs, Cargos.Governador, inconsistencias, bu.VotosGovernador);
            CompararVotosLegenda(RDVs, Cargos.Presidente, inconsistencias, bu.VotosPresidente);

            // Comparar votos dos candidatos
            CompararVotosCandidatos(RDVs, Cargos.DeputadoFederal, inconsistencias, bu.VotosDeputadosFederais);
            CompararVotosCandidatos(RDVs, Cargos.DeputadoEstadual, inconsistencias, bu.VotosDeputadosEstaduais);
            CompararVotosCandidatos(RDVs, Cargos.Senador, inconsistencias, bu.VotosSenador);
            CompararVotosCandidatos(RDVs, Cargos.Governador, inconsistencias, bu.VotosGovernador);
            CompararVotosCandidatos(RDVs, Cargos.Presidente, inconsistencias, bu.VotosPresidente);

            mensagem = string.Empty;
            foreach (var msg in inconsistencias)
            {
                mensagem += msg + Environment.NewLine;
            }
        }

        private void CompararVotosCandidatos(List<VotosSecaoRDV> RDVs, Cargos cargo, List<string> inconsistencias, List<Voto> lstVotosBU)
        {
            foreach (var votoLegendaBU in lstVotosBU.Where(x => !x.VotoLegenda))
            {
                var lstRDV = RDVs.Where(x => x.Cargo == cargo && !x.VotoLegenda && !x.VotoBranco && !x.VotoNulo && x.NumeroCandidato == votoLegendaBU.NumeroCandidato);
                short qtdVotosRDV = 0;
                foreach (var votosRDV in lstRDV)
                {
                    qtdVotosRDV += votosRDV.QtdVotos;
                }

                if (qtdVotosRDV != votoLegendaBU.QtdVotos)
                {
                    inconsistencias.Add($"Quantidade de votos para {cargo.NomeCargo()}, candidato {votoLegendaBU.NumeroCandidato}, é diferente entre o Boletim de Urna e o Registro de Voto. BU: {votoLegendaBU.QtdVotos}, RDV: {qtdVotosRDV}.");
                }
            }
        }

        private void CompararVotosLegenda(List<VotosSecaoRDV> RDVs, Cargos cargo, List<string> inconsistencias, List<Voto> lstVotosBU)
        {
            foreach (var votoLegendaBU in lstVotosBU.Where(x => x.VotoLegenda))
            {
                var lstRDV = RDVs.Where(x => x.Cargo == cargo && x.VotoLegenda && x.NumeroCandidato.ToString().StartsWith(votoLegendaBU.NumeroCandidato.ToString()));
                short qtdVotosRDV = 0;
                foreach (var votosRDV in lstRDV)
                {
                    qtdVotosRDV += votosRDV.QtdVotos;
                }

                if (qtdVotosRDV != votoLegendaBU.QtdVotos)
                {
                    inconsistencias.Add($"Quantidade de votos de legenda para {cargo.NomeCargo()}, partido {votoLegendaBU.NumeroCandidato}, é diferente entre o Boletim de Urna e o Registro de Voto. BU: {votoLegendaBU.QtdVotos}, RDV: {qtdVotosRDV}.");
                }
            }
        }

        private void ContarVotosBrancos(List<VotosSecaoRDV> RDVs, Cargos cargo, short qtdVotosBU, List<string> inconsistencias)
        {
            short qtdVotosRDV = 0;
            foreach (var voto in RDVs.Where(x => x.Cargo == cargo && x.VotoBranco))
            {
                qtdVotosRDV += voto.QtdVotos;
            }

            if (qtdVotosRDV != qtdVotosBU)
            {
                inconsistencias.Add($"Quantidade de votos brancos para {cargo.NomeCargo()} é diferente entre o Boletim de Urna e o Registro de Voto. BU: {qtdVotosBU}, RDV: {qtdVotosRDV}.");
            }
        }

        private void ContarVotosNulos(List<VotosSecaoRDV> RDVs, Cargos cargo, short qtdVotosBU, List<string> inconsistencias)
        {
            short qtdVotosRDV = 0;
            foreach (var voto in RDVs.Where(x => x.Cargo == cargo && x.VotoNulo))
            {
                qtdVotosRDV += voto.QtdVotos;
            }

            if (qtdVotosRDV != qtdVotosBU)
            {
                inconsistencias.Add($"Quantidade de votos nulos para {cargo.NomeCargo()} é diferente entre o Boletim de Urna e o Registro de Voto. BU: {qtdVotosBU}, RDV: {qtdVotosRDV}.");
            }
        }

        public void Dispose()
        {

        }
    }
}
