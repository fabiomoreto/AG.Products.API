namespace AG.Products.API.Domain.Shared
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? value, Error error = null)
            : base(error) =>
            _value = value;

        public TValue Value => IsSuccessful
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator Result<TValue>(TValue? value) => CreateImplicitResult(value);
        public static implicit operator Result<TValue>(Error error) => Failure<TValue>(error);
    }
}
