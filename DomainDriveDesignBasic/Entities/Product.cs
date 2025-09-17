using DomainDriveDesignBasic.ValueObjects;

namespace DomainDriveDesignBasic.Entities
{
    // This is entity because it contain identity
    public class Product(ProductId productId, string name, Money price,string currency)
    {
        public ProductId ProductId { get; init; } = productId;
        public string Name { get; init; } = name ?? throw new ArgumentNullException(nameof(name));
        public Money Price { get; init; } = price ?? throw new ArgumentNullException(nameof(price));
        public string Currency { get; init; } = currency ?? throw new ArgumentNullException(nameof(name));
    }
}
