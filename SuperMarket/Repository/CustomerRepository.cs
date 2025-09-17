using SuperMarket.DomainModel;

namespace SuperMarket.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly Dictionary<Guid, Customer> _customers = new();
        private readonly object _lock = new object();

        public Task AddAsync(Customer entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            lock (_lock)
            {
                _customers[entity.Id] = _customers.ContainsKey(entity.Id)
                    ? throw new InvalidOperationException($"Customer with ID {entity.Id} already exists")
                    : entity;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            lock (_lock)
            {
                if (!_customers.ContainsKey(id))
                {
                    throw new KeyNotFoundException($"Customer with ID {id} not found");
                }
                else
                {
                    _customers.Remove(id);
                }
            }

            return Task.CompletedTask;
        }

        public Task<Customer?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_customers.TryGetValue(id, out Customer? value) ? value : null);
        }

        public Task UpdateAsync(Customer entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            lock (_lock)
            {
                if (!_customers.ContainsKey(entity.Id))
                {
                    throw new KeyNotFoundException($"Customer with ID {entity.Id} not found");
                }
                else
                {
                    _customers[entity.Id] = entity;
                }
            }

            return Task.CompletedTask;
        }
    }
}
