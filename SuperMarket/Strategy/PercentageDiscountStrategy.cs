using SuperMarket.DomainModel;
using SuperMarket.ValueObjects;

namespace SuperMarket.Strategy
{
    //Strategy Pattern for percentage based discount
    public class PercentageDiscountStrategy : DiscountStrategy
    {
        private readonly decimal _percentage;
        public PercentageDiscountStrategy(decimal percentage) => _percentage = percentage;

        protected override Money ApplyDiscount(Money totalPrice, Product product, int quantity)
        {
            var discountAmount = totalPrice.Multiply(_percentage);
            return totalPrice.Add(discountAmount.Negate());
        }
    }
}
