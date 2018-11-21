using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulsi.BusinessLayer;
using Tulsi.BusinessObject;

namespace Tulsi
{
    public partial class ManageInventory : MetroFramework.Forms.MetroForm
    {
        private GenericBLL generic;
        private FormulationBLL formulation;
        private DosageBLL dosage;
        private ManufacturerBLL manufacturer;
        private MedicineBLL medicine;

        public ManageInventory()
        {
            InitializeComponent();
            generic = new GenericBLL();
            formulation = new FormulationBLL();
            dosage = new DosageBLL();
            manufacturer = new ManufacturerBLL();
            medicine = new MedicineBLL();
        }

        private void ManageInventory_Load(object sender, EventArgs e)
        {
            populateCboGeneric();
            populateCboFormulation();
            populateCboDosage();
            populateCboManufacturer();

        }

        private void populateCboGeneric()
        {
            List<Generic> generics = new List<Generic>();
            generics = this.generic.GetAll();

            foreach (Generic g in generics)
            {
                cboGenericName.Items.Add(g.GenericName);
            }

        }

        private void populateCboFormulation()
        {
            FormulationBLL formulation = new FormulationBLL();
            List<Formulation> formulations = new List<Formulation>();
            formulations = formulation.GetAll();
            foreach (Formulation f in formulations)
            {
                cboFormulationType.Items.Add(f.FormulationType);
            }

        }

        private void populateCboDosage()
        {
            DosageBLL dosage = new DosageBLL();
            List<Dosage> dosages = new List<Dosage>();
            dosages = dosage.GetAll();
            foreach (Dosage d in dosages)
            {
                cboDosageAmount.Items.Add(d.DosageAmount);
            }

        }

        private void populateCboManufacturer()
        {
            ManufacturerBLL manufacturer = new ManufacturerBLL();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            manufacturers = manufacturer.GetAll();
            foreach (Manufacturer m in manufacturers)
            {
                cboManufacturer.Items.Add(m.CompanyName);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Generic g = new Generic();
            Formulation f = new Formulation();
            Dosage d = new Dosage();
            Manufacturer m = new Manufacturer();
            int quantity = Convert.ToInt32(txtQuantity.Text);
            double unitPrice = Convert.ToDouble(txtUnitPrice.Text);
            double sellingPrice = Convert.ToDouble(txtSellingPrice.Text);
            DateTime expireDate = Convert.ToDateTime(cboExpireDate.Text);

            expireDate.ToString("yyy-MM-dd");

            g = generic.Get(cboGenericName.Text);
            f = formulation.Get(cboFormulationType.Text);
            d = dosage.Get(cboDosageAmount.Text);
            m = manufacturer.Get(cboManufacturer.Text);

            if (!String.IsNullOrWhiteSpace(txtBrandName.Text) && !String.IsNullOrWhiteSpace(cboGenericName.Text)
                && !String.IsNullOrWhiteSpace(cboFormulationType.Text) && !String.IsNullOrWhiteSpace(cboDosageAmount.Text)
                && !String.IsNullOrWhiteSpace(cboExpireDate.Text) && !String.IsNullOrWhiteSpace(txtQuantity.Text)
                && !String.IsNullOrWhiteSpace(txtUnitPrice.Text) && !String.IsNullOrWhiteSpace(txtSellingPrice.Text))
            {
                Medicine med = new Medicine();
                med.BrandName = txtBrandName.Text;
                med.GenericId = g.Id;
                med.FormulationId = f.Id;
                med.DosageId = d.Id;
                med.ManufacturerId = m.Id;
                med.UnitPrice = unitPrice;
                med.SellingPrice = sellingPrice;
                med.Qty = quantity;
                med.ExpireDate = expireDate;
                int row = medicine.Insert(med);

                if (row > 0)
                {
                    lblMessage.Visible = true;
                }

                Console.WriteLine(g.Id);
                Console.WriteLine(f.Id);
                Console.WriteLine(d.Id);
                Console.WriteLine(m.Id);
                Console.WriteLine(quantity);
                Console.WriteLine(unitPrice);
                Console.WriteLine(sellingPrice);
            }
            

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
