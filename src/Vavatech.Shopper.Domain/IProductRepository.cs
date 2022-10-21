namespace Vavatech.Shopper.Domain
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> GetByColor(string color);
    }

}
