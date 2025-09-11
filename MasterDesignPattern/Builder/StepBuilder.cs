namespace MasterDesignPattern.Builder
{
    internal class StepBuilder
    {
        public void SimulateStepBuilder()
        {
            Console.WriteLine("---------Step Builder------------");
            HttpRequestObject stepBuilder = new HttpRequestStepBuilder()
                .StartBuilder()
                .WithUrl("http://example.com")
                .WithMethod("POST")
                .WithHeader("A", "jh")
                .WithTimeout(10)
                .WithBody("{}")
                .Build();

            stepBuilder.Execute();

        }
    }

    /*
     Step Builder which help use to enforce the step to build in a particular order 
     if we trying to add in different order then it get breaked the chain.
     
     HOW we convert each mandatory steps to interface and option steps in one interface 
       
     -- Step 1 (Mandatory + Interface) this return the step 2 interface
     -- Step 2 (Mandatory + Interface) this return the step 3 interface
     -- Step 3 (Mandatory + Interface) this return the step 4 interface
     -  Optional Step 4 (
            Optional + 
            Interface + 
            Build() Step return the actual target object)

     Suppose URL, METHOD,HEADER are mandtory and rest are optional
     */

    public interface IUrlStep
    {
        IMethodStep WithUrl(string url);
    }

    public interface IMethodStep
    {
        IHeaderStep WithMethod(string method);
    }

    public interface IHeaderStep
    {
        IOptionalStep WithHeader(string key, string value);
    }

    public interface IOptionalStep
    {
        //Return self interface
        IOptionalStep WithBody(string body);
        IOptionalStep WithTimeout(int timeout);

        //Return target object
        HttpRequestObject Build();
    }

    public class HttpRequestStepBuilder : IUrlStep, IMethodStep, IHeaderStep, IOptionalStep
    {
        private string _url;

        private string _method;

        private Dictionary<string, string> _headers;

        private Dictionary<string, string> _queryParams;

        private string _body;

        private int _timeout;

        public HttpRequestStepBuilder()
        {
            _headers = new Dictionary<string, string>();
            _queryParams = new Dictionary<string, string>();
        }

        public HttpRequestObject Build()
        {
            //only check for mandatory fields do not check for optional fields
            if (string.IsNullOrEmpty(_url))
                throw new Exception("URL cannot be empty");

            return new HttpRequestObject(_url, _method, _headers, _queryParams, _body, _timeout);
        }

        public IOptionalStep WithBody(string body)
        {
            _body = body;
            return this;
        }

        public IOptionalStep WithHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        public IHeaderStep WithMethod(string method)
        {
            _method = method;
            return this;
        }

        public IOptionalStep WithTimeout(int timeout)
        {
            _timeout = timeout;
            return this;
        }

        public IMethodStep WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public IUrlStep StartBuilder()
        {
            return new HttpRequestStepBuilder();
        }
    }
}
