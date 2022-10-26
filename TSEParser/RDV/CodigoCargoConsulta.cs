
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
    [ASN1Choice(Name = "CodigoCargoConsulta")]
    public class CodigoCargoConsulta : IASN1PreparedElement 
    {
        
        private CargoConstitucional cargoConstitucional_;
        private bool  cargoConstitucional_selected = false;

        
		[ASN1Element(Name = "cargoConstitucional", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public CargoConstitucional CargoConstitucional
        {
            get { return cargoConstitucional_; }
            set { selectCargoConstitucional(value); }
        }
  
        private NumeroCargoConsultaLivre numeroCargoConsultaLivre_;
        private bool  numeroCargoConsultaLivre_selected = false;

        
		[ASN1Element(Name = "numeroCargoConsultaLivre", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public NumeroCargoConsultaLivre NumeroCargoConsultaLivre
        {
            get { return numeroCargoConsultaLivre_; }
            set { selectNumeroCargoConsultaLivre(value); }
        }
  
        public bool isCargoConstitucionalSelected()
        {
            return this.cargoConstitucional_selected;
        }

        

        public void selectCargoConstitucional (CargoConstitucional val) 
        {
            this.cargoConstitucional_ = val;
            this.cargoConstitucional_selected = true;
            
            this.numeroCargoConsultaLivre_selected = false;
            
        }
  
        public bool isNumeroCargoConsultaLivreSelected()
        {
            return this.numeroCargoConsultaLivre_selected;
        }

        

        public void selectNumeroCargoConsultaLivre (NumeroCargoConsultaLivre val) 
        {
            this.numeroCargoConsultaLivre_ = val;
            this.numeroCargoConsultaLivre_selected = true;
            
            this.cargoConstitucional_selected = false;
            
        }
  

        public void initWithDefaults()
        {
        }

        private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CodigoCargoConsulta));
        public IASN1PreparedElementData PreparedData 
        {
            get { return preparedData; }
        }

    }
            
}