using SuperMarket.ValueObjects;

namespace SuperMarket.DomainModel
{
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }
        public Money UnitPrice { get; }
        public Money TotalPrice => UnitPrice.Multiply(Quantity);

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = product.Price;
        }
    }
}
