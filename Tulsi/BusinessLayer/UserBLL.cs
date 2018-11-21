using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class UserBLL
    {
        private DataAccess db;
        private UserDAL user;

        public UserBLL()
        {
            this.db = new DataAccess();
            this.user = new UserDAL();
        }

        public User Get(string username)
        {
            User u = new User();
            return user.Get(username);
        }

        public List<User> GetAll()
        {
            List<User> ulist = new List<User>();
            return ulist = user.GetAll();
        }

        public bool Insert(User u)
        {
            if (user.Insert(u) > 0)
                return true;
            return false;
        }

        public User Login(User u)
        {
            return user.Login(u);
        }

        // check for same username
        public bool UserExist(User u)
        {
            if (user.Get(u.Username) == null)
                return true;
            return false;
        }

        public bool ValidatePassword(User user)
        {
            if (String.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
                return false;
            return true;
        }

        public bool ValidateUser(User user)
        {

            if (!UserExist(user) && ValidatePassword(user)) return true;
            return false;
        }


        
    }
}
