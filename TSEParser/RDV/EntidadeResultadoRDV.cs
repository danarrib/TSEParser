
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
    [ASN1Sequence(Name = "EntidadeResultadoRDV", IsSet = false)]
    public class EntidadeResultadoRDV : IASN1PreparedElement 
    {
        
        private CabecalhoEntidade cabecalho_;
        
		[ASN1Element(Name = "cabecalho", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public CabecalhoEntidade Cabecalho
        {
            get { return cabecalho_; }
            set { cabecalho_ = value;  }
        }
  
        private Urna urna_;
        
		[ASN1Element(Name = "urna", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public Urna Urna
        {
            get { return urna_; }
            set { urna_ = value;  }
        }
  
        private EntidadeRegistroDigitalVoto rdv_;
        
		[ASN1Element(Name = "rdv", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public EntidadeRegistroDigitalVoto Rdv
        {
            get { return rdv_; }
            set { rdv_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(EntidadeResultadoRDV));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
