
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
    [ASN1Sequence(Name = "HistoricoVotoImpresso", IsSet = false)]
    public class HistoricoVotoImpresso : IASN1PreparedElement 
    {
        
        private int idImpressoraVotos_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 0, Max = 99999999)]
		[ASN1Element(Name = "idImpressoraVotos", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int IdImpressoraVotos
        {
            get { return idImpressoraVotos_; }
            set { idImpressoraVotos_ = value;  }
        }
  
        private int idRepositorioVotos_;
        [ASN1Integer( Name = "" )]
    
		[ASN1ValueRangeConstraint(Min = 0, Max = 99999999)]
		[ASN1Element(Name = "idRepositorioVotos", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public int IdRepositorioVotos
        {
            get { return idRepositorioVotos_; }
            set { idRepositorioVotos_ = value;  }
        }
  
        private DataHoraJE dataHoraLigamento_;
        
		[ASN1Element(Name = "dataHoraLigamento", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public DataHoraJE DataHoraLigamento
        {
            get { return dataHoraLigamento_; }
            set { dataHoraLigamento_ = value;  }
        }
  

        public void initWithDefaults() 
        {
            
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(HistoricoVotoImpresso));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
