using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class DosageDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public DosageDAL()
        {
            this.db = new DataAccess();
        }

        public Dosage Get(string dosageAmount)
        {
            string sql = "SELECT * FROM Dosages WHERE DosageAmount=@dosageAmount";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@dosageAmount", dosageAmount);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Dosage d = new Dosage();
            if (reader.Read())
            {
                d.Id = Convert.ToInt32(reader["Id"]);
                d.DosageAmount = reader["DosageAmount"].ToString();
            }

            this.db.Conn.Close();
            return d;
        }

        public List<Dosage> GetAll()
        {
            string sql = "SELECT * FROM Dosages";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Dosage> dlist = new List<Dosage>();
            while (reader.Read())
            {
                Dosage d = new Dosage();
                d.Id = Convert.ToInt32(reader["Id"]);
                d.DosageAmount = reader["DosageAmount"].ToString();
                dlist.Add(d);
            }

            this.db.Close();
            return dlist;
        }
    }
}
