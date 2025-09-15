namespace MasterDesignPattern.Observerable
{
    internal class RefactorEventAggregator
    {
        public void Simulator()
        {
            /*
             
             Case 1: Subscriber runs before Publisher
                var investor = new Investor("Alice", aggregator);  // subscribes first
                var ticker = new StockTicker("MSFT", aggregator);  // publisher created after
                ticker.SetPrice(250.5m);
                **Subscriber-before-publisher works fine because events are delivered only when publisher calls Publish().
             
             Case 2: Publisher runs before Subscriber
                var ticker = new StockTicker("MSFT", aggregator);  // publisher created first
                ticker.SetPrice(250.5m);                           // publishes event before any subscribers
                var investor = new Investor("Alice", aggregator);  // subscribes after event published
                ticker.SetPrice(255.0m);                           // publishes another event
                **Publisher-before-subscriber works fine because events are delivered only when publisher calls Publish().
                *When Alice subscribes later, she will only get future updates, not past ones.
             */



            var eventAggregator = new ConcreteEventAggreator();

            var investor1 = new Investor("Alice", eventAggregator);
            var investor2 = new Investor("Bob", eventAggregator);

            var stockTicker = new StockTicker(eventAggregator);

            stockTicker.UpdateStockPrice("AAPL", 150.25m);
            stockTicker.UpdateStockPrice("GOOGL", 2750.50m);

            eventAggregator.Unsubscribe<StockPriceChangedEvent>(investor2.OnStockPriceChanged);
            stockTicker.UpdateStockPrice("MSFT", 299.99m);
        }
    }

    /*
     Event Aggregator without static 
     
     */

    public class ConcreteEventAggreator
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers[type] = new List<Delegate>();
            }
            _subscribers[type].Add(action);
        }

        public void Unsubscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                _subscribers[type].Remove(action);
                if (_subscribers[type].Count == 0)
                {
                    _subscribers.Remove(type);
                }
            }
        }


        public void Publishe<T>(T eventData)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                foreach (var action in _subscribers[type].OfType<Action<T>>())
                {
                    action(eventData);
                }
            }
        }
    }

    public class StockPriceChangedEvent
    {
        public string Symbol { get; }
        public decimal Price { get; }

        public StockPriceChangedEvent(string symbol, decimal price)
        {
            Symbol = symbol;
            Price = price;
        }
    }

    //publisher
    public class StockTicker
    {
        private readonly ConcreteEventAggreator _eventAggregator;
        public StockTicker(ConcreteEventAggreator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void UpdateStockPrice(string symbol, decimal price)
        {
            var stockEvent = new StockPriceChangedEvent(symbol, price);
            _eventAggregator.Publishe(stockEvent);
        }
    }

    //Subscriber
    public class Investor
    {
        private readonly string _name;


        public Investor(string name, ConcreteEventAggreator eventAggregator)
        {
            _name = name;
            eventAggregator.Subscribe<StockPriceChangedEvent>(OnStockPriceChanged);
        }
        public void OnStockPriceChanged(StockPriceChangedEvent stockEvent)
        {
            Console.WriteLine($"Investor {_name} notified of {stockEvent.Symbol} price change to {stockEvent.Price}");
        }
    }

}
