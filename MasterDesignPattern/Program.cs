using MasterDesginPattern.Proxy;
using MasterDesginPattern.Strategy;
using MasterDesginPattern.TemplateMethod;
using MasterDesignPattern.Adapter;
using MasterDesignPattern.Builder;
using MasterDesignPattern.Composite;
using MasterDesignPattern.COR;
using MasterDesignPattern.Factory;
using MasterDesignPattern.Observerable;
using MasterDesignPattern.Prototype;

namespace MasterDesginPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Design Pattern and Principles!");

            //-------------Observer-------------
            RefactorEventAggregator refactorEvent = new RefactorEventAggregator();
            refactorEvent.Simulator();

           EventAggregatorSimulator eventAggregatorSimulator=new EventAggregatorSimulator();
            eventAggregatorSimulator.EventAggregatorSimulate();

            EventBased eventBased = new EventBased();
            eventBased.Simulator();

            InterfaceBased interfaceBased = new InterfaceBased();
            interfaceBased.Simulator();


            //--------------COR ----------------
            RequestEscalation requestEscalation = new RequestEscalation();
            requestEscalation.Simulate();

            BookingServiceSteps bookingService = new BookingServiceSteps();
            bookingService.Simulate();

            AtmMoneyDispancer atmMoneyDispancer = new AtmMoneyDispancer();
            atmMoneyDispancer.Simulate();



            //--------------- Composit-------------------
            ModelStatus modelStatus = new ModelStatus();
            modelStatus.Simulate();

            //------------Prototype-------------------

            RestoreSettingDefault restoreSettingDefault = new RestoreSettingDefault();
            restoreSettingDefault.Simulate();

            OrderSnapShot orderSnapShot = new OrderSnapShot();
            orderSnapShot.Simulate();


            //----------factory----------------
            SimpleFactory simpleFactory = new SimpleFactory();
            simpleFactory.SimulateSimpleFactory();

            WarningFactory warningFactory = new WarningFactory();
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
            CurrencyDisplay currencyDisplay = new CurrencyDisplay();
            currencyDisplay.DisplayBorkerRecordOnUI();

            PaymentGateway paymentGateway = new PaymentGateway();
            paymentGateway.ProcessCheckOut();


            //------------Proxy--------------
            CalculationEngineClient calculationEngine = new CalculationEngineClient();
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
