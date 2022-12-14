
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
    [ASN1Enum ( Name = "TipoCargoConsulta")]
    public class TipoCargoConsulta : IASN1PreparedElement 
    {
        public enum EnumType 
        {
            
            [ASN1EnumItem ( Name = "majoritario", HasTag = true , Tag = 1 )]
            majoritario , 
            [ASN1EnumItem ( Name = "proporcional", HasTag = true , Tag = 2 )]
            proporcional , 
            [ASN1EnumItem ( Name = "consulta", HasTag = true , Tag = 3 )]
            consulta , 
        }
        
        private EnumType val;
        
        public EnumType Value
        {
            get { return val; }
            set { val = value; }
        }

        public void initWithDefaults()
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(TipoCargoConsulta));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
