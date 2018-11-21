using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class DesignationBLL
    {
        private DataAccess db;
        private DesignationDAL designation;

        public DesignationBLL()
        {
            this.db = new DataAccess();
            this.designation = new DesignationDAL();
        }

        public List<Designation> GetAll()
        {
            List<Designation> dlist = new List<Designation>();
            return dlist = designation.GetAll();
        }
    }
}
