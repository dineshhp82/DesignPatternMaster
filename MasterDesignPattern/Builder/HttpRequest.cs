namespace MasterDesignPattern.Builder
{
    //Target

    //This class is immutable and thread-safe
    //Immutable objects are inherently thread-safe since their state cannot be modified after creation.
    //We provide all necessary data during construction, and there are no setter methods to change the state later.
    //We prefer to use immutable objects in multi-threaded environments to avoid synchronization issues.
    //And there are few more advantages of immutability like simplicity, reliability, and ease of reasoning about code.
    public class HttpRequestObject
        (string url, string method, Dictionary<string, string> headers, 
        Dictionary<string, string> queryParams, string body, int timeout)
    {
        public string Url { get; } = url;

        public string Method { get; } = method;

        public Dictionary<string, string> Headers { get; } = headers;

        public Dictionary<string, string> QueryParams { get; } = queryParams;

        public string Body { get; } = body;

        public int Timeout { get; } = timeout;


        //This method is added just for simulation purpose and in real project we don't have such type of method
        //we have only properties with getter only
        public void Execute()
        {
            // Simulate executing the HTTP request
            Console.WriteLine($"Executing {Method} request to {Url}");
            Console.WriteLine($"Headers: {string.Join(", ", Headers.Select(h => $"{h.Key}: {h.Value}"))}");
            Console.WriteLine($"Query Params: {string.Join(", ", QueryParams.Select(q => $"{q.Key}={q.Value}"))}");
            Console.WriteLine($"Body: {Body}");
            Console.WriteLine($"Timeout: {Timeout} seconds");
            Console.WriteLine("Request executed successfully.");
        }
    }
}
