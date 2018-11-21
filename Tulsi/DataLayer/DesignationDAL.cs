using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class DesignationDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public DesignationDAL()
        {
            this.db = new DataAccess();
        }

        public List<Designation> GetAll()
        {
            string sql = "SELECT * FROM Designations";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Designation> dlist = new List<Designation>();
            while (reader.Read())
            {
                Designation d = new Designation();
                d.Id = Convert.ToInt32(reader["Id"]);
                d.Position = reader["Position"].ToString();
                dlist.Add(d);
            }
            this.db.Close();
            return dlist;
        }
    }
}
