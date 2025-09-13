using MasterDesginPattern.Proxy;
using MasterDesginPattern.Strategy;
using MasterDesginPattern.TemplateMethod;
using MasterDesignPattern.Adapter;
using MasterDesignPattern.Builder;
using MasterDesignPattern.Composite;
using MasterDesignPattern.Factory;
using MasterDesignPattern.Prototype;

namespace MasterDesginPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Design Pattern World!");

            ModelStatus modelStatus = new ModelStatus();
            modelStatus.Simulate();



            //------------Prototype-------------------
            
            RestoreSettingDefault restoreSettingDefault=new RestoreSettingDefault();
            restoreSettingDefault.Simulate();
            
            OrderSnapShot orderSnapShot=new OrderSnapShot();
            orderSnapShot.Simulate();


            //----------factory----------------
            SimpleFactory simpleFactory = new SimpleFactory();
            simpleFactory.SimulateSimpleFactory();

            WarningFactory warningFactory=new WarningFactory();
            warningFactory.Simulator();

            RefactorWarningFactory refactorWarningFactory = new RefactorWarningFactory();
            refactorWarningFactory.Simulator();

            //-------------Builder ---------------------
            ClassicBuilder classicBuilder = new ClassicBuilder();
            classicBuilder.SimulateHttpRequests();


            DirectorBuilder directorBuilder = new DirectorBuilder();
            directorBuilder.SimulateDirectorBuilder();

            StepBuilder stepBuilder = new StepBuilder();
            stepBuilder.SimulateStepBuilder();


           SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();
            sqlQueryBuilder.CreateQuery();

            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail();

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
