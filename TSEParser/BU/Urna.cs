
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
    [ASN1Sequence(Name = "Urna", IsSet = false)]
    public class Urna : IASN1PreparedElement 
    {
        
        private TipoUrna tipoUrna_;
        
		[ASN1Element(Name = "tipoUrna", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public TipoUrna TipoUrna
        {
            get { return tipoUrna_; }
            set { tipoUrna_ = value;  }
        }
  
        private string versaoVotacao_;
        
		[ASN1String(Name = "", StringType = UniversalTags.GeneralString, IsUCS = false)]
		[ASN1Element(Name = "versaoVotacao", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public string VersaoVotacao
        {
            get { return versaoVotacao_; }
            set { versaoVotacao_ = value;  }
        }
  
        private CorrespondenciaResultado correspondenciaResultado_;
        
		[ASN1Element(Name = "correspondenciaResultado", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public CorrespondenciaResultado CorrespondenciaResultado
        {
            get { return correspondenciaResultado_; }
            set { correspondenciaResultado_ = value;  }
        }
  
        private TipoArquivo tipoArquivo_;
        
		[ASN1Element(Name = "tipoArquivo", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public TipoArquivo TipoArquivo
        {
            get { return tipoArquivo_; }
            set { tipoArquivo_ = value;  }
        }
  
        private NumeroSerieFlash numeroSerieFV_;
        
		[ASN1Element(Name = "numeroSerieFV", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NumeroSerieFlash NumeroSerieFV
        {
            get { return numeroSerieFV_; }
            set { numeroSerieFV_ = value;  }
        }
  
        private TipoApuracaoSA motivoUtilizacaoSA_;
        
        private bool  motivoUtilizacaoSA_present = false;
        
		[ASN1Element(Name = "motivoUtilizacaoSA", IsOptional = true, HasTag = false, HasDefaultValue = false)]
        public TipoApuracaoSA MotivoUtilizacaoSA
        {
            get { return motivoUtilizacaoSA_; }
            set { motivoUtilizacaoSA_ = value; motivoUtilizacaoSA_present = true;  }
        }
  
        public bool isMotivoUtilizacaoSAPresent()
        {
            return this.motivoUtilizacaoSA_present == true;
        }
        

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Urna));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
