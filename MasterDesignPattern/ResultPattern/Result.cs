namespace MasterDesignPattern.ResultPattern
{
    /*
     Result Pattern  : It is a pattern where method return structure result object instead of bool or throwing exception.
    It communicates success ,failure,error message and optionally data making error handling more explicit and manageable.

    Benfits
     - Readability
     - Encapsulate operation outcome (status + error message + optional data)
     - Enforce explciti handling (IsSuccess/IsFail)
     - Suport method chaning because all methods return same result types


     */
    public class Result
    {
        public bool IsSuccess { get; }

        public string Error { get; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
                throw new InvalidOperationException();
            if (!isSuccess && error == string.Empty)
                throw new InvalidOperationException();
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(string message) => new(false, message);

        public static Result Ok() => new(true, string.Empty);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(T value,bool isSuccess, string error) 
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Fail(string message) => new(default!, false, message);  
        
        public static Result<T> Ok(T value) => new(value, true, string.Empty);  
    }
}
