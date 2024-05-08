namespace AG.Products.API.Domain.Shared
{
    public class Result
    {
        public Error Error { get; private set; }
        public bool IsSuccessful { get; private set; } = true;

        public Result(Error? error = null)
        {
            if (error is not null)
            {
                IsSuccessful = false;
                Error = error;
            }
        }

        public static Result Success() => new();
        public static Result<TValue> Success<TValue>(TValue value) => new(value);
        public static Result Failure(Error error) => new(error);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, error);
        public static Result<TValue> CreateImplicitResult<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

        public static implicit operator Result(Error error) => Failure(error);
    }
}
