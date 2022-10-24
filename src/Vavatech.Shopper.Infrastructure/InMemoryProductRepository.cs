using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{
    public class InMemoryProductRepository : InMemoryEntityRepository<Product>, IProductRepository
    {
        public InMemoryProductRepository()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Color = "Red", Price = 1000, Owner = "john" },
                new Product { Id = 2, Name = "Product 2", Color = "Green", Price = 2000, Owner = "john" },
                new Product { Id = 3, Name = "Product 3", Color = "Blue", Price = 4000, Owner = "kate" },
            };

            _entities = products.ToDictionary(c => c.Id);
        }

        public IEnumerable<Product> GetByColor(string color)
        {
            return _entities.Values.Where(p => p.Color == color);
        }
    }
}