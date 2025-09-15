namespace MasterDesignPattern.Observerable
{
    internal class EventAggregatorSimulator
    {
        public void EventAggregatorSimulate()
        {
            var position = new Position();
            var cash = new Cash();
            var openOrders = new OpenOrders();
            position.ModelWeightUpdate(0.25m);
            position.ModelWeightUpdate(0.50m);
            position.ModelWeightUpdate(0.75m);
        }
    }

    /// <summary>
    /// Event message wrapper for strongly-typed event data.
    /// </summary>
    public class EventMessage<T> : EventArgs
    {
        public T Data { get; }
        public EventMessage(T data) => Data = data;
    }

    /// <summary>
    /// Thread-safe, static event aggregator for decoupled event publishing and subscribing.
    /// </summary>
    /// 

    /*
     There is problem with static event aggregator that it can lead to memory leaks if subscribers are not properly unsubscribed.
     Behavior is global and can hard to test and cause hidden coupling between components.
     Harder to manage lifecycle of subscribers and publishers.
     Voilates dependency inversion principle as high-level modules depend on low-level modules.
     */
    public static class EventAggregator<T>
    {
        // OOD: Encapsulation (manages all subscribers internally)
        // Thread-safe dictionary for concurrent access
        private static readonly Dictionary<string, Action<EventMessage<T>>> _subscribers = new();
        private static readonly object _lock = new();

        /// <summary>
        /// Subscribe to an event by name.
        /// </summary>
        public static void Subscribe(string eventName, Action<EventMessage<T>> action)
        {
            if (action == null) return;
            lock (_lock)
            {
                if (!_subscribers.ContainsKey(eventName))
                {
                    _subscribers[eventName] = action;
                }
                else
                {
                    _subscribers[eventName] += action;
                }
            }
        }

        /// <summary>
        /// Unsubscribe from an event by name.
        /// </summary>
        public static void Unsubscribe(string eventName, Action<EventMessage<T>> action)
        {
            if (action == null) return;
            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventName, out var existing))
                {
                    existing -= action;
                    if (existing == null)
                        _subscribers.Remove(eventName);
                    else
                        _subscribers[eventName] = existing;
                }
            }
        }

        /// <summary>
        /// Raise an event by name, passing data to all subscribers.
        /// </summary>
        public static void RaiseEvent(string eventName, T data)
        {
            Action<EventMessage<T>>? handlers;
            lock (_lock)
            {
                _subscribers.TryGetValue(eventName, out handlers);
            }
            handlers?.Invoke(new EventMessage<T>(data));
        }
    }

    /// <summary>
    /// Event name constants.
    /// </summary>
    public static class Consts
    {
        public const string ModelWeightChange = "ModelWeightChange";
    }

    /// <summary>
    /// Publisher: Raises model weight change events.
    /// </summary>
    public class Position
    {
        public Position()
        {
            // No need to keep a local event, use aggregator directly
        }

        public void ModelWeightUpdate(decimal modelWeight)
        {
            EventAggregator<decimal>.RaiseEvent(Consts.ModelWeightChange, modelWeight);
        }
    }

    /// <summary>
    /// Subscriber: Reacts to model weight change events for cash.
    /// </summary>
    public class Cash
    {
        public Cash()
        {
            EventAggregator<decimal>.Subscribe(Consts.ModelWeightChange, OnModelWeightChange);
        }

        private void OnModelWeightChange(EventMessage<decimal> e)
        {
            Console.WriteLine($"Cash received model weight change: {e.Data}");
        }
    }

    /// <summary>
    /// Subscriber: Reacts to model weight change events for open orders.
    /// </summary>
    public class OpenOrders
    {
        public OpenOrders()
        {
            EventAggregator<decimal>.Subscribe(Consts.ModelWeightChange, OnModelWeightChange);
        }

        private void OnModelWeightChange(EventMessage<decimal> e)
        {
            Console.WriteLine($"OpenOrders received model weight change: {e.Data}");
        }
    }
}
