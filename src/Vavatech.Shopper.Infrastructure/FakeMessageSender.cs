using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{
    public class FakeMessageSender : IMessageSender
    {
        private readonly ILogger<FakeMessageSender> logger;

        public FakeMessageSender(ILogger<FakeMessageSender> logger)
        {
            this.logger = logger;
        }

        public void Send(Product product)
        {
            logger.LogInformation($"Product {product.Name} was added.");
        }
    }
}
