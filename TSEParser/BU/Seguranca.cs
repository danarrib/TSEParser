
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
    [ASN1Sequence(Name = "Seguranca", IsSet = false)]
    public class Seguranca : IASN1PreparedElement 
    {
        
        private int idTipoArquivo_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 0, Max = 2)]
		[ASN1Element(Name = "idTipoArquivo", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int IdTipoArquivo
        {
            get { return idTipoArquivo_; }
            set { idTipoArquivo_ = value;  }
        }
  
        private int idCriptografia_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 1, Max = 3)]
		[ASN1Element(Name = "idCriptografia", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int IdCriptografia
        {
            get { return idCriptografia_; }
            set { idCriptografia_ = value;  }
        }
  
        private int idArquivoCD_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 0, Max = 255)]
		[ASN1Element(Name = "idArquivoCD", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int IdArquivoCD
        {
            get { return idArquivoCD_; }
            set { idArquivoCD_ = value;  }
        }
  
        private byte[] idArquivoChave_;
        [ASN1OctetString( Name = "" )]
    
		[ASN1Element(Name = "idArquivoChave", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public byte[] IdArquivoChave
        {
            get { return idArquivoChave_; }
            set { idArquivoChave_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Seguranca));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
