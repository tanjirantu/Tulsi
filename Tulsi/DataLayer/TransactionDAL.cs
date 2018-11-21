using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class TransactionDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public TransactionDAL()
        {
            this.db = new DataAccess();
        }

        public List<Transaction> GetAll()
        {
            string sql = "SELECT * FROM Transactions";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Transaction> tlist = new List<Transaction>();
            while (reader.Read())
            {
                Transaction t = new Transaction();
                t.Id = Convert.ToInt32(reader["Id"]);
                t.MedicineId = Convert.ToInt32(reader["MedicineId"]);
                t.Amount = Convert.ToDouble(reader["Amount"]);
                t.Profit = Convert.ToDouble(reader["Profit"]);
                t.Date = Convert.ToDateTime(reader["Date"]);
                
                tlist.Add(t);
            }

            this.db.Close();
            return tlist;
        }

        public int Insert(Medicine m)
        {
            DateTime date = DateTime.Now;
            string sql = "INSERT INTO Transactions (MedicineId, Amount, Profit, Date) VALUES (@medicineId, @amount, @profit, @dateTime)";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@medicineId", m.Id);
            this.command.Parameters.AddWithValue("@amount", m.UnitPrice);
            this.command.Parameters.AddWithValue("@profit", m.SellingPrice - m.UnitPrice);
            this.command.Parameters.AddWithValue("@dateTime", date);
            this.db.Conn.Open();
            int affectedRows = this.command.ExecuteNonQuery();
            this.db.Close();
            return affectedRows;
        }

        public Transaction GetDailySales(string date)
        {
            string sql = "SELECT SUM(Amount) AS Amount FROM [Transactions] WHERE Date=@date GROUP BY [Date] ORDER BY [Date]";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@date", date);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Transaction t = new Transaction();
            if (reader.Read())
            {
                t.Amount = Convert.ToDouble(reader["Amount"]);
            }

            this.db.Conn.Close();
            return t;
        }
    }
}
