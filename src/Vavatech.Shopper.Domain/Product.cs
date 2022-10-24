using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Owner { get; set; }
    }
}
