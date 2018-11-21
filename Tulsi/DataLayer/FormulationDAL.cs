using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class FormulationDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public FormulationDAL()
        {
            this.db = new DataAccess();
        }

        public Formulation Get(string formulationType)
        {
            string sql = "SELECT * FROM Formulations WHERE FormulationType=@formulationType";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@formulationType", formulationType);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Formulation f = new Formulation();
            if (reader.Read())
            {
                f.Id = Convert.ToInt32(reader["Id"]);
                f.FormulationType = reader["FormulationType"].ToString();
            }

            this.db.Conn.Close();
            return f;
        }

        public List<Formulation> GetAll()
        {
            string sql = "SELECT * FROM Formulations";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Formulation> flist = new List<Formulation>();
            while (reader.Read())
            {
                Formulation f = new Formulation();
                f.Id = Convert.ToInt32(reader["Id"]);
                f.FormulationType = reader["FormulationType"].ToString();

                flist.Add(f);
            }

            this.db.Close();
            return flist;
        }

    }
}
