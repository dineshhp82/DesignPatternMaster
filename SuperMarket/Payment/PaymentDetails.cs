using SuperMarket.ValueObjects;

namespace SuperMarket.Payment
{
    // Base abstract class for payment details (Strategy pattern)
    public abstract class PaymentDetails
    {
        public Money Amount { get; protected set; }
        public DateTime PaymentDate { get; protected set; }
        public string PaymentMethod { get; protected set; }

        protected PaymentDetails(Money amount, string paymentMethod)
        {
            Amount = amount;
            PaymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));
            PaymentDate = DateTime.UtcNow;
        }

        public abstract bool Validate();
        public abstract string GetMaskedDetails();
    }
}
