namespace AG.Products.API.Domain.Shared
{
    public record Error(string Code, string Message)
    {
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
        public static readonly Error NotFound = new("Error.NotFound", "The specified resource was not found.");
    };
}