using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class GenericDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public GenericDAL()
        {
            this.db = new DataAccess();
        }

        public Generic Get(string genericName)
        {
            string sql = "SELECT * FROM Generics WHERE GenericName=@genericName";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@genericName", genericName);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Generic g = new Generic();
            if (reader.Read())
            {
                g.Id = Convert.ToInt32(reader["Id"]);
                g.GenericName = reader["GenericName"].ToString();
            }

            this.db.Conn.Close();
            return g;
        }

        public List<Generic> GetAll()
        {
            string sql = "SELECT * FROM Generics";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Generic> glist = new List<Generic>();
            while (reader.Read())
            {
                Generic g = new Generic();
                g.Id = Convert.ToInt32(reader["Id"]);
                g.GenericName = reader["GenericName"].ToString();

                glist.Add(g);
            }

            this.db.Close();
            return glist;
        }

        public List<Medicine> Search(Generic generic)
        {
            Generic g = new Generic();
            g = Get(generic.GenericName);

            string sql = "SELECT [Medicines].[Id], [Medicines].[BrandName], [Generics].[GenericName], [Formulations].[FormulationType], [Dosages].[DosageAmount], [Manufacturers].[CompanyName], [Medicines].[UnitPrice], [Medicines].[Quantity] "
                        + "FROM [Medicines] "
                        + "INNER JOIN [Generics] ON "
                        + "[Medicines].[GenericId] = [Generics].[Id] "
                        + "INNER JOIN [Formulations] ON "
                        + "[Medicines].[FormulationId] = [Formulations].[Id] "
                        + "INNER JOIN [Dosages] ON "
                        + "[Medicines].[DosageId] = [Dosages].[Id] "
                        + "INNER JOIN [Manufacturers] ON"
                        + "[Medicines].[ManufacturerId] = [Manufacturers].[Id]"
                        + "WHERE [Medicines].[GenericId] = @genericId";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@genericId", g.Id);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Medicine> mlist = new List<Medicine>();
            while (reader.Read())
            {
                Medicine m = new Medicine();

                m.Id = Convert.ToInt32(reader["Id"]);
                m.BrandName = reader["BrandName"].ToString();
                m.GenericName = reader["GenericName"].ToString();
                m.FormulationType = reader["FormulationType"].ToString();
                m.DosageAmount = reader["DosageAmount"].ToString();
                m.Manufacturer = reader["CompanyName"].ToString();
                m.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);

                mlist.Add(m);
            }

            this.db.Close();
            return mlist;
        }
    }
}
