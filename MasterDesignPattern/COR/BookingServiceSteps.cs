namespace MasterDesignPattern.COR
{
    internal class BookingServiceSteps
    {
        /*
         Extensible → Add "FraudCheckHandler" without touching other code.
         Readable → Each step isolated in its own class.
         Flexible → You can rearrange handlers depending on business rules.
         Reusable → Same PaymentHandler could be used in other workflows.
         */
        public void Simulate()
        {
            var chain = new AvailabilityHandler();

            chain.SetNext(new UserInfoHandler())
                 .SetNext(new PaymentHandler())
                 .SetNext(new AddressHandler())
                 .SetNext(new BookingHandlerStep())
                 .SetNext(new NotificationHandler());

            var request = new BookingRequest
            {
                IsAvailable = true,
                IsUserValid = true,
                IsPaymentValid = true,
                IsAddressValid = true,
                UserEmail = "user@example.com"
            };

            chain.Handle(request);
        }
    }
    /*
     Your Requirement (as a pipeline)
        Check Availability
        Validate User Info
        Validate Payment Details
        Validate User Address
        Send Booking Request
        Send Notification


       Problem Without CoR :If you implement this in a big if/else block or sequential method calls.

            if (CheckAvailability(request)) {
             if (ValidateUserInfo(request)) {
                if (ValidatePayment(request)) {
                    if (ValidateAddress(request)) {
                        if (SendBooking(request)) {
                            SendNotification(request);
                        }
                    }
                }
            }
        }


     Solution With Chain of Responsibility
      -Each step is a handler.
      -AvailabilityHandler → UserInfoHandler → PaymentHandler → AddressHandler → BookingHandler → NotificationHandler.
      -

    Example Flow (with CoR)

    var chain = new AvailabilityHandler()
                .SetNext(new UserInfoHandler())
                .SetNext(new PaymentHandler())
                .SetNext(new AddressHandler())
                .SetNext(new BookingHandler())
                .SetNext(new NotificationHandler());

    chain.Handle(request);


     
     */

    public class BookingRequest
    {
        public bool IsAvailable { get; set; }
        public bool IsUserValid { get; set; }
        public bool IsPaymentValid { get; set; }
        public bool IsAddressValid { get; set; }
        public bool IsBookingConfirmed { get; set; }
        public string UserEmail { get; set; }
    }

    public interface IBookingHandler
    {
        IBookingHandler SetNext(IBookingHandler next);
        void Handle(BookingRequest request);
    }

    // Why we add this abstract class because if we have 10 handler then we need to implement SetNext method in all handler class
    // so to avoid this we create this abstract class and inherit in all handler class
    // OOD: Inheritance (Abstract base for handlers)
    // OOD: Encapsulation (Handles next handler logic)
    // SOLID: DRY - Avoids code duplication for SetNext logic
    // SOLID: OCP - Can extend by adding new handler types
    public abstract class BookingHandler : IBookingHandler
    {
        private IBookingHandler _nextHandler;

        public IBookingHandler SetNext(IBookingHandler next)
        {
            _nextHandler = next;
            return next;
        }

        public virtual void Handle(BookingRequest request)
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle(request);
            }
        }
    }

    public class AvailabilityHandler : BookingHandler
    {
        public override void Handle(BookingRequest request)
        {
            if (request.IsAvailable)
            {
                Console.WriteLine("Availability Check Passed.");
                base.Handle(request);
            }
            else
            {
                Console.WriteLine("Availability Check Failed.");
            }
        }
    }

    public class UserInfoHandler : BookingHandler
    {
        public override void Handle(BookingRequest request)
        {
            if (request.IsUserValid)
            {
                Console.WriteLine("User Info Validated.");
                base.Handle(request);
            }
            else
            {
                Console.WriteLine("User Info Validation Failed.");
            }
        }
    }

    public class PaymentHandler : BookingHandler
    {
        public override void Handle(BookingRequest request)
        {
            if (request.IsPaymentValid)
            {
                Console.WriteLine("Payment Validated.");
                base.Handle(request);
            }
            else
            {
                Console.WriteLine("Payment Validation Failed.");
            }
        }
    }

    public class AddressHandler : BookingHandler
    {
        public override void Handle(BookingRequest request)
        {
            if (request.IsAddressValid)
            {
                Console.WriteLine("Address Validated.");
                base.Handle(request);
            }
            else
            {
                Console.WriteLine("Address Validation Failed.");
            }
        }
    }

    public class BookingHandlerStep : BookingHandler
    {
        public override void Handle(BookingRequest request)
        {
            Console.WriteLine("Sending booking request to external system...");
            request.IsBookingConfirmed = true; // Simulate success
            base.Handle(request);
        }
    }

    public class NotificationHandler : BookingHandler
    {
        public override void Handle(BookingRequest request)
        {
            if (request.IsBookingConfirmed)
            {
                Console.WriteLine($"Booking confirmed! Notification sent to {request.UserEmail}.");
            }
            else
            {
                Console.WriteLine("Booking not confirmed. No notification sent.");
            }
        }
    }
}
