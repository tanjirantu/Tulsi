using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulsi.DataLayer
{
    public class DataAccess
    {
        private SqlConnection connection;

        public DataAccess()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TulsiDB"].ConnectionString;
            this.connection = new SqlConnection(connectionString);
        }

        public SqlConnection Conn
        {
            get { return this.connection; }
        }

        public void Close()
        {
            try
            {
                this.connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
