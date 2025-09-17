using SuperMarket.DomainModel;

namespace SuperMarket.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly Dictionary<Guid, Product> _products = new();
        private readonly object _lock = new object();

        public Task AddAsync(Product entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            lock (_lock)
            {
                _products[entity.Id] = _products.ContainsKey(entity.Id)
                    ? throw new InvalidOperationException($"Product with ID {entity.Id} already exists")
                    : entity;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            lock(_lock)
            {
                if(!_products.ContainsKey(id))
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }
                else
                {
                    _products.Remove(id);    
                }
            }

            return Task.CompletedTask;
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
           return Task.FromResult(_products.TryGetValue(id, out Product? value) ? value : null);
        }

        public Task UpdateAsync(Product entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            lock (_lock)
            {
                if (!_products.ContainsKey(entity.Id))
                {
                    throw new KeyNotFoundException($"Product with ID {entity.Id} not found");
                }
                else
                {
                    _products[entity.Id] = entity;
                }
            }

            return Task.CompletedTask;
        }
    }
}
