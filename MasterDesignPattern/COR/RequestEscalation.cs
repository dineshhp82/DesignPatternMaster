namespace MasterDesignPattern.COR
{
    /*
     Call Center Escalation System:

Requests move up the chain until someone handles them.

Each handler (Junior, Senior, Manager, Senior Manager) decides:

Can I handle it? → If yes, stop chain.

Otherwise, pass it to the next handler.
     
     
     */
    internal class RequestEscalation
    {
        public void Simulate()
        {
            var junior = new JuniorHandler();
            junior.SetNext(new SeniorHandler())
                .SetNext(new ManagerHandler())
                .SetNext(new SeniorManagerHandler());

            var req1 = new SupportRequest(1, "Password reset");
            var req2 = new SupportRequest(2, "Billing issue");
            var req3 = new SupportRequest(3, "Technical escalation");
            var req4 = new SupportRequest(4, "Legal complaint");

            junior.HandleRequest(req1);
            junior.HandleRequest(req2);
            junior.HandleRequest(req3);
            junior.HandleRequest(req4);

        }
    }

    public class SupportRequest
    {
        public int Level { get; }
        public string Description { get; }

        public SupportRequest(int level, string description)
        {
            Level = level;
            Description = description;
        }
    }

    public interface IRequestHandler
    {
        IRequestHandler SetNext(IRequestHandler requestHandler);
        void HandleRequest(SupportRequest request);
    }

    public abstract class BaseRequestHandler : IRequestHandler
    {
        protected IRequestHandler _requestHandler;

        public virtual void HandleRequest(SupportRequest request)
        {
            if (_requestHandler != null)
            {
                //Forward request to next handler if there is any next request handler
                _requestHandler.HandleRequest(request);
            }
        }

        public IRequestHandler SetNext(IRequestHandler requestHandler)
        {
            //this set and return handler
            _requestHandler = requestHandler;
            return _requestHandler; // return next for fluent chain
        }
    }

    public class JuniorHandler : BaseRequestHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level <= 1)
            {
                Console.WriteLine($"Junior handled request: {request.Description}");
                return;
            }

            base.HandleRequest(request);
        }
    }

    public class SeniorHandler : BaseRequestHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level == 2)
            {
                Console.WriteLine($"Senior handled request: {request.Description}");
                return;
            }

            base.HandleRequest(request);
        }
    }

    public class ManagerHandler : BaseRequestHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level == 3)
            {
                Console.WriteLine($"Manager handled request: {request.Description}");
                return;
            }

            base.HandleRequest(request);
        }
    }

    public class SeniorManagerHandler : BaseRequestHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Level >= 3)
            {
                Console.WriteLine($"Senior Manager handled request: {request.Description}");
                return;
            }

            base.HandleRequest(request);
        }
    }
}
