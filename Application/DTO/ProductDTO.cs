namespace AG.Products.API.Application.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public SupplierDTO Supplier { get; set; }
        public ValidityPeriodDTO ValidityPeriod { get; set; }
    }
}
