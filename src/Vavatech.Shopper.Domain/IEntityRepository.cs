namespace Vavatech.Shopper.Domain
{
    // Interfejs ogólniony (szablon)
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }

   

}
