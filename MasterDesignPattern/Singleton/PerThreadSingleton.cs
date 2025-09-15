namespace MasterDesignPattern.Singleton
{
    internal class PerThreadSingleton
    {
        public void Simulator()
        {
            // Each thread gets its own instance of AppLogger
            Parallel.For(0, 5, i =>
            {
                var logger = AppLogger.Instance;
                logger.Log($"Log entry from thread {i}");
            });
        }   
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public class AppLogger : ILogger
    {
        private static readonly ThreadLocal<AppLogger> _instance = new(() => new AppLogger());
        // Private constructor to prevent external instantiation
        private AppLogger() { }

        public static AppLogger Instance => _instance.Value;

        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}
