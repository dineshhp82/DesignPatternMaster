using SuperMarket.DomainModel;
using SuperMarket.ValueObjects;

namespace SuperMarket.Strategy
{
    public class SeasonalDiscountStrategy : DiscountStrategy
    {
        private readonly decimal _seasonalRate;

        public SeasonalDiscountStrategy(decimal seasonalRate) => _seasonalRate = seasonalRate;

        protected override Money ApplyDiscount(Money totalPrice, Product product, int quantity)
        {
            var discountAmount = totalPrice.Multiply(_seasonalRate);
            return totalPrice.Add(discountAmount.Negate());
        }
    }
}
