
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
    [ASN1Choice(Name = "DadosSecaoSA")]
    public class DadosSecaoSA : IASN1PreparedElement 
    {
        
        private DadosSecao dadosSecao_;
        private bool  dadosSecao_selected = false;

        
		[ASN1Element(Name = "dadosSecao", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public DadosSecao DadosSecao
        {
            get { return dadosSecao_; }
            set { selectDadosSecao(value); }
        }
  
        private DadosSA dadosSA_;
        private bool  dadosSA_selected = false;

        
		[ASN1Element(Name = "dadosSA", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public DadosSA DadosSA
        {
            get { return dadosSA_; }
            set { selectDadosSA(value); }
        }
  
        public bool isDadosSecaoSelected()
        {
            return this.dadosSecao_selected;
        }

        

        public void selectDadosSecao (DadosSecao val) 
        {
            this.dadosSecao_ = val;
            this.dadosSecao_selected = true;
            
            this.dadosSA_selected = false;
            
        }
  
        public bool isDadosSASelected()
        {
            return this.dadosSA_selected;
        }

        

        public void selectDadosSA (DadosSA val) 
        {
            this.dadosSA_ = val;
            this.dadosSA_selected = true;
            
            this.dadosSecao_selected = false;
            
        }
  

        public void initWithDefaults()
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DadosSecaoSA));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}