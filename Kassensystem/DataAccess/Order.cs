﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class Order
    {
        public int Id { get; set; }
        DateTime Date { get; set; }
        Employee Employee{ get; set; }
    }
}
