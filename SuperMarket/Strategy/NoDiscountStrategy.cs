using SuperMarket.DomainModel;
using SuperMarket.ValueObjects;

namespace SuperMarket.Strategy
{
    public class NoDiscountStrategy : DiscountStrategy
    {
        protected override Money ApplyDiscount(Money totalPrice, Product product, int quantity)
        {
            return totalPrice;
        }
    }
}
