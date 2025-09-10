using MasterDesginPattern.Adapter;
using MasterDesginPattern.Proxy;
using MasterDesginPattern.Strategy;
using MasterDesginPattern.TemplateMethod;

namespace MasterDesginPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Design Pattern World!");

            //-------------Adapter---------------
            CurrencyDisplay  currencyDisplay =new CurrencyDisplay();
            currencyDisplay.DisplayBorkerRecordOnUI();

            PaymentGateway paymentGateway = new PaymentGateway();
            paymentGateway.ProcessCheckOut();


            //------------Proxy--------------
            CalculationEngineClient calculationEngine=new CalculationEngineClient();
            calculationEngine.PerformCalculation();

            StockPrice stockPrice = new StockPrice();
            stockPrice.ProcessStockPrice();

            RateLimittingProxy rateLimittingProxy = new RateLimittingProxy();

            //-----------------Template-------------------
            BrokerFileParser brokerFileParser = new BrokerFileParser();
            PaymentGatewayClient paytmGateway = new PaymentGatewayClient();

            //------------------Strategy--------------------------
            Shipment shipment = new Shipment();
            

            Console.ReadLine();

        }
    }
}
