namespace MasterDesignPattern.Builder
{
    public class ClassicBuilder
    {
        public void SimulateHttpRequests()
        {

            //Increase Readability
            //Avoids the telescoping constructor problem
            //Immutable Object
            //Separation of Concerns
            //Fluent Interface
            //Easier to maintain and extend
            //Reduces errors
            //Encapsulates complex construction logic
            //Improves code organization
            //Facilitates testing
            //Supports optional parameters
            //Enhances code clarity
            //Promotes reusability

            //But there is some problems in this solution
            //Sequence of adding parameters is not enforced
            //Builder class can become large
            //Not suitable for simple objects
            //Overhead of creating a separate builder class
            //Not thread-safe unless explicitly handled
            //Potential for incomplete object construction if not used carefully
            //May lead to code bloat with many builder methods
            //Lack of compile-time checks for mandatory fields
            //Can be overkill for simple object creation scenarios
            HttpRequestObject httpRequest1 = new HttpRequestBuilder()
                .WithUrl("https://api.example.com/data")
                .WithMethod("POST")
                .WithHeader("Authorization", "dkhd8765")
                .Build();


            HttpRequestObject httpRequest2 = new HttpRequestBuilder()
               .WithUrl("https://api.example.com/data")
               .WithMethod("POST")
               .WithHeader("Authorization", "dkhd8765")
               .WithTimeout(120)
               .WithBody("{ \"name\": \"John\", \"age\": 30 }")
               .Build();

            httpRequest1.Execute();

            Console.WriteLine("-----------Second Request----------");
            httpRequest2.Execute();

        }

    }

    //Encapsulates the construction logic for HttpRequestObject
    //Easy to extend building steps without modifying the target class follow open /closed principle
    public class HttpRequestBuilder
    {
        private string _url;

        private string _method;

        private Dictionary<string, string> _headers;

        private Dictionary<string, string> _queryParams;

        private string _body;

        private int _timeout;

        public HttpRequestBuilder()
        {
            _headers = new Dictionary<string, string>();
            _queryParams = new Dictionary<string, string>();
        }


        public HttpRequestBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public HttpRequestBuilder WithMethod(string method)
        {
            _method = method;
            return this;
        }

        public HttpRequestBuilder WithHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        public HttpRequestBuilder WithQueryParams(string key, string value)
        {
            _queryParams[key] = value;
            return this;
        }

        public HttpRequestBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public HttpRequestBuilder WithTimeout(int timeout)
        {
            _timeout = timeout;
            return this;
        }

        public HttpRequestObject Build()
        {
            //only check for mandatory fields do not check for optional fields
            if (string.IsNullOrEmpty(_url))
                throw new Exception("URL cannot be empty");

            return new HttpRequestObject(_url, _method, _headers, _queryParams, _body, _timeout);
        }
    }
}
