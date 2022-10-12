using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSEParser
{
    public class UnidadeFederativa
    {
        [Key]
        [MaxLength(2)]
        [Required]
        public string Sigla { get; set; }
        [MaxLength(20)]
        [Required]
        public string Nome { get; set; }
    }

    public class Municipio
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Codigo { get; set; }
        [MaxLength(40)]
        [Required]
        public string Nome { get; set; }
        [MaxLength(2)]
        [Required]
        public string UFSigla { get; set; }
        public UnidadeFederativa UF { get; set; }
    }

    public class SecaoEleitoral
    {
        public Municipio Municipio { get; set; }

        [Required]
        public int MunicipioCodigo { get; set; }
        [Required]
        public short CodigoZonaEleitoral { get; set; }
        [Required]
        public short CodigoSecao { get; set; }
        [Required]
        public short CodigoLocalVotacao { get; set; }
        [Required]
        public short EleitoresAptos { get; set; }
        [Required]
        public short Comparecimento { get; set; }
        [Required]
        public short EleitoresFaltosos { get; set; }
        [Required]
        public short HabilitadosPorAnoNascimento { get; set; }
        [Required]
        public int CodigoIdentificacaoUrnaEletronica { get; set; }
        [Required]
        public DateTime AberturaUrnaEletronica { get; set; }
        [Required]
        public DateTime FechamentoUrnaEletronica { get; set; }
        public short DF_EleitoresAptos { get; set; }
        public short DF_VotosNominais { get; set; }
        public short DF_VotosLegenda { get; set; }
        public short DF_Brancos { get; set; }
        public short DF_Nulos { get; set; }
        public short DF_Total { get; set; }

        public short DE_EleitoresAptos { get; set; }
        public short DE_VotosNominais { get; set; }
        public short DE_VotosLegenda { get; set; }
        public short DE_Brancos { get; set; }
        public short DE_Nulos { get; set; }
        public short DE_Total { get; set; }

        public short SE_EleitoresAptos { get; set; }
        public short SE_VotosNominais { get; set; }
        public short SE_Brancos { get; set; }
        public short SE_Nulos { get; set; }
        public short SE_Total { get; set; }

        public short GO_EleitoresAptos { get; set; }
        public short GO_VotosNominais { get; set; }
        public short GO_Brancos { get; set; }
        public short GO_Nulos { get; set; }
        public short GO_Total { get; set; }

        public short PR_EleitoresAptos { get; set; }
        public short PR_VotosNominais { get; set; }
        public short PR_Brancos { get; set; }
        public short PR_Nulos { get; set; }
        public short PR_Total { get; set; }
    }

    public class VotosSecao
    {
        public SecaoEleitoral SecaoEleitoral { get; set; }

        [Required]
        [Column("MunicipioCodigo")]
        public int SecaoEleitoralMunicipioCodigo { get; set; }
        [Required]
        [Column("CodigoZonaEleitoral")]
        public short SecaoEleitoralCodigoZonaEleitoral { get; set; }
        [Required]
        [Column("CodigoSecao")]
        public short SecaoEleitoralCodigoSecao { get; set; }
        [Required]
        public Cargos Cargo { get; set; }
        [Required]
        public int NumeroCandidato { get; set; }
        [Required]
        public short QtdVotos { get; set; }
        public bool VotoLegenda { get; set; }
    }

    public class VotosMunicipio
    {
        public Municipio Municipio { get; set; }

        [Required]
        [Column("MunicipioCodigo")]
        public int MunicipioCodigo { get; set; }
        [Required]
        public Cargos Cargo { get; set; }
        [Required]
        public int NumeroCandidato { get; set; }
        [Required]
        public long QtdVotos { get; set; }
        public bool VotoLegenda { get; set; }
    }

    public class Candidato
    {
        [Required]
        public Cargos Cargo { get; set; }
        [Required]
        public int NumeroCandidato { get; set; }
        [MaxLength(2)]
        public string UFSigla { get; set; }
        [MaxLength(30)]
        [Required]
        public string Nome { get; set; }

        public UnidadeFederativa UF { get; set; }
    }

    public class Partido
    {
        [Key]
        [Required]
        public byte Numero { get; set; }
        [MaxLength(30)]
        [Required]
        public string Nome { get; set; }
    }

    public enum Cargos : byte
    {
        DeputadoFederal = 1,
        DeputadoEstadual = 2,
        Senador = 3,
        Governador = 4,
        Presidente = 5
    }
}
