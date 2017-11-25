using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Discount
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        List<Product> Products { get; set; }
    }
}
