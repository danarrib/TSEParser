
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
    [ASN1Sequence(Name = "CorrespondenciaResultado", IsSet = false)]
    public class CorrespondenciaResultado : IASN1PreparedElement 
    {
        
        private IdentificacaoUrna identificacao_;
        
		[ASN1Element(Name = "identificacao", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public IdentificacaoUrna Identificacao
        {
            get { return identificacao_; }
            set { identificacao_ = value;  }
        }
  
        private Carga carga_;
        
		[ASN1Element(Name = "carga", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public Carga Carga
        {
            get { return carga_; }
            set { carga_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CorrespondenciaResultado));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
