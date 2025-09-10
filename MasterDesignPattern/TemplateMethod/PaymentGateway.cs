namespace MasterDesginPattern.TemplateMethod
{
    public class PaymentGatewayClient
    {
        public PaymentGatewayClient()
        {
            var gpay = new GooglePayGateway();
            gpay.ProcessPayment("Reqest of 100 rupees from Gpay");

            var payTm = new PaytmGateway();
            gpay.ProcessPayment("Reqest of 200 rupees from Paytm");
        }
    }

    public abstract class WebPaymentGateway
    {
        //Template method
        //Pipeline of steps  (Validate -> Initiate  -> Confirm -> Send Notification)
        public bool ProcessPayment(string request)
        {
            Console.WriteLine(request);
            if (!Validate(request))
            {
                Console.WriteLine("No Validate");
                return false;
            }

            if (!Initiate(request))
            {
                Console.WriteLine("No Initiate");
                return false;
            }

            if (!Confirm(request))
            {
                Console.WriteLine("No Confirm");
                return false;
            }

            //--- Some bank system proceess your payement

            SendNotification(request);
            return true;
        }

        //Dynamic step
        public abstract bool Validate(string request);

        //Dynamic step
        public abstract bool Initiate(string request);

        //Dynamic step
        public abstract bool Confirm(string request);

        //Fixed step
        public void SendNotification(string request)
        {
            Console.WriteLine("Send Notification");
        }
    }

    public class PaytmGateway : WebPaymentGateway
    {
        public override bool Confirm(string request)
        {
            return true;
        }

        public override bool Initiate(string request)
        {
            return true;
        }

        public override bool Validate(string request)
        {
            return true;
        }
    }

    public class GooglePayGateway : WebPaymentGateway
    {
        public override bool Confirm(string request)
        {
            return true;
        }

        public override bool Initiate(string request)
        {
            return true;
        }

        public override bool Validate(string request)
        {
            return true;
        }
    }
}
