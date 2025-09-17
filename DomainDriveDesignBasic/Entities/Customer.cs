using DomainDriveDesignBasic.ValueObjects;

namespace DomainDriveDesignBasic.Entities
{
    public class Customer( CustomerId customerId, string name, Address address)
    {
        public CustomerId CustomerId { get; init; } = customerId;
        public string Name { get; init; } = name ?? throw new ArgumentNullException(nameof(name));
        public Address Address { get; init; } = address ?? throw new ArgumentNullException(nameof(address));
    }
}
