using DomainDriveDesignBasic.Entities;
using DomainDriveDesignBasic.ValueObjects;

namespace DomainDriveDesignBasic.AggregateRoots
{
    //This is also entity because it has identity
    //It is also the Aggregate Root of the Order Aggregate
    //Order is an Entity and Aggregate Root within the Domain Model."

    // DDD: Aggregate Root
    // - Main entry point for an aggregate.
    // - Enforces invariants and business rules for the aggregate.
    // - Controls access to child entities and value objects.

    public class Order
    {
        public OrderId OrderId { get; init; }
        public Customer Customer { get; init; }

        private readonly List<OrderLine> _orderLines = [];

        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();

        private Order(OrderId orderId, Customer customer)
        {
            OrderId = orderId;
            Customer = customer;
        }

        //why we’d pass a collection of (Product, Qty) tuples instead of a collection of OrderLine objects.
        /*
         The Order aggregate root is responsible for enforcing business rules (invariants) around OrderLines.
         If we let the caller pass in ready-made OrderLine objects, they could bypass rules.
         By only accepting raw data (Product + Qty), we force all OrderLine creation to go through the Order class, where rules are checked.
     
        👉 This ensures consistency.

        ❌ Bad Design: Passing OrderLine collection directly

        var orderLines = new List<OrderLine>
        {
            new OrderLine(productA, -5), // 🚨 Invalid Qty!
            new OrderLine(productB, 2)
        };

        var order = Order.CreateNew(customer, orderLines); 

        The caller can create an invalid OrderLine.
        The Order aggregate root never got the chance to enforce "Quantity > 0".
        
        It prevents the caller from creating invalid or inconsistent OrderLines.
        It makes the API closer to the real-world intent (customers order products & quantities, not “lines”).
         
         */
        public static Order Create(Customer customer, IEnumerable<(Product Product, int Quantity)> orderLineRequests)
        {
            if (customer == null) 
                throw new ArgumentNullException(nameof(customer));

            if (orderLineRequests == null || !orderLineRequests.Any())
                throw new InvalidOperationException("Order must have at least one line.");

            var order = new Order(OrderId.New(), customer);

            foreach (var req in orderLineRequests)
            {
                order.AddOrderLine(req.Product, req.Quantity); //Rules enforced here
            }

            return order;
        }

        public void AddOrderLine(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than zero.");

            _orderLines.Add(new OrderLine(product, quantity));
        }

        public void RemoveOrderLine(Product product)
        {
            var orderLine = _orderLines.FirstOrDefault(ol => ol.Product.ProductId == product.ProductId);
            if (orderLine == null)
            {
                throw new InvalidOperationException("Order line not found for the specified product.");
            }
            else
            {
                _orderLines.Remove(orderLine);
            }
        }

        public Money CalculateTotal()
        {
            var _totalAmount = _orderLines.Sum(r => r.TotalPrice.Amount);
            return Money.Create(_totalAmount, _orderLines.FirstOrDefault()?.Product.Currency);
        }
    }
}
