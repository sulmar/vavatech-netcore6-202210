using System.Net.Http.Headers;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{

    public class InMemoryCustomerRepository : InMemoryEntityRepository<Customer>, ICustomerRepository
    {
        public InMemoryCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Smith", Salary = 1000  },
                new Customer { Id = 2, FirstName = "Kate", LastName = "Smith", Salary = 2000, Gender = Gender.Female },
                new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Salary = 3000 },
            };

            _entities = customers.ToDictionary(c => c.Id);
        }

        

        
    }
}