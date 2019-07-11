using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Entity_framework_core.Models
{
    public class Order
    {
        public long Id { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string ProductNumber { get; set; }

    }

    
}
