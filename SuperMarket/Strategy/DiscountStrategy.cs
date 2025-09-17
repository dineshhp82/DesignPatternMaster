using SuperMarket.DomainModel;
using SuperMarket.Interfaces;
using SuperMarket.ValueObjects;

namespace SuperMarket.Strategy
{
    // Why we added this abstract class? 
    // To avoid code duplication in different discount strategies
    public abstract class DiscountStrategy : IPricingStrategy
    {
        // Template method pattern  
        public Money CalculatePrice(Product product, int quantity)
        {
            var basePrice = product.Price.Multiply(quantity);

            return ApplyDiscount(basePrice, product, quantity);
        }

        protected abstract Money ApplyDiscount(Money totalPrice, Product product, int quantity);
    }
}
