
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
    [ASN1BoxedType(Name = "NumeroSerieFlash")]
    public class NumeroSerieFlash: IASN1PreparedElement 
    {
    
        private byte[] val = null;

        [ASN1OctetString(Name = "NumeroSerieFlash")]
        
            [ASN1SizeConstraint ( Max = 4L )]
        
        public byte[] Value
        {
            get { return val; }
            set { val = value; }
        }
        
        public NumeroSerieFlash() 
        {
        }

        public NumeroSerieFlash(byte[] value) 
        {
            this.Value = value;
        }
        
        public NumeroSerieFlash(BitString value) 
        {
            this.Value = value.Value;
        }

        public void initWithDefaults()
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(NumeroSerieFlash));
        public IASN1PreparedElementData PreparedData {
            get { return preparedData; }
        }

    }
            
}