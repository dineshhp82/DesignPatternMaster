using SuperMarket.ValueObjects;

namespace SuperMarket.DomainModel
{
    public class Order : Entity
    {
        public OrderId OrderId { get; }
        public Guid CustomerId { get; }
        public Address ShippingAddress { get; private set; }
        public Money TotalAmount { get; private set; }
        public DateTime OrderDate { get; }
        public OrderStatus Status { get; private set; }

        public Order(Guid customerId, Address shippingAddress)
        {
            OrderId = OrderId.New();
            CustomerId = customerId;
            ShippingAddress = shippingAddress;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            TotalAmount = Money.Zero;
        }
    }
}
