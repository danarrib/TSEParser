using System;
using System.Collections.Generic;
using System.Text;

namespace TSEParser
{
    public class BoletimUrna
    {
        public BoletimUrna()
        {
            VotosDeputadosFederais = new List<Voto>();
            VotosDeputadosEstaduais = new List<Voto>();
            VotosSenador = new List<Voto>();
            VotosGovernador = new List<Voto>();
            VotosPresidente = new List<Voto>();
        }

        public string UF { get; set; }
        public string NomeUF { get; set; }
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
        public List<Voto> VotosDeputadosFederais { get; set; }
        public short DF_EleitoresAptos { get; set; }
        public short DF_VotosNominais { get; set; }
        public short DF_VotosLegenda { get; set; }
        public short DF_Brancos { get; set; }
        public short DF_Nulos { get; set; }
        public short DF_Total { get; set; }
        public List<Voto> VotosDeputadosEstaduais { get; set; }
        public short DE_EleitoresAptos { get; set; }
        public short DE_VotosNominais { get; set; }
        public short DE_VotosLegenda { get; set; }
        public short DE_Brancos { get; set; }
        public short DE_Nulos { get; set; }
        public short DE_Total { get; set; }
        public List<Voto> VotosSenador { get; set; }
        public short SE_EleitoresAptos { get; set; }
        public short SE_VotosNominais { get; set; }
        public short SE_Brancos { get; set; }
        public short SE_Nulos { get; set; }
        public short SE_Total { get; set; }
        public List<Voto> VotosGovernador { get; set; }
        public short GO_EleitoresAptos { get; set; }
        public short GO_VotosNominais { get; set; }
        public short GO_Brancos { get; set; }
        public short GO_Nulos { get; set; }
        public short GO_Total { get; set; }
        public List<Voto> VotosPresidente { get; set; }
        public short PR_EleitoresAptos { get; set; }
        public short PR_VotosNominais { get; set; }
        public short PR_Brancos { get; set; }
        public short PR_Nulos { get; set; }
        public short PR_Total { get; set; }
    }

    public class Voto
    {
        public byte NumeroPartido { get; set; }
        public string NomePartido { get; set; }
        public int NumeroCandidato { get; set; }
        public string NomeCandidato { get; set; }
        public short QtdVotos { get; set; }
        public bool VotoLegenda { get; set; }
    }


}
