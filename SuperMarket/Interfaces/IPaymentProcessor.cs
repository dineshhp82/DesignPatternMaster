using SuperMarket.DomainModel;
using SuperMarket.Payment;

namespace SuperMarket.Interfaces
{
    public interface IPaymentProcessor
    {
        Task<PaymentResult> ProcessPaymentAsync(Order order, PaymentDetails payment);
    }
}
