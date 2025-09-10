namespace MasterDesginPattern.Proxy
{
    internal class CalculationEngineClient
    {
        public void PerformCalculation()
        {
            var currencyData = new CurrencyCache();
            var calEngine = new CalculationEngine(currencyData);
            //Till this line nothing is called to cache and call the currency provider after this as first call then call the expensive
            //Cache operations
            calEngine.CalculatePositions();
            calEngine.CalculateOrders();
            calEngine.CalculateOpenOrders();
        }
    }

    /*
     We have a calculation engine which perform different types of calculation like cash,position or open orders
     All these calculation required fxrate to calculate the calculation so we need the some kind of cache that 
     store fxrate and does not create cache object every calculation but once.
    
     - We want to cache fxrate
     - Create fxrate object only once when first time access rather than every calculation
     - create a proxy for fxrate cache so that help to achive second point
     */


    // This is orgional source where fxrate are stored or may be some kind of service (xe.com)

    public interface ICurrencyProvider
    {
        IEnumerable<(string, decimal)> GetCurrencyNames();
    }

    public class CurrencyProvider : ICurrencyProvider
    {
        public IEnumerable<(string, decimal)> GetCurrencyNames()
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

    public class CurrencyCache : ICurrencyProvider
    {
        private static IEnumerable<(string, decimal)> currencyCache = null;

        private CurrencyProvider _currencyProvider = null;

        //Favour over composition instead of inhertiance
        // CurrencyCache has-a currency Provider.

        //Cache Proxy or remote proxy
        // We can easily replace the Currency Provider with another without interfering with client
        // Encapuslation : Hide the internale details
        public IEnumerable<(string, decimal)> GetCurrencyNames()
        {
            // If cache is not fill the fill cache
            // Lazy loading  => This currencyCache only load when first time call. 
            if (_currencyProvider == null && currencyCache == null)
            {
                _currencyProvider = new CurrencyProvider(); ;
                Console.WriteLine("Make call to actual service");
                currencyCache = _currencyProvider.GetCurrencyNames();
            }
            //else return from cache object instead of actual service call
            Console.WriteLine("Make call to cache Proxy");

            return currencyCache;
        }
    }

    //
    public class CalculationEngine
    {
        private readonly ICurrencyProvider currencyProvider;

        public CalculationEngine(ICurrencyProvider currencyProvider)
        {
            this.currencyProvider = currencyProvider;
        }

        public void CalculateOpenOrders()
        {
            var getCurrency = currencyProvider.GetCurrencyNames();

            //Perform fx rate based calcualtion
            Console.WriteLine("Perform Open Order fx rate based calc");
        }


        public void CalculateOrders()
        {
            var getCurrency = currencyProvider.GetCurrencyNames();
            //Perform fx rate based calcualtion
            Console.WriteLine("Perform Orders fx rate based calc");
        }

        public void CalculatePositions()
        {
            var getCurrency = currencyProvider.GetCurrencyNames();
            //Perform fx rate based calcualtion
            Console.WriteLine("Perform Position fx rate based calc");
        }
    }

}