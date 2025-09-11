namespace MasterDesignPattern.Builder
{
    internal class DirectorBuilder
    {

        public void SimulateDirectorBuilder()
        {

            HttpRequestBuilderForDirector httpRequestBuilder = new HttpRequestBuilderForDirector();
            HttpRequestDirector httpRequestDirector = new HttpRequestDirector(httpRequestBuilder);

            var request1 = httpRequestDirector.ConstructGetRequest("http://example.com");
            var request2 = httpRequestDirector.ConstructPostRequest("http://example.com", "{ \"name\": \"John\", \"age\": 30 }");
            
            
            //And we can also use direct httpBuilder also
            var request3 = httpRequestBuilder
                .WithUrl("https://api.example.com/data")
                .WithMethod("POST")
                .WithHeader("Authorization", "dkhd8765")
                .Build();

            Console.WriteLine("-----------Director Builder------------");
            request1.Execute();
            Console.WriteLine("-----------Second Request------------");
            request2.Execute();
            Console.WriteLine("-----------Third Request------------");
            request3.Execute();

        }
    }

    /*
     Director Builder is same as Classic Builder but with Director class which is responsible for constructing the object 
     with some stander configuration of object for e.g if we have HttpRequest object and we want to create some standard
     configuration of HttpRequest object like GET request with some default headers and timeout then we can create a Director class.

     While building the object using Director class we can use the builder class to create the object with standard configuration.
     The main advantage of using Director class is that we can create different standard configuration of object without changing the builder class.
     
     While building the object we can use Builder as well as Director class to create the object with configuration.
      1. Director class is responsible for constructing the object with standard configuration.
      2. Builder class is responsible for creating the object with custom configuration.
    

    This will help in reducing the code duplication and also will help in maintaining the code.
    This will also help in creating the object with standard configuration without changing the builder class.
     */


    //This is same as Classic Builder HttpRequestBuilder
    public class HttpRequestBuilderForDirector
    {
        private string _url;

        private string _method;

        private Dictionary<string, string> _headers;

        private Dictionary<string, string> _queryParams;

        private string _body;

        private int _timeout;

        public HttpRequestBuilderForDirector()
        {
            _headers = new Dictionary<string, string>();
            _queryParams = new Dictionary<string, string>();
        }


        public HttpRequestBuilderForDirector WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public HttpRequestBuilderForDirector WithMethod(string method)
        {
            _method = method;
            return this;
        }

        public HttpRequestBuilderForDirector WithHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        public HttpRequestBuilderForDirector WithQueryParams(string key, string value)
        {
            _queryParams[key] = value;
            return this;
        }

        public HttpRequestBuilderForDirector WithBody(string body)
        {
            _body = body;
            return this;
        }

        public HttpRequestBuilderForDirector WithTimeout(int timeout)
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

    public class HttpRequestDirector
    {
        private readonly HttpRequestBuilderForDirector _builder;

        public HttpRequestDirector(HttpRequestBuilderForDirector builder)
        {
            _builder = builder;
        }

        //Standard GET request with default headers and timeout
        public HttpRequestObject ConstructGetRequest(string url)
        {
            return _builder
                .WithUrl(url)
                .WithMethod("GET")
                .WithHeader("Accept", "application/json")
                .WithTimeout(30)
                .Build();
        }

        //Standard POST request with default headers and timeout
        public HttpRequestObject ConstructPostRequest(string url, string body)
        {
            return _builder
                .WithUrl(url)
                .WithMethod("POST")
                .WithHeader("Content-Type", "application/json")
                .WithBody(body)
                .WithTimeout(30)
                .Build();
        }
    }
}


