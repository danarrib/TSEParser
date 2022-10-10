
//
// This file was generated by the BinaryNotes compiler (created by Abdulla Abdurakhmanov, modified by Sylvain Prevost).
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System;
using System.Numerics;

using org.bn.attributes;
using org.bn.attributes.constraints;
using org.bn.coders;
using org.bn.types;
using org.bn;

namespace TSEBU {


    [ASN1PreparedElement]
    [ASN1Sequence(Name = "EntidadeBoletimUrna", IsSet = false)]
    public class EntidadeBoletimUrna : IASN1PreparedElement 
    {
        
        private CabecalhoEntidade cabecalho_;
        
		[ASN1Element(Name = "cabecalho", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public CabecalhoEntidade Cabecalho
        {
            get { return cabecalho_; }
            set { cabecalho_ = value;  }
        }
  
        private Fase fase_;
        
		[ASN1Element(Name = "fase", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public Fase Fase
        {
            get { return fase_; }
            set { fase_ = value;  }
        }
  
        private Urna urna_;
        
		[ASN1Element(Name = "urna", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public Urna Urna
        {
            get { return urna_; }
            set { urna_ = value;  }
        }
  
        private IdentificacaoSecaoEleitoral identificacaoSecao_;
        
		[ASN1Element(Name = "identificacaoSecao", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public IdentificacaoSecaoEleitoral IdentificacaoSecao
        {
            get { return identificacaoSecao_; }
            set { identificacaoSecao_ = value;  }
        }
  
        private DataHoraJE dataHoraEmissao_;
        
		[ASN1Element(Name = "dataHoraEmissao", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public DataHoraJE DataHoraEmissao
        {
            get { return dataHoraEmissao_; }
            set { dataHoraEmissao_ = value;  }
        }
  
        private DadosSecaoSA dadosSecaoSA_;
        
		[ASN1Element(Name = "dadosSecaoSA", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public DadosSecaoSA DadosSecaoSA
        {
            get { return dadosSecaoSA_; }
            set { dadosSecaoSA_ = value;  }
        }
  
        private QtdEleitores qtdEleitoresLibCodigo_;
        
        private bool  qtdEleitoresLibCodigo_present = false;
        
		[ASN1Element(Name = "qtdEleitoresLibCodigo", IsOptional = true, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public QtdEleitores QtdEleitoresLibCodigo
        {
            get { return qtdEleitoresLibCodigo_; }
            set { qtdEleitoresLibCodigo_ = value; qtdEleitoresLibCodigo_present = true;  }
        }
  
        private QtdEleitores qtdEleitoresCompBiometrico_;
        
        private bool  qtdEleitoresCompBiometrico_present = false;
        
		[ASN1Element(Name = "qtdEleitoresCompBiometrico", IsOptional = true, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public QtdEleitores QtdEleitoresCompBiometrico
        {
            get { return qtdEleitoresCompBiometrico_; }
            set { qtdEleitoresCompBiometrico_ = value; qtdEleitoresCompBiometrico_present = true;  }
        }
  
        private System.Collections.Generic.ICollection<ResultadoVotacaoPorEleicao> resultadosVotacaoPorEleicao_;
        
		[ASN1SequenceOf(Name = "resultadosVotacaoPorEleicao", IsSetOf = false)]
    
		[ASN1Element(Name = "resultadosVotacaoPorEleicao", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public System.Collections.Generic.ICollection<ResultadoVotacaoPorEleicao> ResultadosVotacaoPorEleicao
        {
            get { return resultadosVotacaoPorEleicao_; }
            set { resultadosVotacaoPorEleicao_ = value;  }
        }
  
        private System.Collections.Generic.ICollection<CorrespondenciaResultado> historicoCorrespondencias_;
        
        private bool  historicoCorrespondencias_present = false;
        
		[ASN1SequenceOf(Name = "historicoCorrespondencias", IsSetOf = false)]
    
		[ASN1Element(Name = "historicoCorrespondencias", IsOptional = true, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public System.Collections.Generic.ICollection<CorrespondenciaResultado> HistoricoCorrespondencias
        {
            get { return historicoCorrespondencias_; }
            set { historicoCorrespondencias_ = value; historicoCorrespondencias_present = true;  }
        }
  
        private System.Collections.Generic.ICollection<HistoricoVotoImpresso> historicoVotoImpresso_;
        
        private bool  historicoVotoImpresso_present = false;
        
		[ASN1SequenceOf(Name = "historicoVotoImpresso", IsSetOf = false)]
    
		[ASN1Element(Name = "historicoVotoImpresso", IsOptional = true, HasTag = true, Tag = 5, HasDefaultValue = false)]
        public System.Collections.Generic.ICollection<HistoricoVotoImpresso> HistoricoVotoImpresso
        {
            get { return historicoVotoImpresso_; }
            set { historicoVotoImpresso_ = value; historicoVotoImpresso_present = true;  }
        }
  
        private byte[] chaveAssinaturaVotosVotavel_;
        [ASN1OctetString( Name = "" )]
    
		[ASN1Element(Name = "chaveAssinaturaVotosVotavel", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public byte[] ChaveAssinaturaVotosVotavel
        {
            get { return chaveAssinaturaVotosVotavel_; }
            set { chaveAssinaturaVotosVotavel_ = value;  }
        }
  
        public bool isQtdEleitoresLibCodigoPresent()
        {
            return this.qtdEleitoresLibCodigo_present == true;
        }
        
        public bool isQtdEleitoresCompBiometricoPresent()
        {
            return this.qtdEleitoresCompBiometrico_present == true;
        }
        
        public bool isHistoricoCorrespondenciasPresent()
        {
            return this.historicoCorrespondencias_present == true;
        }
        
        public bool isHistoricoVotoImpressoPresent()
        {
            return this.historicoVotoImpresso_present == true;
        }
        

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(EntidadeBoletimUrna));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}