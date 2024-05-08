using AG.Products.API.Application.DTO;
using AG.Products.API.Domain.Entities;
using AG.Products.API.Domain.Repositories;
using AG.Products.API.Domain.Shared;
using MediatR;

namespace AG.Products.API.Application.Commands
{
    public record CreateProductCommand(string Description, SupplierDTO Supplier, ValidityPeriodDTO ValidityPeriod) : IRequest<Result>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var createProductResult = Product.CreateProduct(
                request.Description,
                request.Supplier.Code,
                request.Supplier.Name,
                request.Supplier.CNPJ,
                request.ValidityPeriod.ManufactureDate,
                request.ValidityPeriod.DueDate);

            if (!createProductResult.IsSuccessful) return createProductResult.Error;

            _productRepository.Add(createProductResult.Value);

            await _productRepository.UnitOfWork.Commit();

            return Result.Success();
        }
    }
}
