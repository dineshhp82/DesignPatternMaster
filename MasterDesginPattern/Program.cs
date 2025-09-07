using MasterDesginPattern.Adapter;

namespace MasterDesginPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            CurrencyDisplay  currencyDisplay =new CurrencyDisplay();
            currencyDisplay.DisplayBorkerRecordOnUI();

            PaymentGateway paymentGateway = new PaymentGateway();
            paymentGateway.ProcessCheckOut();

            Console.ReadLine();

        }
    }
}
