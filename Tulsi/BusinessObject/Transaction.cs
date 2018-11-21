using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulsi.BusinessObject
{
    public class Transaction
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public double Amount { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }

    }
}
