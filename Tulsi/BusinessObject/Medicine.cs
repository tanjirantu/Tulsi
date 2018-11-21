using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulsi.BusinessObject
{
    public class Medicine
    {
        private Generic generic;
        private Formulation formulation;
        private Dosage dosage;
        private Manufacturer manufacturer;

        public Medicine() 
        {
            this.generic = new Generic();
            this.formulation = new Formulation();
            this.dosage = new Dosage();
            this.manufacturer = new Manufacturer();
        }

        public Medicine(Generic g, Formulation f, Dosage d, Manufacturer m)
        {
            this.generic = g;
            this.formulation = f;
            this.dosage = d;
            this.manufacturer = m;
        }

        public int Id 
        { 
            get; 
            set; 
        }
        public string BrandName 
        { 
            get; 
            set; 
        }
        public int GenericId { get; set; }
        public string GenericName 
        {
            get { return this.generic.GenericName; }
            set { this.generic.GenericName = value; } 
        }
        public int FormulationId { get; set; }
        public string FormulationType
        {
            get { return this.formulation.FormulationType; }
            set { this.formulation.FormulationType= value; }
        }
        public int DosageId { get; set; }
        public string DosageAmount 
        {
            get { return this.dosage.DosageAmount; }
            set { this.dosage.DosageAmount = value; } 
        }
        public int ManufacturerId { get; set; }
        public string Manufacturer 
        {
            get { return this.manufacturer.CompanyName; }
            set { this.manufacturer.CompanyName = value; }
        }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Qty { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
