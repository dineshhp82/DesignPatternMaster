using SuperMarket.Interfaces;
using SuperMarket.Repository;
using SuperMarket.Strategy;

namespace SuperMarket.Factories
{
    //Simple factory to create pricing strategies based on discount type
    public static class PricingStrategyFactory
    {
        public static IPricingStrategy Create(string discountType)
        {
            return discountType.ToLower() switch
            {
                "percentage" => new PercentageDiscountStrategy(0.1m), // 10% off
                "bulk" => new BulkDiscountStrategy(5, 0.15m), // 15% off for 5+ items
                "seasonal" => new SeasonalDiscountStrategy(0.3m),
                _ => new NoDiscountStrategy()
            };
        }
    }
}
