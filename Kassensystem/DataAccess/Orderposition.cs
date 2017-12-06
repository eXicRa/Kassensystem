using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    ////Johann,Nils
    public class Orderposition
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
