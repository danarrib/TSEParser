
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
    [ASN1Sequence(Name = "CabecalhoEntidade", IsSet = false)]
    public class CabecalhoEntidade : IASN1PreparedElement 
    {
        
        private DataHoraJE dataGeracao_;
        
		[ASN1Element(Name = "dataGeracao", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public DataHoraJE DataGeracao
        {
            get { return dataGeracao_; }
            set { dataGeracao_ = value;  }
        }
  
        private IDEleitoral idEleitoral_;
        
		[ASN1Element(Name = "idEleitoral", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public IDEleitoral IdEleitoral
        {
            get { return idEleitoral_; }
            set { idEleitoral_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CabecalhoEntidade));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
