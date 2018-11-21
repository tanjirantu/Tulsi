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
    public partial class Receipt : MetroFramework.Forms.MetroForm
    {
        public BindingList<Medicine> medicines;

        public Receipt()
        {
            InitializeComponent();
            this.medicines = new BindingList<Medicine>();
        }

        private void Receipt_Load(object sender, EventArgs e)
        {

        }

        public bool CalculateBill(BindingList<Medicine> mlist, double discount)
        {
            this.medicines = mlist;
            double subtotal = 0;
            double total = 0;
            lblItem.SetBounds(23, 176, 31, 19);
            foreach (Medicine medicine in mlist)
            {
                subtotal += Convert.ToInt32(medicine.SellingPrice);
                total = subtotal - discount;
                lblItem.Text += medicine.BrandName + "\n";
                lblItemPrice.Text += medicine.SellingPrice + "\n";
                Console.WriteLine(medicine.SellingPrice - medicine.UnitPrice);
                
            }

            lblTotalPrice.SetBounds(311, 367, 33, 19);
            lblItemPrice.SetBounds(311, 176, 38, 19);
            lblTotalPrice.Text = total.ToString();
            subtotal = 0;
            total = 0;
            return true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            MedicineBLL med = new MedicineBLL();
            TransactionBLL tran = new TransactionBLL();

            foreach(Medicine m in medicines)
            {
                tran.Insert(m);
                med.Update(m);
            }
        }
    }
}
