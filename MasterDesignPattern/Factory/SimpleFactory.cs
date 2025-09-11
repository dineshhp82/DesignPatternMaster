namespace MasterDesignPattern.Factory
{
    internal class SimpleFactory
    {
        public void SimulateSimpleFactory()
        {
            Console.WriteLine("---------Simple Factory------------");
            decimal amount = 100.00m;
            IPaymentFactory creditCardPayment = PaymentFactory.GetPaymentMethod("CreditCard");
            creditCardPayment.ProcessPayment(amount);
            IPaymentFactory paypalPayment = PaymentFactory.GetPaymentMethod("PayPal");
            paypalPayment.ProcessPayment(amount);
            IPaymentFactory debitCardPayment = PaymentFactory.GetPaymentMethod("DebitCard");
            debitCardPayment.ProcessPayment(amount);
        }
    }


    public interface IPaymentFactory
    {
        decimal ProcessPayment(decimal amount);
    }

    public class CreditCardPayment : IPaymentFactory
    {
        public decimal ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of {amount:C}");
            // Add logic for processing credit card payment
            return amount;
        }
    }

    public class PayPalPayment : IPaymentFactory
    {
        public decimal ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing PayPal payment of {amount:C}");
            // Add logic for processing PayPal payment
            return amount;
        }
    }


    public class DebitCardPayment : IPaymentFactory
    {
        public decimal ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing debit card payment of {amount:C}");
            // Add logic for processing debit card payment
            return amount;
        }
    }


    public class PaymentFactory
    {
        public static IPaymentFactory GetPaymentMethod(string method)
        {
            return method.ToLower() switch
            {
                "creditcard" => new CreditCardPayment(),
                "paypal" => new PayPalPayment(),
                "debitcard" => new DebitCardPayment(),
                _ => throw new NotSupportedException($"Payment method '{method}' is not supported.")
            };
        }
    }
}
