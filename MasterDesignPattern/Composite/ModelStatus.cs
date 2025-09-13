namespace MasterDesignPattern.Composite
{
    // OOP: Encapsulation (Simulate method encapsulates the workflow)
    // OOP: Abstraction (Hides the details of status composition)
    // SOLID: Single Responsibility Principle (SRP) - Only simulates status composition
    internal class ModelStatus
    {
        public void Simulate()
        {
            // OOP: Object Composition, Polymorphism (using IModelStatus interface)
            var startStatus = new CompositeStatus(1, "Start", DateTime.Now);
            startStatus.Add(new SimpleStatus(1, "New Model", DateTime.Now));

            var inProgressStatus = new CompositeStatus(1, "In-Progress", DateTime.Now);
            inProgressStatus.Add(new SimpleStatus(1, "Model Edit In-Progress", DateTime.Now));
            inProgressStatus.Add(new SimpleStatus(1, "Compliance In-Progress", DateTime.Now));
            inProgressStatus.Add(new SimpleStatus(1, "Send To Blotter In-Progress", DateTime.Now));

            var rejectedStatus = new CompositeStatus(1, "Rejected", DateTime.Now);

            var complianceReject = new CompositeStatus(1, "Compliance Reject", DateTime.Now);
            complianceReject.Add(new SimpleStatus(1, "Compliance Warning", DateTime.Now));
            complianceReject.Add(new SimpleStatus(1, "Compliance Error", DateTime.Now));

            var sendToBlotterReject = new CompositeStatus(1, "Send To Blotter Reject", DateTime.Now);
            sendToBlotterReject.Add(new SimpleStatus(1, "Compliance Failed", DateTime.Now));
            sendToBlotterReject.Add(new SimpleStatus(1, "Compliance Warning", DateTime.Now));

            rejectedStatus.Add(complianceReject);
            rejectedStatus.Add(sendToBlotterReject);

            var approvalStatus = new CompositeStatus(1, "Approval", DateTime.Now);
            approvalStatus.Add(new SimpleStatus(1, "Compliance Approval", DateTime.Now));
            approvalStatus.Add(new SimpleStatus(1, "Send To Blotter Approval", DateTime.Now));

            var completeStatus = new CompositeStatus(1, "Complete", DateTime.Now);
            complianceReject.Add(new SimpleStatus(1, "Send To Blotter Complete", DateTime.Now));

            startStatus.Add(inProgressStatus);
            startStatus.Add(rejectedStatus);
            startStatus.Add(approvalStatus);
            startStatus.DisplayStatus(4);
        }
    }

    /*
     In Portfolio application model has different status
     Start
        -New Model
     In-Progress
        - Model Edit In-Progress
        - Compliance In-Progress
        - Send To Blotter In-Progress
    Rejected
        - Compliance Reject
             -Compliance Warning
             -Compliance Error 
        - Send To Blotter Reject
             -Compliance Failed
             -Compliance Warning
    Approval
        - Compliance Approval
        - Send To Blotter Approval
    Complete
         - Send To Blotter Complete

     To Handle this scenario we can use Composite Design Pattern
     We have hierarchy of status leaf node contain the model Id and status with timestamp remarks
     In non leaf node contains the list of child status
    */

    // OOP: Abstraction (Defines contract for all statuses)
    // OOP: Polymorphism (Allows treating all statuses uniformly)
    // SOLID: Interface Segregation Principle (ISP) - Small, focused interface
    public interface IModelStatus
    {
        // OOP: Encapsulation (Exposes only necessary data)
        int ModelId { get; }
        string Status { get; }
        DateTime TimeStamp { get; }

        // OOP: Abstraction, Polymorphism (Implemented differently by leaf and composite)
        void DisplayStatus(int depth);
    }

    // OOP: Inheritance (Implements IModelStatus)
    // OOP: Encapsulation (Fields are readonly, set via constructor)
    // OOP: Polymorphism (Can be used wherever IModelStatus is expected)
    // SOLID: Single Responsibility Principle (SRP) - Represents a single status)
    // SOLID: Liskov Substitution Principle (LSP) - Can substitute IModelStatus
    public class SimpleStatus : IModelStatus
    {
        public int ModelId { get; }
        public string Status { get; }
        public DateTime TimeStamp { get; }

        public SimpleStatus(int modelId, string status, DateTime timeStamp)
        {   
            ModelId = modelId;
            Status = status;
            TimeStamp = timeStamp;
        }

        // OOP: Polymorphism (Implements interface method)
        // SOLID: Open/Closed Principle (OCP) - Can extend by adding new status types without modifying interface
        public void DisplayStatus(int depth)
        {
            Console.WriteLine(new String('-', depth) + $"Status: {Status} , ModelId: {ModelId}, TimeStamp: {TimeStamp}");
        }
    }

    // OOP: Inheritance (Implements IModelStatus)
    // OOP: Encapsulation (Manages child statuses internally)
    // OOP: Polymorphism (Can be used wherever IModelStatus is expected)
    // OOP: Composition (Holds a collection of IModelStatus)
    // SOLID: Single Responsibility Principle (SRP) - Manages a group of statuses
    // SOLID: Liskov Substitution Principle (LSP) - Can substitute IModelStatus
    // SOLID: Open/Closed Principle (OCP) - Can add new composite/leaf types without modifying this class
    public class CompositeStatus : IModelStatus
    {
        private readonly List<IModelStatus> _children = [];

        public int ModelId { get; }
        public string Status { get; }
        public DateTime TimeStamp { get; }

        public CompositeStatus(int modelId, string status, DateTime timeStamp)
        {
            ModelId = modelId;
            Status = status;
            TimeStamp = timeStamp;
        }

        // OOP: Encapsulation (Manages child statuses)
        public void Add(IModelStatus component) => _children.Add(component);
        public void Remove(IModelStatus component) => _children.Remove(component);

        // OOP: Polymorphism (Implements interface method)
        // SOLID: Open/Closed Principle (OCP) - Can extend by adding new child types
        public void DisplayStatus(int depth)
        {
            Console.WriteLine(new String('-', depth) + $"Status: {Status}, ModelId: {ModelId}, TimeStamp: {TimeStamp}");
            foreach (var child in _children)
                child.DisplayStatus(depth + 2);
        }
    }
}
