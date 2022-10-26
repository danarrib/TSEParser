
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

namespace TSERDV {


    [ASN1PreparedElement]
    [ASN1Sequence(Name = "IdentificacaoSecaoEleitoral", IsSet = false)]
    public class IdentificacaoSecaoEleitoral : IASN1PreparedElement 
    {
        
        private MunicipioZona municipioZona_;
        
		[ASN1Element(Name = "municipioZona", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public MunicipioZona MunicipioZona
        {
            get { return municipioZona_; }
            set { municipioZona_ = value;  }
        }
  
        private NumeroLocal local_;
        
		[ASN1Element(Name = "local", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NumeroLocal Local
        {
            get { return local_; }
            set { local_ = value;  }
        }
  
        private NumeroSecao secao_;
        
		[ASN1Element(Name = "secao", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NumeroSecao Secao
        {
            get { return secao_; }
            set { secao_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(IdentificacaoSecaoEleitoral));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
