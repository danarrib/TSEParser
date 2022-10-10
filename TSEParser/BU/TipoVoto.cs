
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
    [ASN1Enum ( Name = "TipoVoto")]
    public class TipoVoto : IASN1PreparedElement 
    {
        public enum EnumType 
        {
            
            [ASN1EnumItem ( Name = "nominal", HasTag = true , Tag = 1 )]
            nominal , 
            [ASN1EnumItem ( Name = "branco", HasTag = true , Tag = 2 )]
            branco , 
            [ASN1EnumItem ( Name = "nulo", HasTag = true , Tag = 3 )]
            nulo , 
            [ASN1EnumItem ( Name = "legenda", HasTag = true , Tag = 4 )]
            legenda , 
            [ASN1EnumItem ( Name = "cargoSemCandidato", HasTag = true , Tag = 5 )]
            cargoSemCandidato
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

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(TipoVoto));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}