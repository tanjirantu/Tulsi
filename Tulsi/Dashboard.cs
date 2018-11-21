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
    public partial class Dashboard : MetroFramework.Forms.MetroForm
    {
        private TransactionBLL transaction;
        private MedicineBLL medicine;

        public Dashboard()
        {
            InitializeComponent();
            this.transaction = new TransactionBLL();
            this.medicine = new MedicineBLL();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {          
            DateTime date = DateTime.Now;
            
            Transaction t = new Transaction();
            t = transaction.GetDailySales(date.ToString("yyy-MM-dd"));
            lblTransactionToday.Text = t.Amount.ToString();

            Medicine m = new Medicine();
            m = medicine.GetOrderableMedicinesQty();
            lblLowOnQty.Text = m.Qty.ToString();
        }

        private void lblTransactionToday_Click(object sender, EventArgs e)
        {

        }

        private void tileTransactionReport_Click(object sender, EventArgs e)
        {
            TransactionReport trans = new TransactionReport();
            trans.Show();
        }

        private void tileManageInventory_Click(object sender, EventArgs e)
        {
            ManageInventory mi = new ManageInventory();
            mi.Show();
        }
    }
}
