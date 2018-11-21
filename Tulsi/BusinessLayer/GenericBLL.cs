using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class GenericBLL
    {
        private DataAccess db;
        private GenericDAL generic;

        public GenericBLL()
        {
            this.db = new DataAccess();
            this.generic = new GenericDAL();
        }

        public Generic Get(string genericName)
        {
            return generic.Get(genericName);
        }

        public List<Medicine> Search(string genericName)
        {
            Generic g = new Generic();
            g.GenericName = genericName;
            return generic.Search(g);

        }

        public List<Generic> GetAll()
        {
            List<Generic> generics = new List<Generic>();
            generics = generic.GetAll();
            return generics;
        }
    }
}
