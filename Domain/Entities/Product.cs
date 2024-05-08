using AG.Products.API.Domain.Errors;
using AG.Products.API.Domain.Shared;
using AG.Products.API.Domain.ValueObjects;

namespace AG.Products.API.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public Supplier Supplier { get; private set; }
        public ValidityPeriod ValidityPeriod { get; private set; }

        protected Product()
        {
            IsActive = true;
        }

        public static Result<Product> CreateProduct(string description, int supplierCode, string supplierName, string supplierCnpj, DateOnly manufactureDate, DateOnly dueDate)
        {
            var product = new Product();

            var setDescriptionResult = product.ChangeDescription(description);
            if (!setDescriptionResult.IsSuccessful)
            {
                return setDescriptionResult.Error;
            }

            var setSupplierResult = product.ChangeSupplier(supplierCode, supplierName, supplierCnpj);
            if (!setSupplierResult.IsSuccessful)
            {
                return setSupplierResult.Error;
            }

            var setValidityPeriodResult = product.ChangeValidityPeriod(manufactureDate, dueDate);
            if (!setValidityPeriodResult.IsSuccessful)
            {
                return setValidityPeriodResult.Error;
            }

            return product;
        }

        public Result ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return DomainErrors.Product.InvalidDescription;
            }

            Description = description;

            return Result.Success();
        }

        public Result ChangeSupplier(int supplierCode, string supplierName, string cnpj)
        {
            var createSupplierResult = Supplier.Create(supplierCode, supplierName, cnpj);
            if (!createSupplierResult.IsSuccessful)
            {
                return Result.Failure(createSupplierResult.Error);
            }

            Supplier = createSupplierResult.Value;

            return Result.Success();
        }

        public Result ChangeValidityPeriod(DateOnly manufactureDate, DateOnly dueDate)
        {
            var createValidityPeriodResult = ValidityPeriod.Create(manufactureDate, dueDate);
            if (!createValidityPeriodResult.IsSuccessful)
            {
                return Result.Failure<Product>(createValidityPeriodResult.Error);
            }

            ValidityPeriod = createValidityPeriodResult.Value;

            return Result.Success();
        }

        public void Undelete()
        {
            IsActive = true;
        }

        public void Delete()
        {
            IsActive = false;
        }
    }
}
