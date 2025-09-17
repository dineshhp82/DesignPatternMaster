using SuperMarket.DomainModel;

namespace SuperMarket.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly Dictionary<Guid, Order> _orders = new();
        private readonly object _lock = new object();

        public Task AddAsync(Order entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            lock (_lock)
            {
                _orders[entity.Id] = _orders.ContainsKey(entity.Id)
                    ? throw new InvalidOperationException($"Order with ID {entity.Id} already exists")
                    : entity;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            lock (_lock)
            {
                if (!_orders.ContainsKey(id))
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }
                else
                {
                    _orders.Remove(id);
                }
            }

            return Task.CompletedTask;
        }

        public Task<Order?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_orders.TryGetValue(id, out Order? value) ? value : null);
        }

        public Task UpdateAsync(Order entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            lock (_lock)
            {
                if (!_orders.ContainsKey(entity.Id))
                {
                    throw new KeyNotFoundException($"Order with ID {entity.Id} not found");
                }
                else
                {
                    _orders[entity.Id] = entity;
                }
            }

            return Task.CompletedTask;
        }
    }
}
