namespace MasterDesignPattern.Mediator
{
    /*
     Mediator Pattern :  define an object that encapsulates how a set of objects interact.
     It promotes loose coupling by keeping objects from referring to each other explicitly.
     Instead, they communicate through a mediator object.

                                 C1           C2
       C1->C2->C3                C2    [M]    C3
                                 C3           C1    
      if c1 has reference to c2 and c2 has reference to c3 and so on ..... tightly coupling
      Instead of that we can use mediator pattern where c1,c2,c3 will have reference to mediator only
      
      Without Mediator
      TradeService → RiskCheckService → ComplianceService → MarketGateway → NotificationService
        - Tight coupling: every service knows about the next one.
        - Hard to change order (e.g., run compliance before risk check).
        - Adding a new step means modifying multiple classes.
      
      With Mediator
      TradeService
        |
        v
      TradeMediator
           ├─> RiskCheckService
           ├─> ComplianceService
           ├─> MarketGateway
           └─> NotificationService

        Each service only talks to the mediator.
        Mediator decides the order of execution.
        Easy to extend (e.g., add FraudCheckService).


       Obserer Pattern looks same but different purpose
       
       Observer Pattern :  define a one-to-many dependency between objects so that when one object changes state.
       Mediator Pattern :  define an object that encapsulates how a set of objects interact.  It is many-to-many relationship.
       
    
       Mediator Pattern Benefits
        - Reduced Complexity: Simplifies interactions by centralizing communication.
        - Loose Coupling: Components are decoupled from each other, making changes easier.
        - Flexibility: Easy to add, remove, or modify components without affecting others.
        - Improved Maintainability: Centralized logic makes it easier to manage and understand interactions.
       When to Use Mediator Pattern
        - Complex Interactions: When multiple components interact in complex ways.
        - Frequent Changes: When the interaction logic is likely to change often.
        - Reusability: When you want to reuse components in different contexts without changing their code.
       
      
      Mediator is kind of Orachestartor.
       
     */
    public class TradeExecutions
    {
        public TradeExecutions()
        {
            var risk = new RiskCheckService();
            var compliance = new ComplianceService();
            var market = new MarketGateway();
            var notify = new NotificationService();

            var mediator = new TradeMediator(risk, compliance, market, notify);

            risk.ValidateRisk("BUY Order 1000 APPL");
        }
    }

    // Here ITradeMediator has reference of ITradeService and ITradeService has reference of ITradeMediator.
    public interface ITradeMediator
    {
        void Notify(ITradeService sender, string ev, string order);
    }

    public interface ITradeService
    {
        void SetMediator(ITradeMediator mediator);
    }

    //Concreate service
    public class RiskCheckService : ITradeService
    {
        private ITradeMediator _mediator;

        public void SetMediator(ITradeMediator mediator)
        {
            _mediator = mediator;
        }

        public void ValidateRisk(string order)
        {
            Console.WriteLine($"[RiskCheck] Validating risk for {order}");
            _mediator.Notify(this, "RiskValidated", order);
        }
    }

    public class ComplianceService : ITradeService
    {
        private ITradeMediator _mediator;
        public void SetMediator(ITradeMediator mediator)
        {
            _mediator = mediator;
        }
        public void CheckCompliance(string order)
        {
            Console.WriteLine($"[Compliance] Checking compliance for {order}");
            _mediator.Notify(this, "ComplianceChecked", order);
        }
    }

    public class MarketGateway : ITradeService
    {
        private ITradeMediator _mediator;
        public void SetMediator(ITradeMediator mediator)
        {
            _mediator = mediator;
        }

        public void SendToMarket(string order)
        {
            Console.WriteLine($"[MarketGateway] Sending {order} to market...");
            _mediator.Notify(this, "TradeExecuted", order);
        }
    }

    public class NotificationService : ITradeService
    {
        private ITradeMediator _mediator;
        public void SetMediator(ITradeMediator mediator) => _mediator = mediator;

        public void NotifyTrader(string order)
        {
            Console.WriteLine($"[Notification] Trader notified about {order}");
        }
    }

    public class TradeMediator : ITradeMediator
    {
        private readonly RiskCheckService _risk;
        private readonly ComplianceService _compliance;
        private readonly MarketGateway _market;
        private readonly NotificationService _notify;

        public TradeMediator(RiskCheckService risk,
        ComplianceService compliance,
        MarketGateway market,
        NotificationService notify)
        {
            _risk = risk;
            _compliance = compliance;
            _market = market;
            _notify = notify;

            _risk.SetMediator(this);
            _compliance.SetMediator(this);
            _market.SetMediator(this);
            _notify.SetMediator(this);
        }

        public void Notify(ITradeService sender, string ev, string order)
        {
            switch (ev)
            {
                case "RiskValidated":  //Note : Risk call compliance
                    _compliance.CheckCompliance(order);
                    break;
                case "ComplianceValidated": //Compliance call Trade
                    _market.SendToMarket(order);
                    break;
                case "TradeExecuted": // last step notify trader
                    _notify.NotifyTrader(order);
                    break;
            }
        }
    }
}
