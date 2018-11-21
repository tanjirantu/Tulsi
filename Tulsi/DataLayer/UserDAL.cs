using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class UserDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public UserDAL()
        {
            this.db = new DataAccess();
        }

        public User Get(string username)
        {
            string sql = "SELECT * FROM Users WHERE Username=@username";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@username", username);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            User u = new User();
            if (reader.Read())
            {
                u.Id = Convert.ToInt32(reader["Id"]);
                u.FullName = reader["FullName"].ToString();
                u.DesignationId = Convert.ToInt32(reader["DesignationId"]);
                u.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
            }

            this.db.Conn.Close();
            return u;
        }

        public int Insert(User user)
        {
            string sql = "INSERT INTO Users (FullName, Username, Password, DesignationId, JoiningDate) VALUES (@fullName, @username, @password, @designationId, @joiningDate)";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@fullName", user.FullName);
            this.command.Parameters.AddWithValue("@username", user.Username);
            this.command.Parameters.AddWithValue("@password", user.Password);
            this.command.Parameters.AddWithValue("@designationId", user.DesignationId);
            this.command.Parameters.AddWithValue("@joiningDate", user.JoiningDate);
            this.db.Conn.Open();
            int affectedRows = this.command.ExecuteNonQuery();
            this.db.Conn.Close();
            return affectedRows;
        }

        public List<User> GetAll()
        {
            string sql = "SELECT * FROM Users";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<User> ulist = new List<User>();
            while (reader.Read())
            {
                User u = new User();
                u.Id = Convert.ToInt32(reader["Id"]);
                u.FullName = reader["FullName"].ToString();
                u.DesignationId = Convert.ToInt32(reader["DesignationId"]);
                u.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);

                ulist.Add(u);
            }

            this.db.Close();
            return ulist;
        }

        public User Login(User user)
        {
            string sql = "SELECT * FROM Users WHERE Username=@username AND Password=@password";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@username", user.Username);
            this.command.Parameters.AddWithValue("@password", user.Password);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            User u = new User();
            if (reader.Read())
            {
                u.Id = Convert.ToInt32(reader["Id"]);
                u.FullName = reader["FullName"].ToString();
                u.DesignationId = Convert.ToInt32(reader["DesignationId"]);
                u.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
            }

            this.db.Conn.Close();
            return u;
        }

        
    }
}
