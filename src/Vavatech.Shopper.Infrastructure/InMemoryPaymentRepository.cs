using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{
    public class InMemoryPaymentRepository : InMemoryEntityRepository<Payment>, IPaymentRepository
    {
        public InMemoryPaymentRepository()
        {
            var payments = new List<Payment>
            {
                new Payment { Id = 1, Amount = 1000 },
                new Payment { Id = 2, Amount = 2000 },
                new Payment { Id = 3, Amount = 4000 },
            };

            _entities = payments.ToDictionary(c => c.Id);
        }
    }
}