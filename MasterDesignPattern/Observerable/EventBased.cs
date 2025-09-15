namespace MasterDesignPattern.Observerable
{
    // OOD: Encapsulation (Simulator method encapsulates the event-based workflow)
    // OOD: Abstraction (Hides event subscription details from the client)
    // SOLID: SRP - Only responsible for demonstrating event-based observer pattern
    internal class EventBased
    {
        public void Simulator()
        {
            var lotSizeFeed = new LotSizeFeedChange();

            var futureLotSizeObserver = new FutureLotSize();
            var optionsLotSizeObserver = new OptionsLotSize();

            // OOD: Observer Pattern (Event-based)
            lotSizeFeed.LotSizeChanged += futureLotSizeObserver.OnLotSizeChanged;
            lotSizeFeed.LotSizeChanged += optionsLotSizeObserver.OnLotSizeChanged;

            lotSizeFeed.UpdateLotSize(new LotSizeChangeArgs("Google", 13000));
            lotSizeFeed.UpdateLotSize(new LotSizeChangeArgs("MITX", 10000));
            lotSizeFeed.UpdateLotSize(new LotSizeChangeArgs("EVVE", 8000));

            // Unsubscribe OptionsLotSize observer
            lotSizeFeed.LotSizeChanged -= optionsLotSizeObserver.OnLotSizeChanged;

            lotSizeFeed.UpdateLotSize(new LotSizeChangeArgs("MSFT", 5000));
        }
    }

    // OOD: Encapsulation (EventArgs for lot size changes)
    // SOLID: SRP - Only holds event data
    public class LotSizeChangeArgs : EventArgs
    {
        public string SecurityName { get; }
        public int LotSize { get; }

        public LotSizeChangeArgs(string securityName, int lotSize)
        {
            SecurityName = securityName;
            LotSize = lotSize;
        }
    }

    // OOD: Subject in Observer Pattern
    // SOLID: SRP - Only responsible for publishing lot size changes
    public class LotSizeFeedChange
    {
        // OOD: Event-based Observer Pattern
        public event Action<LotSizeChangeArgs> LotSizeChanged;

        protected virtual void OnLotSizeChanged(LotSizeChangeArgs e)
        {
            LotSizeChanged?.Invoke(e);
        }

        public void UpdateLotSize(LotSizeChangeArgs args)
        {
            OnLotSizeChanged(args);
        }
    }

    // OOD: Observer in Observer Pattern
    // SOLID: SRP - Only responsible for handling future lot size changes
    public class FutureLotSize
    {
        public void OnLotSizeChanged(LotSizeChangeArgs e)
        {
            Console.WriteLine($"[Future] {e.SecurityName} - {e.LotSize}");
        }
    }

    // OOD: Observer in Observer Pattern
    // SOLID: SRP - Only responsible for handling options lot size changes
    public class OptionsLotSize
    {
        public void OnLotSizeChanged(LotSizeChangeArgs e)
        {
            Console.WriteLine($"[Options] {e.SecurityName} - {e.LotSize}");
        }
    }
}
