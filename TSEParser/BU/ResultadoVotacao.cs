
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
    [ASN1Sequence(Name = "ResultadoVotacao", IsSet = false)]
    public class ResultadoVotacao : IASN1PreparedElement 
    {
        
        private TipoCargoConsulta tipoCargo_;
        
		[ASN1Element(Name = "tipoCargo", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public TipoCargoConsulta TipoCargo
        {
            get { return tipoCargo_; }
            set { tipoCargo_ = value;  }
        }
  
        private QtdEleitores qtdComparecimento_;
        
		[ASN1Element(Name = "qtdComparecimento", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public QtdEleitores QtdComparecimento
        {
            get { return qtdComparecimento_; }
            set { qtdComparecimento_ = value;  }
        }
  
        private System.Collections.Generic.ICollection<TotalVotosCargo> totaisVotosCargo_;
        
		[ASN1SequenceOf(Name = "totaisVotosCargo", IsSetOf = false)]
    
		[ASN1Element(Name = "totaisVotosCargo", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public System.Collections.Generic.ICollection<TotalVotosCargo> TotaisVotosCargo
        {
            get { return totaisVotosCargo_; }
            set { totaisVotosCargo_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ResultadoVotacao));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}