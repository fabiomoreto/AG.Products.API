using AG.Products.API.Domain.Errors;
using AG.Products.API.Domain.Shared;
using System.Text.RegularExpressions;

namespace AG.Products.API.Domain.ValueObjects
{
    public record Supplier
    {
        public int Code { get; }
        public string Name { get; }
        public string CNPJ { get; }

        //Constructor for EF
        protected Supplier() { }

        private Supplier(int code, string name, string cnpj)
        {
            Code = code;
            Name = name;
            CNPJ = cnpj;
        }

        public static Result<Supplier> Create(int code, string name, string cnpj)
        {
            var supplier = new Supplier(code, name, cnpj);

            if (!supplier.IsCnpjValid)
            {
                return DomainErrors.Supplier.InvalidCNPJ;
            }

            return supplier;
        }

        private bool IsCnpjValid => ValidateCnpj(CNPJ);

        private bool ValidateCnpj(string cnpj)
        {
            string pattern = @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$";
            return Regex.IsMatch(cnpj, pattern);
        }
    }
}
