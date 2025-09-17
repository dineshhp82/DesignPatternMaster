using DomainDriveDesignBasic.AggregateRoots;
using DomainDriveDesignBasic.Entities;
using DomainDriveDesignBasic.ValueObjects;

namespace DomainDriveDesignBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Domain Driven Design");

            var product1 = new Product(ProductId.New(), "Laptop", Money.Create(1500, "USD"), "USD");
            var product2 = new Product(ProductId.New(), "Mouse", Money.Create(120, "USD"), "USD");

            var customer = new Customer(CustomerId.New(), "John Doe", new Address("123 Main St", "NYC", "USA"));

            var order = Order.Create(customer, new List<(Product, int)>
            {
                (product1,1),
                (product2,2)
            });

            Console.WriteLine($"Order Total: {order.CalculateTotal().Amount} {order.CalculateTotal().Currency}");

            foreach (var item in order.OrderLines)
            {
                Console.WriteLine($"{item.Product.ProductId}-{item.Product.Name}");
            }

            Console.ReadLine();
        }
    }
}
