using SuperMarket.DomainModel;
using SuperMarket.Interfaces;

namespace SuperMarket.Payment
{
    //Decorator Pattern
    public class LoggingPaymentProcessor(IPaymentProcessor innerProcessor, ILogger logger) : IPaymentProcessor
    {
        private readonly IPaymentProcessor _innerProcessor = innerProcessor;
        private readonly ILogger _logger = logger;

        public async Task<PaymentResult> ProcessPaymentAsync(Order order, PaymentDetails payment)
        {
            _logger.LogInfo($"Processing payment for Order ID: {order.Id}");
            var result = await _innerProcessor.ProcessPaymentAsync(order, payment);
            _logger.LogInfo($"Payment processed: {result.Success}");
            return result;
        }
    }
}
