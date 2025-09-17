using SuperMarket.DomainModel;
using SuperMarket.ValueObjects;

namespace SuperMarket.Strategy
{
    public class BulkDiscountStrategy(int minQuantity, decimal percentage) : DiscountStrategy
    {
        private readonly int _minQuantity = minQuantity;
        private readonly decimal _percentage = percentage;

        protected override Money ApplyDiscount(Money totalPrice, Product product, int quantity)
        {
            return quantity >= _minQuantity
                ? totalPrice.Add(totalPrice.Multiply(_percentage).Negate())
                : totalPrice;
        }
    }
}
