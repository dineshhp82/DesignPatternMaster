using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesginPattern.Proxy
{
    /*
     Stock prices come from an external API (expensive → rate-limited, slower).
     UI needs real-time prices in milliseconds.
  
    Solution: Instead of always calling the real API, use a proxy that:
                Checks Redis cache first.
            If not found → call API, store in Redis, return result.
     */
    public class StockPrice
    {
        public void ProcessStockPrice()
        {
            IStockService stockService = new StockServiceProxy(new RealStockService());

            // First call → hits real API
            Console.WriteLine($"Price AAPL: {stockService.GetStockPrice("AAPL")}\n");

            // Second call within 10s → served from cache
            Console.WriteLine($"Price AAPL: {stockService.GetStockPrice("AAPL")}\n");

            // Different stock → hits API
            Console.WriteLine($"Price MSFT: {stockService.GetStockPrice("MSFT")}\n");

            // After 10s, cache expires → API call again
            Console.WriteLine("Waiting 12 seconds...");
            System.Threading.Thread.Sleep(12000);

            Console.WriteLine($"Price AAPL: {stockService.GetStockPrice("AAPL")}\n");
        }

    }

    public interface IStockService
    {
        decimal GetStockPrice(string symbol);
    }

    //Real Subject (External API Service)

    public class RealStockService : IStockService
    {
        public decimal GetStockPrice(string symbol)
        {
            Console.WriteLine($"[RealStockService] Fetching {symbol} price from external API...");
            // Simulate API call (expensive)
            System.Threading.Thread.Sleep(2000);
            return symbol switch
            {
                "AAPL" => 185.50m,
                "MSFT" => 340.20m,
                "GOOG" => 2800.75m,
                _ => 100.0m
            };
        }
    }


    //Step 3: Proxy (Caching Proxy with Redis-like Store)
    public class StockServiceProxy : IStockService
    {
        private readonly RealStockService _realService;
        private readonly Dictionary<string, (decimal price, DateTime cachedAt)> _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(10);

        public StockServiceProxy(RealStockService realService)
        {
            _realService = realService;
            _cache = new Dictionary<string, (decimal, DateTime)>();
        }

        public decimal GetStockPrice(string symbol)
        {
            // 1. Check cache
            if (_cache.ContainsKey(symbol))
            {
                var (cachedPrice, cachedAt) = _cache[symbol];
                if (DateTime.Now - cachedAt < _cacheDuration)
                {
                    Console.WriteLine($"[Proxy] Returning cached {symbol} price: {cachedPrice}");
                    return cachedPrice;
                }
            }

            // 2. Call real API
            decimal freshPrice = _realService.GetStockPrice(symbol);

            // 3. Save in cache
            _cache[symbol] = (freshPrice, DateTime.Now);

            Console.WriteLine($"[Proxy] Cached {symbol} price: {freshPrice}");
            return freshPrice;

        }
    }




}
