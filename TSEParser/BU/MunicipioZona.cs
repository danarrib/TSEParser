
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
    [ASN1Sequence(Name = "MunicipioZona", IsSet = false)]
    public class MunicipioZona : IASN1PreparedElement 
    {
        
        private CodigoMunicipio municipio_;
        
		[ASN1Element(Name = "municipio", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public CodigoMunicipio Municipio
        {
            get { return municipio_; }
            set { municipio_ = value;  }
        }
  
        private NumeroZona zona_;
        
		[ASN1Element(Name = "zona", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NumeroZona Zona
        {
            get { return zona_; }
            set { zona_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(MunicipioZona));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
