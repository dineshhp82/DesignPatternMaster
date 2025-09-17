using SuperMarket.DomainModel;
using SuperMarket.ValueObjects;

namespace SuperMarket.Interfaces
{
    public interface IPricingStrategy
    {
        Money CalculatePrice(Product product, int quantity);
    }
}
