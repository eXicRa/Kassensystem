using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{   //Johann,Nils
    public class Order
    {
        public int Id { get; set; }
        public int Taxes { get; set; }
        public DateTime Date { get; set; }
        public Employee Employee { get; set; }

    }
}
