using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class MedicineBLL
    {
        private DataAccess db;
        private MedicineDAL medicine;

        public MedicineBLL()
        {
            this.db = new DataAccess();
            this.medicine = new MedicineDAL();
        }

        public Medicine Get(int id)
        {
            return medicine.Get();
        }

        public List<Medicine> GetAllDetails()
        {
            return medicine.GetAllDetails();
        }

        public List<Medicine> Search(string brandName)
        {
            Medicine m = new Medicine();
            m.BrandName = brandName;
            return medicine.Search(m);

        }

        public int Update(Medicine m)
        {
            return medicine.Update(m);
        }

        public Medicine GetOrderableMedicinesQty()
        {
            return medicine.GetOrdableMedicinesQty();
        }

        public int Insert(Medicine m)
        {
            return medicine.Insert(m);
        }
    }
}
