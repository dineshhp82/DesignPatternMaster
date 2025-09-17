using SuperMarket.Interfaces;

namespace SuperMarket.AppLogger
{
    public class Logger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }
    }
}
