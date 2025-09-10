namespace MasterDesginPattern.Adapter
{
    internal class PaymentGateway
    {
        public void ProcessCheckOut()
        {
            var paymentGateway = new OurPaymentGateway();

            IPaymentProcessor paymentProcessor = new OurPaymentGatewayAdapter(paymentGateway);

            var checkoutService = new CheckoutService(paymentProcessor);

            checkoutService.Checkout(100, "INR");
        }
    }

    /*
     Suppose our company used 3rd party payment processor but after some time company decide to create out own payment processor
     becuase of costing of 3rd party.

     Now client does not want to change there code to accept new payment processor so to make new payment processor compatiable
     use adapter
     
     */

    //Client Used Service to handle the payment system or say some kind of orcherstrator
    class CheckoutService
    {
        private IPaymentProcessor paymentProcessor;

        public CheckoutService(IPaymentProcessor paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        public void Checkout(double amount, string currency)
        {
            Console.WriteLine($"CheckoutService: Attempting to process order for ${amount} {currency}");
            paymentProcessor.ProcessPayment(amount, currency);
            if (paymentProcessor.IsPaymentSuccessful())
            {
                Console.WriteLine($"CheckoutService: Order successful! Transaction ID: {paymentProcessor.GetTransactionId()}");
            }
            else
            {
                Console.WriteLine("CheckoutService: Order failed. Payment was not successful.");
            }
        }
    }

    //This is Target or client expacted interface
    interface IPaymentProcessor
    {
        void ProcessPayment(double amount, string currency);
        bool IsPaymentSuccessful();
        string GetTransactionId();
    }

    //adapter to accept our payment gateway
    class OurPaymentGatewayAdapter : IPaymentProcessor
    {
        private OurPaymentGateway legacyGateway;
        private long currentRef;

        public OurPaymentGatewayAdapter(OurPaymentGateway legacyGateway)
        {
            this.legacyGateway = legacyGateway;
        }

        public void ProcessPayment(double amount, string currency)
        {
            Console.WriteLine($"Adapter: Translating processPayment() for {amount} {currency}");
            legacyGateway.ExecuteTransaction(amount, currency);
            currentRef = legacyGateway.GetReferenceNumber();
        }

        public bool IsPaymentSuccessful()
        {
            return legacyGateway.CheckStatus(currentRef);
        }

        public string GetTransactionId()
        {
            return "LEGACY_TXN_" + currentRef;
        }
    }

    //Our payment Processor eariler we have 3rd party
    internal class OurPaymentGateway
    {
        private long transactionReference;
        private bool isPaymentSuccessfulFlag;

        public OurPaymentGateway()
        {
            transactionReference = 0;
            isPaymentSuccessfulFlag = false;
        }

        //Check status of client have enough fund to process
        public bool CheckStatus(long transactionReference)
        {
            Console.WriteLine($"LegacyGateway: Checking status for ref: {transactionReference}");
            return isPaymentSuccessfulFlag;
        }

        //Execute transaction and give a trans id
        public void ExecuteTransaction(double totalAmount, string currency)
        {
            Console.WriteLine($"LegacyGateway: Executing transaction for {currency} {totalAmount}");
            transactionReference = DateTimeOffset.Now.Ticks;
            isPaymentSuccessfulFlag = true;
            Console.WriteLine($"LegacyGateway: Transaction executed successfully. Txn ID: {transactionReference}");
        }


        //This just give the latest trans id
        public long GetReferenceNumber()
        {
            return transactionReference;
        }
    }
}
