namespace MasterDesginPattern.Proxy
{
    internal class RateLimittingProxy
    {
        public RateLimittingProxy()
        {

            IWeatherService stockService = new WeatherProxy(
               new WeatherService(),
               maxRequests: 3,                 // allow 3 requests
               timeWindow: TimeSpan.FromSeconds(10) // per 10 seconds
           );

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    Console.WriteLine($"Request {i}: Temp = {stockService.GetWeather("India")}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Request {i}: {ex.Message}");
                }
                Thread.Sleep(2000); // wait 2 seconds between calls
            }
        }
    }

    public interface IWeatherService
    {
        decimal GetWeather(string city);
    }

    public class WeatherService : IWeatherService
    {
        public decimal GetWeather(string city)
        {
            Console.WriteLine($"[WeatherService] Fetching weather for {city} from external system...");
            // Simulate weather value retrieval
            return new Random().Next(100, 1000);
        }
    }

    //Proxy
    public class WeatherProxy : IWeatherService
    {
        private readonly IWeatherService _weatherService;

        // Limit config
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;

        // Tracking requests
        private readonly Queue<DateTime> _requestTimestamps = new();

        public WeatherProxy(IWeatherService weatherService, int maxRequests, TimeSpan timeWindow)
        {
            _weatherService = weatherService;
            _maxRequests = maxRequests;
            _timeWindow = timeWindow;
        }

        public decimal GetWeather(string city)
        {
            var now = DateTime.UtcNow;

            // Remove old timestamps (outside the time window)
            while (_requestTimestamps.Count > 0 && (now - _requestTimestamps.Peek()) > _timeWindow)
            {
                _requestTimestamps.Dequeue();
            }

            // Check if limit exceeded
            if (_requestTimestamps.Count >= _maxRequests)
            {
                Console.WriteLine($"[RateLimitingProxy] Too many requests! Please wait...");
                throw new InvalidOperationException("Rate limit exceeded. Try again later.");
            }

            // Record new request
            _requestTimestamps.Enqueue(now);

            // Delegate to real service
            return _weatherService.GetWeather(city);
        }
    }
}
