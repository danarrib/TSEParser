
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
    [ASN1Enum ( Name = "TipoArquivo")]
    public class TipoArquivo : IASN1PreparedElement 
    {
        public enum EnumType 
        {
            
            [ASN1EnumItem ( Name = "votacaoUE", HasTag = true , Tag = 1 )]
            votacaoUE , 
            [ASN1EnumItem ( Name = "votacaoRED", HasTag = true , Tag = 2 )]
            votacaoRED , 
            [ASN1EnumItem ( Name = "saMistaMRParcialCedula", HasTag = true , Tag = 3 )]
            saMistaMRParcialCedula , 
            [ASN1EnumItem ( Name = "saMistaBUImpressoCedula", HasTag = true , Tag = 4 )]
            saMistaBUImpressoCedula , 
            [ASN1EnumItem ( Name = "saManual", HasTag = true , Tag = 5 )]
            saManual , 
            [ASN1EnumItem ( Name = "saEletronica", HasTag = true , Tag = 6 )]
            saEletronica , 
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

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(TipoArquivo));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}