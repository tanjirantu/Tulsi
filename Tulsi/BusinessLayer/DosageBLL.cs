using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class DosageBLL
    {
        private DosageDAL dosage;

        public DosageBLL()
        {
            this.dosage = new DosageDAL();
        }

        public List<Dosage> GetAll()
        {
            return dosage.GetAll();
        }

        public Dosage Get(string p)
        {
            return dosage.Get(p);
        }
    }
}
