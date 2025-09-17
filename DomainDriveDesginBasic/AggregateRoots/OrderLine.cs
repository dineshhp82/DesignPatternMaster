using DomainDriveDesignBasic.Entities;
using DomainDriveDesignBasic.ValueObjects;

namespace DomainDriveDesignBasic.AggregateRoots
{
   
    //Value Object
    //No identity (OrderLineId not needed).
    //Two lines with same Product + Quantity are equal.
    //Perfect for immutability.

    public record OrderLine(Product Product, int Quantity)
    {
        public Product Product { get; init; } = Product ?? throw new ArgumentNullException(nameof(Product));
        public int Quantity { get; init; } = Quantity;

        public Money TotalPrice => Money.Multiply(Product.Price, Quantity);
    }
}
