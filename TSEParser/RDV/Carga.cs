
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
    [ASN1Sequence(Name = "Carga", IsSet = false)]
    public class Carga : IASN1PreparedElement 
    {
        
        private NumeroInternoUrna numeroInternoUrna_;
        
		[ASN1Element(Name = "numeroInternoUrna", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NumeroInternoUrna NumeroInternoUrna
        {
            get { return numeroInternoUrna_; }
            set { numeroInternoUrna_ = value;  }
        }
  
        private NumeroSerieFlash numeroSerieFC_;
        
		[ASN1Element(Name = "numeroSerieFC", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NumeroSerieFlash NumeroSerieFC
        {
            get { return numeroSerieFC_; }
            set { numeroSerieFC_ = value;  }
        }
  
        private DataHoraJE dataHoraCarga_;
        
		[ASN1Element(Name = "dataHoraCarga", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public DataHoraJE DataHoraCarga
        {
            get { return dataHoraCarga_; }
            set { dataHoraCarga_ = value;  }
        }
  
        private string codigoCarga_;
        
		[ASN1String(Name = "", StringType = UniversalTags.GeneralString, IsUCS = false)]
		[ASN1Element(Name = "codigoCarga", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public string CodigoCarga
        {
            get { return codigoCarga_; }
            set { codigoCarga_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Carga));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
