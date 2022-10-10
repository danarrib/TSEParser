
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
    [ASN1Choice(Name = "TipoApuracaoSA")]
    public class TipoApuracaoSA : IASN1PreparedElement 
    {
        
        private ApuracaoMistaMR apuracaoMistaMR_;
        private bool  apuracaoMistaMR_selected = false;

        
		[ASN1Element(Name = "apuracaoMistaMR", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ApuracaoMistaMR ApuracaoMistaMR
        {
            get { return apuracaoMistaMR_; }
            set { selectApuracaoMistaMR(value); }
        }
  
        private ApuracaoMistaBUAE apuracaoMistaBUAE_;
        private bool  apuracaoMistaBUAE_selected = false;

        
		[ASN1Element(Name = "apuracaoMistaBUAE", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ApuracaoMistaBUAE ApuracaoMistaBUAE
        {
            get { return apuracaoMistaBUAE_; }
            set { selectApuracaoMistaBUAE(value); }
        }
  
        private ApuracaoTotalmenteManualDigitacaoAE apuracaoTotalmenteManual_;
        private bool  apuracaoTotalmenteManual_selected = false;

        
		[ASN1Element(Name = "apuracaoTotalmenteManual", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public ApuracaoTotalmenteManualDigitacaoAE ApuracaoTotalmenteManual
        {
            get { return apuracaoTotalmenteManual_; }
            set { selectApuracaoTotalmenteManual(value); }
        }
  
        private ApuracaoEletronica apuracaoEletronica_;
        private bool  apuracaoEletronica_selected = false;

        
		[ASN1Element(Name = "apuracaoEletronica", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public ApuracaoEletronica ApuracaoEletronica
        {
            get { return apuracaoEletronica_; }
            set { selectApuracaoEletronica(value); }
        }
  
        public bool isApuracaoMistaMRSelected()
        {
            return this.apuracaoMistaMR_selected;
        }

        

        public void selectApuracaoMistaMR (ApuracaoMistaMR val) 
        {
            this.apuracaoMistaMR_ = val;
            this.apuracaoMistaMR_selected = true;
            
            this.apuracaoMistaBUAE_selected = false;
            
            this.apuracaoTotalmenteManual_selected = false;
            
            this.apuracaoEletronica_selected = false;
            
        }
  
        public bool isApuracaoMistaBUAESelected()
        {
            return this.apuracaoMistaBUAE_selected;
        }

        

        public void selectApuracaoMistaBUAE (ApuracaoMistaBUAE val) 
        {
            this.apuracaoMistaBUAE_ = val;
            this.apuracaoMistaBUAE_selected = true;
            
            this.apuracaoMistaMR_selected = false;
            
            this.apuracaoTotalmenteManual_selected = false;
            
            this.apuracaoEletronica_selected = false;
            
        }
  
        public bool isApuracaoTotalmenteManualSelected()
        {
            return this.apuracaoTotalmenteManual_selected;
        }

        

        public void selectApuracaoTotalmenteManual (ApuracaoTotalmenteManualDigitacaoAE val) 
        {
            this.apuracaoTotalmenteManual_ = val;
            this.apuracaoTotalmenteManual_selected = true;
            
            this.apuracaoMistaMR_selected = false;
            
            this.apuracaoMistaBUAE_selected = false;
            
            this.apuracaoEletronica_selected = false;
            
        }
  
        public bool isApuracaoEletronicaSelected()
        {
            return this.apuracaoEletronica_selected;
        }

        

        public void selectApuracaoEletronica (ApuracaoEletronica val) 
        {
            this.apuracaoEletronica_ = val;
            this.apuracaoEletronica_selected = true;
            
            this.apuracaoMistaMR_selected = false;
            
            this.apuracaoMistaBUAE_selected = false;
            
            this.apuracaoTotalmenteManual_selected = false;
            
        }
  

        public void initWithDefaults()
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(TipoApuracaoSA));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}
