using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Productgroup
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
