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
    public partial class POS : MetroFramework.Forms.MetroForm
    {
        private MedicineBLL medicine;
        private GenericBLL generic;
        private BindingList<Medicine> medicines;

        public POS()
        {
            InitializeComponent();
            this.medicine = new MedicineBLL();
            this.generic = new GenericBLL();
            this.medicines = new BindingList<Medicine>();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            txtQty.Text = "1";
            txtDiscount.Text = "0";
            //populateCboFormulation();
            //populateCboDosage();
            //populateCboManufacturer();
        }

        //private void populateCboFormulation()
        //{
        //    FormulationBLL formulation = new FormulationBLL();
        //    List<Formulation> formulations = new List<Formulation>();
        //    formulations = formulation.GetAll();
        //    foreach (Formulation f in formulations)
        //    {
        //        cboFormulation.Items.Add(f.FormulationType);
        //    }

        //}

        //private void populateCboDosage()
        //{
        //    DosageBLL dosage = new DosageBLL();
        //    List<Dosage> dosages = new List<Dosage>();
        //    dosages = dosage.GetAll();
        //    foreach (Dosage d in dosages)
        //    {
        //        cboDosage.Items.Add(d.DosageAmount);
        //    }

        //}

        //private void populateCboManufacturer()
        //{
        //    ManufacturerBLL manufacturer = new ManufacturerBLL();
        //    List<Manufacturer> manufacturers = new List<Manufacturer>();
        //    manufacturers = manufacturer.GetAll();
        //    foreach (Manufacturer m in manufacturers)
        //    {
        //        cboManufacturer.Items.Add(m.CompanyName);
        //    }

        //}

        private void metroContextMenu1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void lnkHelp_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Medicine> medicines = new List<Medicine>();
            medicines = medicine.GetAllDetails();
     
            if (String.IsNullOrWhiteSpace(cboSearchBy.Text) && String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                grdMedicineDetails.AutoGenerateColumns = false;
                grdMedicineDetails.Columns["Id"].DataPropertyName = "Id";
                grdMedicineDetails.Columns["BrandName"].DataPropertyName = "BrandName";
                grdMedicineDetails.Columns["GenericName"].DataPropertyName = "GenericName";
                grdMedicineDetails.Columns["FormulationType"].DataPropertyName = "FormulationType";
                grdMedicineDetails.Columns["DosageAmount"].DataPropertyName = "DosageAmount";
                grdMedicineDetails.Columns["UnitPrice"].DataPropertyName = "UnitPrice";
                grdMedicineDetails.Columns["UnitPrice"].Visible = false;
                grdMedicineDetails.Columns["SellingPrice"].DataPropertyName = "Price";
                grdMedicineDetails.Columns["Manufacturer"].DataPropertyName = "Manufacturer";

                grdMedicineDetails.DataSource = medicines.Select(m => new 
                                                                { 
                                                                    Id = m.Id,
                                                                    BrandName = m.BrandName, 
                                                                    GenericName = m.GenericName ,
                                                                    FormulationType = m.FormulationType,
                                                                    DosageAmount = m.DosageAmount,
                                                                    UnitPrice = m.UnitPrice,
                                                                    Price = m.SellingPrice,
                                                                    Manufacturer = m.Manufacturer

                                                                }).ToList();
            }
            else
            {
                switch (cboSearchBy.SelectedIndex)
                {
                    case 0:
                        grdMedicineDetails.DataSource = generic.Search(txtSearch.Text);
                        break;
                    case 1:
                        grdMedicineDetails.DataSource = medicine.Search(txtSearch.Text);
                        break;
                }
            }            
        }

        static int s = 0;
        static int t = 0;
        private void grdMedicineDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //object value = grdMedicineDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            Medicine m = new Medicine();
            m.Id = Convert.ToInt32(grdMedicineDetails.Rows[e.RowIndex].Cells["Id"].FormattedValue);
            m.BrandName = grdMedicineDetails.Rows[e.RowIndex].Cells["BrandName"].FormattedValue.ToString();
            m.FormulationType = grdMedicineDetails.Rows[e.RowIndex].Cells["FormulationType"].FormattedValue.ToString();
            m.GenericName = grdMedicineDetails.Rows[e.RowIndex].Cells["GenericName"].FormattedValue.ToString();
            m.DosageAmount = grdMedicineDetails.Rows[e.RowIndex].Cells["DosageAmount"].FormattedValue.ToString();
            m.Manufacturer = grdMedicineDetails.Rows[e.RowIndex].Cells["Manufacturer"].FormattedValue.ToString();
            m.UnitPrice = Convert.ToDouble(grdMedicineDetails.Rows[e.RowIndex].Cells["UnitPrice"].FormattedValue) * Convert.ToInt32(txtQty.Text);
            m.SellingPrice = Convert.ToDouble(grdMedicineDetails.Rows[e.RowIndex].Cells["SellingPrice"].FormattedValue) * Convert.ToInt32(txtQty.Text);

            m.Qty = Convert.ToInt32(txtQty.Text);

            lblItemName.SetBounds(254, 8, 36, 19);
            lblItemName.Text = m.BrandName.ToString();
            lblPrice.SetBounds(254, 8, 36, 19);
            lblPrice.Text = m.SellingPrice.ToString();

            s += Convert.ToInt32(m.SellingPrice);
            lblSubtotal.Text = s.ToString();
            t = s - Convert.ToInt32(txtDiscount.Text);
            lblTotal.Text = t.ToString();

            calculateInvoice(medicines);
            grdPurchasedItems_AddMedicines(m);
        }


        

        public void calculateInvoice(BindingList<Medicine> mlist)
        {
            //double price = 0.0;
            double subtotal = 0.0;
            double total = 0.0;
            foreach(Medicine medicine in mlist)
            {
                subtotal += Convert.ToInt32(medicine.SellingPrice);
                total = subtotal - Convert.ToInt32(txtDiscount.Text);
            }           
        }

        private void grdPurchasedItems_AddMedicines(Medicine m)
        {
            grdPurchasedItems.DataSource = null;
            medicines.Add(m);
            grdPurchasedItems.AutoGenerateColumns = false;
            grdPurchasedItems.Columns["pId"].DataPropertyName = "Id";
            grdPurchasedItems.Columns["pBrandName"].DataPropertyName = "BrandName";
            grdPurchasedItems.Columns["pGenericName"].DataPropertyName = "GenericName";
            grdPurchasedItems.Columns["pFormulationType"].DataPropertyName = "FormulationType";
            grdPurchasedItems.Columns["pDosageAmount"].DataPropertyName = "DosageAmount";
            grdPurchasedItems.Columns["pSellingPrice"].DataPropertyName = "SellingPrice";
            grdPurchasedItems.Columns["pManufacturer"].DataPropertyName = "Manufacturer";

            
            grdPurchasedItems.DataSource = medicines;
            

        }

        private void grdPurchasedItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow item in this.grdPurchasedItems.SelectedRows)
            {
                grdPurchasedItems.Rows.RemoveAt(item.Index);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            foreach (Medicine medicine in medicines)
            {
                Console.WriteLine(medicine.Qty);
            }

            Receipt rc = new Receipt();
            if (rc.CalculateBill(medicines, Convert.ToInt32(txtDiscount.Text)))
            {
                rc.Show();
                grdPurchasedItems.DataSource = null;
            } 

            txtDiscount.Text = "0";
            
        }


        //private void cboFormulation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Console.WriteLine(cboFormulation.SelectedIndex+1);
        //}

        //private void cboDosage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Console.WriteLine(cboDosage.SelectedIndex + 1);
        //}

        //private void cboManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Console.WriteLine(cboManufacturer.SelectedIndex + 1);
        //}
    }
}
