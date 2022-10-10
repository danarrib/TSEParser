
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
    [ASN1Sequence(Name = "DadosSA", IsSet = false)]
    public class DadosSA : IASN1PreparedElement 
    {
        
        private int juntaApuradora_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 0, Max = 9999)]
		[ASN1Element(Name = "juntaApuradora", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int JuntaApuradora
        {
            get { return juntaApuradora_; }
            set { juntaApuradora_ = value;  }
        }
  
        private int turmaApuradora_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 0, Max = 9999)]
		[ASN1Element(Name = "turmaApuradora", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int TurmaApuradora
        {
            get { return turmaApuradora_; }
            set { turmaApuradora_ = value;  }
        }
  
        private NumeroInternoUrna numeroInternoUrnaOrigem_;
        
        private bool  numeroInternoUrnaOrigem_present = false;
        
		[ASN1Element(Name = "numeroInternoUrnaOrigem", IsOptional = true, HasTag = false, HasDefaultValue = false)]
        public NumeroInternoUrna NumeroInternoUrnaOrigem
        {
            get { return numeroInternoUrnaOrigem_; }
            set { numeroInternoUrnaOrigem_ = value; numeroInternoUrnaOrigem_present = true;  }
        }
  
        public bool isNumeroInternoUrnaOrigemPresent()
        {
            return this.numeroInternoUrnaOrigem_present == true;
        }
        

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DadosSA));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
