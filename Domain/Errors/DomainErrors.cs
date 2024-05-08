using AG.Products.API.Domain.Shared;

namespace AG.Products.API.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Product
        {
            public static Error InvalidDescription = new Error("Product.ChangeDescription", "The product must have a valid description.");
        }

        public static class Supplier
        {
            public static Error InvalidCNPJ = new Error("Supplier.CreateSupplier", "The CNPJ provided for supplier is not valid.");
        }

        public static class ValidityPeriod
        {
            public static Error InvalidDueDate = new Error("ValidityPeriod.CreateValidityPeriod", "Due date must be greater than manufacture date.");
        }
    }
}
