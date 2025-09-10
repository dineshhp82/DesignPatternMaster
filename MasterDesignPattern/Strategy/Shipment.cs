namespace MasterDesginPattern.Strategy
{
    internal class Shipment
    {

        public Shipment()
        {
            //Use them interchanglably at runtime 
            var shipment = new ShipmentService(new WeightBasedShipment(100));
            var result = shipment.CalculateShipment(new ShipmentOrder(2.5m, 0, 0));
            Console.WriteLine(result);
        }
    }

    public class ShipmentOrder(decimal weight, decimal distance, decimal orderValue)
    {
        public decimal Weight { get; } = weight;

        public decimal Distance { get; } = distance;

        public decimal OrderValue { get; } = orderValue;
    }

    //<<abstract stratgey>>
    // Abstraction
    public interface IShipmentCalculation
    {
        decimal CalculateShipment(ShipmentOrder shipmentOrder);
    }

    // ------- Define a Family of Algorthim---------------
    //follow OCP
    public class FlatRateShipment : IShipmentCalculation
    {
        decimal flatRate;

        public FlatRateShipment(decimal flatRate)
        {
            this.flatRate = flatRate;
        }

        public decimal CalculateShipment(ShipmentOrder shipmentOrder)
        {
            Console.WriteLine("Flat Rate Stratgey");
            return flatRate;
        }
    }

    public class DistanceBasedShipment(decimal perKm) : IShipmentCalculation
    {
        decimal perKm = perKm;

        public decimal CalculateShipment(ShipmentOrder shipmentOrder)
        {
            Console.WriteLine("Distance based Stratgey");
            return perKm * shipmentOrder.Distance;
        }
    }

    public class WeightBasedShipment(decimal perKg) : IShipmentCalculation
    {
        decimal perKg = perKg;

        public decimal CalculateShipment(ShipmentOrder shipmentOrder)
        {
            Console.WriteLine("Weight based Stratgey");
            return perKg * shipmentOrder.Weight;
        }
    }

    public class FedExBasedShipment(decimal baseFee, decimal percentageFee) : IShipmentCalculation
    {
        decimal baseFee = baseFee;
        decimal percentageFee = percentageFee;

        public decimal CalculateShipment(ShipmentOrder shipmentOrder)
        {
            Console.WriteLine("Fed Fx based Stratgey");
            return baseFee + (shipmentOrder.OrderValue * percentageFee);
        }
    }

    //Conext Class  (put them into a class) 
    //follow SRP only responsible for calculateShipment no other responsibity
    //In future of we need to add new stratgey then no need to change the existing code and service
    //Isolated  new class add with seprate test cases.
    public class ShipmentService
    {
        private readonly IShipmentCalculation shipmentCalculation;

        //DIP 
        //Run time pollymorphism 
        //Swap shipment calcualtion at runtime
        //Loosly coupled
        //Composition over inhertance
        public ShipmentService(IShipmentCalculation shipmentCalculation)
        {
            this.shipmentCalculation = shipmentCalculation;
        }

        //Delegation of work to concreate stratgey
        public decimal CalculateShipment(ShipmentOrder shipmentOrder)
        {
            return shipmentCalculation.CalculateShipment(shipmentOrder);
        }
    }
}
