
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
    [ASN1BoxedType(Name = "DataHoraJE")]
    public class DataHoraJE: IASN1PreparedElement 
    {

        private String val;

        [ASN1String(Name = "DataHoraJE", StringType = UniversalTags.GeneralString, IsUCS = false)]
        
            [ASN1SizeConstraint ( Max = 15L )]
        
        public String Value
        {
            get { return val; }
            set { val = value; }
        }
        
        public DataHoraJE() 
        {
        }

        public DataHoraJE(String val) 
        {
            this.val = val;
        }

        public void initWithDefaults() 
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DataHoraJE));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}