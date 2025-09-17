namespace SuperMarket.Payment
{
    public class PaymentResult
    {
        public bool Success { get; }
        public string TransactionId { get; }
        public string ErrorMessage { get; }
        public DateTime ProcessedAt { get; }

        public PaymentResult(bool success, string transactionId = null, string errorMessage = null)
        {
            Success = success;
            TransactionId = transactionId;
            ErrorMessage = errorMessage;
            ProcessedAt = DateTime.UtcNow;
        }

        // Factory methods for clarity 
        public static PaymentResult Successful(string transactionId) =>
            new PaymentResult(true, transactionId);

        //Factory method for clarity
        // We can also use static methods to create instances of the class in a more readable way
        public static PaymentResult Failed(string errorMessage) =>
            new PaymentResult(false, null, errorMessage);
    }
}
