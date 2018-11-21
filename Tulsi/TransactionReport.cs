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
    public partial class TransactionReport : MetroFramework.Forms.MetroForm
    {
        private TransactionBLL transactions;
        private MedicineBLL medicine;

        public TransactionReport()
        {
            InitializeComponent();
            this.transactions = new TransactionBLL();
            this.medicine = new MedicineBLL();
        }

        private void TransactionReport_Load(object sender, EventArgs e)
        {
            //grdTransactionDetails.DataSource = transactions.GetAll();

            grdTransactionDetails.AutoGenerateColumns = false;
            grdTransactionDetails.Columns["Id"].DataPropertyName = "Id";
            grdTransactionDetails.Columns["Medicine"].DataPropertyName = "MedicineId";
            grdTransactionDetails.Columns["Amount"].DataPropertyName = "Amount";
            grdTransactionDetails.Columns["Profit"].DataPropertyName = "Profit";
            grdTransactionDetails.Columns["Date"].DataPropertyName = "Date";

            List<Transaction> transaction = new List<Transaction>();
            transaction = transactions.GetAll();
            grdTransactionDetails.DataSource = transaction;
        }
    }
}
