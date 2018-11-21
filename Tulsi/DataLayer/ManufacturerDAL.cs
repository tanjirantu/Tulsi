using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class ManufacturerDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public ManufacturerDAL()
        {
            this.db = new DataAccess();
        }

        public List<Manufacturer> GetAll()
        {
            string sql = "SELECT * FROM Manufacturers";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Manufacturer> mlist = new List<Manufacturer>();
            while (reader.Read())
            {
                Manufacturer m = new Manufacturer();
                m.Id = Convert.ToInt32(reader["Id"]);
                m.CompanyName = reader["CompanyName"].ToString();
                mlist.Add(m);
            }

            this.db.Close();
            return mlist;
        }

        public Manufacturer Get(string manufacturerName)
        {
            string sql = "SELECT * FROM Manufacturers WHERE CompanyName=@manufacturerName";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@manufacturerName", manufacturerName);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Manufacturer m = new Manufacturer();
            if (reader.Read())
            {
                m.Id = Convert.ToInt32(reader["Id"]);
                m.CompanyName = reader["CompanyName"].ToString();
            }

            this.db.Conn.Close();
            return m;
        }
    }
}
