using SuperMarket.ValueObjects;

namespace SuperMarket.Payment
{
    public class PayPalPayment : PaymentDetails
    {
        public string Email { get; private set; }
        public string TransactionId { get; private set; }

        public PayPalPayment(Money amount, string email)
            : base(amount, "PayPal")
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            TransactionId = GenerateTransactionId();
        }

        private string GenerateTransactionId()
        {
            return $"PP-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N").Substring(0, 8)}";
        }

        public override string GetMaskedDetails()
        {
            var maskedEmail = Email; //MaskEmail(Email);
            return $"PayPal: {maskedEmail}, Transaction: {TransactionId}";
        }

        public override bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Email)) //|| !IsValidEmail(Email))
                return false;

            if (Amount.Amount <= 0)
                return false;

            return true;
        }
    }
}
