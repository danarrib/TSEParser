
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
    [ASN1Enum ( Name = "MotivoApuracaoEletronica")]
    public class MotivoApuracaoEletronica : IASN1PreparedElement 
    {
        public enum EnumType 
        {
            
            [ASN1EnumItem ( Name = "naoFoiPossivelReuperarResultado", HasTag = true , Tag = 1 )]
            naoFoiPossivelReuperarResultado , 
            [ASN1EnumItem ( Name = "urnaNaoChegouMidiaDefeituosa", HasTag = true , Tag = 2 )]
            urnaNaoChegouMidiaDefeituosa , 
            [ASN1EnumItem ( Name = "urnaNaoChegouMidiaExtraviada", HasTag = true , Tag = 3 )]
            urnaNaoChegouMidiaExtraviada , 
            [ASN1EnumItem ( Name = "outros", HasTag = true , Tag = 99 )]
            outros , 
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

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(MotivoApuracaoEletronica));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
