namespace MasterDesignPattern.Prototype
{
    internal class OrderSnapShot
    {
        public void Simulate()
        {
            // Step 1: Fetch open orders from "DB"
            var dbOrders = new List<Order>
            {
                new Order { OrderId = 1, Symbol = "AAPL", Quantity = 100, Price = 150, Status = "Open" },
                new Order { OrderId = 2, Symbol = "GOOG", Quantity = 50, Price = 2800, Status = "Open" }
            };

            Console.WriteLine("Original Orders from DB:");
            dbOrders.ForEach(Console.WriteLine);

            // Step 2: Clone orders for user to edit

            var clonedOrders = dbOrders.Select(r => r.Clone()).ToList();

            // Simulate user modifying cloned copy
            clonedOrders[0].Quantity = 120; // Changed
            clonedOrders[1].Status = "Cancelled"; // Changed

            Console.WriteLine("\nCloned Orders (after user changes):");
            clonedOrders.ForEach(Console.WriteLine);

            // Step 3: Compare original vs cloned orders
            Console.WriteLine("\nChanges Detected:");

            for (int i = 0; i < dbOrders.Count; i++)
            {
                var original = dbOrders[i];
                var modified = clonedOrders[i];

                if (original.Quantity != modified.Quantity)
                    Console.WriteLine($"OrderId={original.OrderId} Quantity changed {original.Quantity} -> {modified.Quantity}");

                if (original.Price != modified.Price)
                    Console.WriteLine($"OrderId={original.OrderId} Price changed {original.Price} -> {modified.Price}");

                if (original.Status != modified.Status)
                    Console.WriteLine($"OrderId={original.OrderId} Status changed {original.Status} -> {modified.Status}");
            }
        }
    }

    /*
      Open Orders  -> Fetch from Database -> Open Orders -> Create a clone - made some modifation in clone
      -> before send to another API -> Compare open orders  originla and clone and send modified only.


     Open Orders ->Original is expensive to create 
     */

    // Prototype interface
    /*
      Why IPrototype<T> ?
      
     1.) Guarantees Clonability : Class that implement this interface must implement the clone method
     2.) Decouples Client from Concrete Class : Client doesn’t care how cloning is done (shallow, deep, via serialization).
     3.) Type-Safe Cloning Unlike ICloneable (built into .NET), IPrototype<T> avoids ambiguity.
     
     */
    public interface IPrototype<T>
    {
        T Clone();
    }

    // Order entity
    public class Order : IPrototype<Order>
    {
        public int OrderId { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }

        public Order Clone()
        {
            // Shallow copy is fine here since all fields are primitive/string
            return (Order)MemberwiseClone();
        }

        public override string ToString()
        {
            return $"[OrderId={OrderId}, Symbol={Symbol}, Qty={Quantity}, Price={Price}, Status={Status}]";
        }
    }
}
