using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain
{
    public interface IMessageSender
    {
        void Send(Product product);
    }
}
