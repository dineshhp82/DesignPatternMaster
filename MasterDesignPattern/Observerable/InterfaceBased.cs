namespace MasterDesignPattern.Observerable
{
    internal class InterfaceBased
    {
        public void Simulator()
        {
            var broker1 = new JPMorgan();
            var broker2 = new RoyalBankOfCanada();
            var broker3 = new RoyalBankOfScottland();

            var future = new FutureStockPrice();
            future.AddBroker(broker1);
            future.AddBroker(broker2);
            future.AddBroker(broker3);

            future.UpdatePrice(new StockPriceValue("GOOGLE", 10.99m));
            future.NotifyPriceChange();

            future.UpdatePrice(new StockPriceValue("MICROSOFT", 12.99m));
            future.NotifyPriceChange();


            future.RemoveBroker(broker2);
            future.UpdatePrice(new StockPriceValue("IBM", 89.99m));
            future.NotifyPriceChange();

        }
    }

    /*
     Observer Pattern : Define a one-to-many dependency between object so that when one object change state other react to it.
     
     Main Participants of Observer
     
      IObserver -> Method Update() Update method has paramater to get the state from Subject.

      ISubject -> Attach(IObserver observer), Detach(IObserver observer), Notify() Notify all attached observers of state change.
      ISubject store the collection of IObserver.

      Benfits of Observer Pattern 
      - Decoupling : Subject and Observer are loosely coupled. Subject doesn't need to know details about observers.
      - Scalability : New observers can be added without modifying the subject.
      - Flexibility : Observers can be changed at runtime.
     
    - Use Cases of Observer Pattern
      - Event Handling Systems : GUI frameworks use observer pattern for event handling.
      - Real-Time Data Feeds : Stock market applications use observer pattern to update stock prices in real-time.
      - Notification Systems : Social media platforms use observer pattern to notify users about new messages or friend requests.
      - Model-View-Controller (MVC) Architecture : In MVC, the view observes the model for changes and updates the UI accordingly.

     **Subject can be abstract or concrete class. if we have multiple type of subject then use abstract but if single type of subject
     then do not need to make abstract this will be concrete class.
     
     ** Observer can be interface or abstract class. if we have multiple type of observer then use interface or abstract but if single type of observer
     then do not need to make interface or abstract this will be concrete class.


    **Observer can have reference of subject to get the state of subject.This is useful when observer need to PULL the state from subject.
    **Observer can also get the state from subject via Update method parameter.This is useful when observer need to PUSH the state from subject.  
     
     But Observer with reference of subject make observer tight coupled. So avoid most of time untill we need to call some method of subject
      
     Let take a example of stock price change

     */

    public class StockPriceValue(string name, decimal price)
    {
        public string Name { get; } = name;
        public decimal Price { get; } = price;
    }

    public interface IBroker
    {
        void Update(StockPriceValue stockPrice);
    }

    public interface IStockPrice
    {
        void AddBroker(IBroker broker);
        void RemoveBroker(IBroker broker);
        void NotifyPriceChange();
    }

    public class FutureStockPrice : IStockPrice
    {
        private readonly List<IBroker> brokers;
        private StockPriceValue stockPrice;

        public FutureStockPrice() => brokers = [];

        public void UpdatePrice(StockPriceValue stockPrice)
        {
            this.stockPrice = stockPrice;

        }

        public void AddBroker(IBroker broker)
        {
            brokers.Add(broker);
        }

        public void NotifyPriceChange()
        {
            foreach (var broker in brokers)
            {
                broker.Update(stockPrice);
            }
        }

        public void RemoveBroker(IBroker broker)
        {
            brokers.Remove(broker);
        }
    }

    public class JPMorgan : IBroker
    {
        public void Update(StockPriceValue stockPrice)
        {
            Console.WriteLine($"[{nameof(JPMorgan)}] Stock Price for security {stockPrice.Name} - {stockPrice.Price}");
        }
    }

    public class RoyalBankOfScottland : IBroker
    {
        public void Update(StockPriceValue stockPrice)
        {
            Console.WriteLine($"[{nameof(RoyalBankOfScottland)}] Stock Price for security {stockPrice.Name} - {stockPrice.Price}");
        }
    }

    public class RoyalBankOfCanada : IBroker
    {
        public void Update(StockPriceValue stockPrice)
        {
            Console.WriteLine($"[{nameof(RoyalBankOfCanada)}] Stock Price for security {stockPrice.Name} - {stockPrice.Price}");
        }
    }
}
