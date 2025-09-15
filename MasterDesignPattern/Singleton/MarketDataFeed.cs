using MasterDesignPattern.Singleton;

namespace MasterDesignPattern.Singleton
{
    internal class MarketDataFeed
    {
        /*Single connection to Market Data Provider (saves cost & resources).
         * Consistency: All systems get the same feed, no discrepancies.
         * Thread-safety: Lazy<T> ensures safe initialization.
         * Extensibility: Can implement IMarketDataFeed interface for testing/mocking.
         */
        public void Simulate()
        {
            MarketDataFeedManager.Instance.UpdatePrice("AAPL", 180.50m);
            MarketDataFeedManager.Instance.UpdatePrice("MSFT", 340.20m);

            // Trade engines consuming the SAME shared feed
            var equitiesEngine = new TradeEngine();
            equitiesEngine.ExecuteTrade("AAPL", 100);

            var derivativesEngine = new TradeEngine();
            derivativesEngine.ExecuteTrade("MSFT", 50);
        }
    }
    /*
     In an investment bank, all trading systems (equities, derivatives, FX) need access to real-time market data (stock prices, FX rates, indices).

If every service opens its own connection to the Market Data Provider (like Bloomberg, Reuters, ICE),
→ it’s expensive, redundant, and could exceed provider connection limits.

Instead, we need one shared data feed connection that everyone can use.
     

    - Single Instance 
    - Take care about singleton
    - Global Access Point
    - Lazy Initialization
    - Thread Safety
    - Inhertiance from interface and IDispose
    - Can pass as parameter
    - Unit Testing
    - Can use in DI
    - Can mock able
    - Can follow OOP 

    -Singleton 
       - Lazy
       - Eagar
       - Thread Safe
       - Per Thread

    - Singleton Anti-Patterns to avoid
      
     
     */

    public interface IMarketDataFeedManager
    {
        public decimal GetPrice(string symbol);
        public void UpdatePrice(string symbol, decimal price);   
    }

    public sealed class MarketDataFeedManager : IMarketDataFeedManager
    {
        private static readonly Lazy<MarketDataFeedManager> _instance = new(() => new MarketDataFeedManager());

        public static MarketDataFeedManager Instance => _instance.Value;

        private readonly Dictionary<string, decimal> _prices = new();

        // Private constructor to prevent direct instantiation
        private MarketDataFeedManager()
        {
            ConnectToProvider();
        }

        private void ConnectToProvider()
        {
            Console.WriteLine("Connecting to Bloomberg/Reuters feed...");
            // Imagine a socket/stream API setup here
        }

        public decimal GetPrice(string symbol)
        {
            return _prices.TryGetValue(symbol, out var price) ? price : 0m;
        }

        public void UpdatePrice(string symbol, decimal price)
        {
            _prices[symbol] = price;
            Console.WriteLine($"Price updated: {symbol} = {price}");
        }
    }
}

public class TradeEngine
{
    public void ExecuteTrade(string symbol, int qty)
    {
        var price = MarketDataFeedManager.Instance.GetPrice(symbol);
        Console.WriteLine($"Executing trade: {qty} shares of {symbol} at {price}");
    }
}
