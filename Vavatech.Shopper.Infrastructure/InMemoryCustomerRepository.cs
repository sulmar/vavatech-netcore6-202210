using System.Net.Http.Headers;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private readonly IDictionary<int, Customer> _customers;

        public InMemoryCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Smith", Salary = 1000 },
                new Customer { Id = 2, FirstName = "Kate", LastName = "Smith", Salary = 2000, Gender = Gender.Female },
                new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Salary = 3000 },
            };

            _customers = customers.ToDictionary(c => c.Id);
        }

        public void Add(Customer customer)
        {
            int id = _customers.Keys.Max();

            customer.Id = ++id;

            _customers.Add(customer.Id, customer);
        }

        public IEnumerable<Customer> Get()
        {
            return _customers.Values;
        }

        public Customer Get(int id)
        {
            _customers.TryGetValue(id, out Customer customer);

            return customer;
        }

        public void Remove(int id)
        {
            _customers.Remove(id);
        }

        public void Update(Customer customer)
        {
            int id = customer.Id;
            Remove(customer.Id);
            _customers.Add(id, customer);
        }

        
    }
}