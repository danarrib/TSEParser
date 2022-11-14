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
        public DateTime Zeresima { get; set; }
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

        public bool LogUrnaInconsistente { get; set; }
        public short ModeloUrnaEletronica { get; set; }
        public bool ResultadoSistemaApuracao { get; set; }
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

    public class DefeitosSecao
    {
        public SecaoEleitoral SecaoEleitoral { get; set; }

        [Required]
        [Column("MunicipioCodigo")]
        public int DefeitosSecaoMunicipioCodigo { get; set; }
        [Required]
        [Column("CodigoZonaEleitoral")]
        public short DefeitosSecaoCodigoZonaEleitoral { get; set; }
        [Required]
        [Column("CodigoSecao")]
        public short DefeitosSecaoCodigoSecao { get; set; }
        public bool SemArquivo { get; set; }
        public bool Rejeitado { get; set; }
        public bool Excluido { get; set; }
        public int CodigoIdentificacaoUrnaEletronicaBU { get; set; }
        public bool ArquivoIMGBUFaltando { get; set; }
        public bool ArquivoBUFaltando { get; set; }
        public bool ArquivoRDVFaltando { get; set; }
        public bool ArquivoLOGJEZFaltando { get; set; }
        public bool ArquivoBUeIMGBUDiferentes { get; set; }
        public bool ArquivoIMGBUCorrompido { get; set; }
        public bool ArquivoBUCorrompido { get; set; }
        public bool ArquivoRDVCorrompido { get; set; }
        public bool DiferencaVotosBUeIMGBU { get; set; }
    }

    public class VotosSecaoRDV
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
        public short IdVotoRDV { get; set; }
        [Required]
        public Cargos Cargo { get; set; }
        [Required]
        public int NumeroCandidato { get; set; }
        [Required]
        public short QtdVotos { get; set; }
        [Required]
        public bool VotoLegenda { get; set; }
        [Required]
        public bool VotoNulo { get; set; }
        [Required]
        public bool VotoBranco { get; set; }
    }

    public class VotosLog
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
        public short IdVotoLog { get; set; }
        [Required]
        public int LinhaLog { get; set; }
        [Required]
        public int LinhaLogFim { get; set; }
        [Required]
        public DateTime InicioVoto { get; set; }
        [Required]
        public DateTime HabilitacaoUrna { get; set; }
        [Required]
        public DateTime FimVoto { get; set; }
        [Required]
        public bool PossuiBiometria { get; set; }
        public DedoBiometria DedoBiometria { get; set; }
        public short ScoreBiometria { get; set; }
        [Required]
        public bool HabilitacaoCancelada { get; set; }
        [Required]
        public bool VotouDF { get; set; }
        [Required]
        public bool VotouDE { get; set; }
        [Required]
        public bool VotouSE { get; set; }
        [Required]
        public bool VotouGO { get; set; }
        [Required]
        public bool VotouPR { get; set; }
        [Required]
        public bool VotoNuloSuspensaoDF { get; set; }
        [Required]
        public bool VotoNuloSuspensaoDE { get; set; }
        [Required]
        public bool VotoNuloSuspensaoSE { get; set; }
        [Required]
        public bool VotoNuloSuspensaoGO { get; set; }
        [Required]
        public bool VotoNuloSuspensaoPR { get; set; }
        [Required]
        public bool VotoComputado { get; set; }
        [Required]
        public byte QtdTeclasIndevidas { get; set; }
        [Required]
        public bool EleitorSuspenso { get; set; }
        public short ModeloUrnaEletronica { get; set; }
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
        Presidente = 5,
        Prefeito = 10,
        Vereador = 11,
    }
    public enum DedoBiometria : byte
    {
        Indefinido = 0,
        PolegarDireito = 1,
        PolegarEsquerdo = 2,
        IndicadorDireito = 3,
        IndicadorEsquerdo = 4,
    }
}
