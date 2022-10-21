using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
    }
}
