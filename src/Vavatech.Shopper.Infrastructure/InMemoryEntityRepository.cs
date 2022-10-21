using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{
    // Klasa generyczna (szablon)
    public class InMemoryEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {

        protected IDictionary<int, TEntity> _entities;

        public InMemoryEntityRepository()
        {
            _entities = new Dictionary<int, TEntity>();
        }

        public void Add(TEntity entity)
        {
            int id = _entities.Keys.Max();

            entity.Id = ++id;

            _entities.Add(entity.Id, entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return _entities.Values;
        }

        public TEntity Get(int id)
        {
            _entities.TryGetValue(id, out TEntity entity);

            return entity;
        }

        public void Remove(int id)
        {
            _entities.Remove(id);
        }

        public void Update(TEntity entity)
        {
            int id = entity.Id;
            Remove(entity.Id);
            _entities.Add(id, entity);
        }
    }
}