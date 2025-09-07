namespace MasterDesginPattern.Adapter
{
    internal class CurrencyDisplay
    {
        public void DisplayBorkerRecordOnUI()
        {
            var jpMorganData = new JpMorganBrokerData();
            var fxData = CurrencyRates.GetAllCurrencies();
            var processBaseRecord = new ProcessBrokerRecordAdapter(jpMorganData, fxData);

            var usdBaseRecords = processBaseRecord.ProcessBaseRecordUsd();

            foreach (var baseRecord in usdBaseRecords)
            {
                Console.WriteLine(baseRecord);
            }

        }
    }

    /*      
     We have a requirement that out UI grid/Report only display data in USD but we have getting data in
     different currencies from external System so in order to achive that we are going to use adapter patter

     - Make system loosly coupled
     - Favour over composition instead of inhertance
     - SRP of adapter to just convert from one system to another
     - DIP our target is injected in constructor rather than create and depend upon abstraction (ITarget) interface
     - Client does no about the details of how adapter convert it knows only about the abstract detail
       so hide the complexity and internal details.
     - Client does not affect if adapter change the adaptee.
 
     start class and Interface
     
    - 3rd party system 
        JP Morgan return data in different currencies (list of record)

     - Adapater (take JPMorgan data convert into USD and display on UI)

     - UI System which accept the data in USD

     
     */

    //This is our system that expact the BaseRecord all 3rd party API's (it could be JPMorgan,Morgan Stalany,BNP,RBC etc.)
    public class BaseRecord
    {
        public string BrokerName { get; set; }

        public string Currency { get; set; }

        public decimal MarketValueInUSD { get; set; }

        public decimal CurrencyRate { get; set; }

        public override string ToString()
        {
            return $"{BrokerName} - {Currency} - {MarketValueInUSD} - {CurrencyRate}";
        }
    }

    public interface IProcessBrokerBaseRecord
    {
        IEnumerable<BaseRecord> ProcessBaseRecordUsd();
    }

    //This is Adapter use abstration with IS-A relationship
    public class ProcessBrokerRecordAdapter : IProcessBrokerBaseRecord
    {
        //HAS - A Relationship JpMorganBrokerData or adaptee
        //DIP - inject depedency instead of create object inside the class
        //Favour over composition than inhertiance
        private readonly JpMorganBrokerData jpMorganBrokerData;

        //Inject currency data so no need to evaluevate every time
        private readonly IEnumerable<(string currency, decimal rate)> currencyRate;

        public ProcessBrokerRecordAdapter(
            JpMorganBrokerData jpMorganBrokerData, 
            IEnumerable<(string currency, decimal rate)> currencyRate)
        {
            this.jpMorganBrokerData = jpMorganBrokerData;
            this.currencyRate = currencyRate;
        }


        //Encapsulation - Hiding the internal details/ complexity
        public IEnumerable<BaseRecord> ProcessBaseRecordUsd()
        {
            foreach (var brokerRecord in jpMorganBrokerData.GetBrokerRecords())
            {
                var fxRate = currencyRate.FirstOrDefault(x => x.currency == brokerRecord.Currency);

                if (fxRate.currency == null)
                    throw new Exception($"Fx Rate not found for currency{brokerRecord.Currency}");

                yield return new BaseRecord
                {
                    BrokerName = brokerRecord.Broker,
                    Currency = brokerRecord.Currency,
                    CurrencyRate = fxRate.rate,
                    MarketValueInUSD = fxRate.rate * brokerRecord.MarketValue
                };
            }

        }
    }


    // 3rd Party System say some kind of API return data 

    public class JpMorganBrokerData
    {
        public IEnumerable<JpMorganBrokerRecord> GetBrokerRecords()
        {
            yield return new JpMorganBrokerRecord { Broker = "MITX", MarketValue = 1000, Currency = "GBP" };
            yield return new JpMorganBrokerRecord { Broker = "ABVE", MarketValue = 80, Currency = "EUR" };
            yield return new JpMorganBrokerRecord { Broker = "GOOG", MarketValue = 90, Currency = "USD" };
            yield return new JpMorganBrokerRecord { Broker = "MSTS", MarketValue = 89, Currency = "INR" };

        }
    }

    public class JpMorganBrokerRecord
    {
        public string Broker { get; set; }

        public string Currency { get; set; }

        public decimal MarketValue { get; set; }
    }

    //Currency rate
    public class CurrencyRates
    {
        public static IEnumerable<(string currency, decimal rate)> GetAllCurrencies()
        {
            yield return ("GBP", 60.5m);
            yield return ("EUR", 70.5m);
            yield return ("INR", 80.5m);
            yield return ("JPY", 30.5m);
            yield return ("KRZ", 70.1m);
            yield return ("CHZ", 69.5m);
            yield return ("USD", 1m);
        }
    }

}
