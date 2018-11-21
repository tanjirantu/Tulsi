using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;
using Tulsi.DataLayer;

namespace Tulsi.BusinessLayer
{
    public class FormulationBLL
    {
        private FormulationDAL formulation;

        public FormulationBLL()
        {
            this.formulation = new FormulationDAL();
        }

        public List<Formulation> GetAll()
        {
            return formulation.GetAll();
        }

        public Formulation Get(string p)
        {
            return formulation.Get(p);
        }
    }
}
