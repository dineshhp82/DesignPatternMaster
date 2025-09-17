using SuperMarket.ValueObjects;

namespace SuperMarket.DomainModel
{
    // Single Responsibility Principle
    public class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public string Category { get; private set; }

        public Product(string name, string description, Money price, int stockQuantity, string category)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
        }

        // In Domain model we can use methods to encapsulate business logic but logic only related to this domain model
        // no external depedency and and business logic that involves multiple domain models should be in a service
        // for example a method to update stock quantity
        public void UpdateStock(int quantity)
        {
            if (StockQuantity + quantity < 0)
                throw new InvalidOperationException("Insufficient stock");

            StockQuantity += quantity;
            MarkAsUpdated();
        }

        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice;
            MarkAsUpdated();
        }
    }
}
