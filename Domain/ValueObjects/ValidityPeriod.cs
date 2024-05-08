using AG.Products.API.Domain.Errors;
using AG.Products.API.Domain.Shared;

namespace AG.Products.API.Domain.ValueObjects
{
    public record ValidityPeriod
    {
        public DateOnly ManufactureDate { get; }
        public DateOnly DueDate { get; }

        //Constructor for EF
        protected ValidityPeriod() { }

        private ValidityPeriod(DateOnly manufactureDate, DateOnly dueDate)
        {
            ManufactureDate = manufactureDate;
            DueDate = dueDate;
        }

        public static Result<ValidityPeriod> Create(DateOnly manufactureDate, DateOnly dueDate)
        {
            var validityPeriod = new ValidityPeriod(manufactureDate, dueDate);

            if (!validityPeriod.IsDueDateGreaterThanManufacturing)
            {
                return DomainErrors.ValidityPeriod.InvalidDueDate;
            }

            return validityPeriod;
        }

        private bool IsDueDateGreaterThanManufacturing => DueDate > ManufactureDate;
    }
}