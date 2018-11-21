using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class TransactionBLL
    {
        private DataAccess db;
        private TransactionDAL transaction;

        public TransactionBLL()
        {
            this.db = new DataAccess();
            this.transaction = new TransactionDAL();
        }

        public int Insert(Medicine m)
        {
            return transaction.Insert(m);
        }

        public Transaction GetDailySales(string date)
        {
            return transaction.GetDailySales(date);
        }

        public List<Transaction> GetAll()
        {
            return transaction.GetAll();
        }
    }
}
