using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class ManufacturerBLL
    {
        private ManufacturerDAL manufacturer;

        public ManufacturerBLL()
        {
            this.manufacturer = new ManufacturerDAL();
        }

        public List<Manufacturer> GetAll()
        {
            return manufacturer.GetAll();
        }

        public Manufacturer Get(string p)
        {
            return manufacturer.Get(p);
        }
    }
}
