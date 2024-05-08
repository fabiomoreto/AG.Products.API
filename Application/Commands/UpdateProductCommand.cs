using AG.Products.API.Application.DTO;
using AG.Products.API.Domain.Repositories;
using AG.Products.API.Domain.Shared;
using MediatR;

namespace AG.Products.API.Application.Commands
{
    public record UpdateProductCommand(Guid Id, string Description, SupplierDTO? Supplier, ValidityPeriodDTO? ValidityPeriod) : IRequest<Result>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id, cancellationToken);

            if (product is null) return Error.NotFound;

            if (request.Description is not null)
            {
                var changeDescriptionResult = product.ChangeDescription(request.Description);
                if (!changeDescriptionResult.IsSuccessful) return changeDescriptionResult.Error;
            }

            if(request.Supplier is not null)
            {
                var changeSupplierResult = product.ChangeSupplier(
                request.Supplier.Code,
                request.Supplier.Name,
                request.Supplier.CNPJ);

                if (!changeSupplierResult.IsSuccessful) return changeSupplierResult.Error;
            }

            if (request.ValidityPeriod is not null)
            {
                var changeValidityPeriodResult = product.ChangeValidityPeriod(
                request.ValidityPeriod.ManufactureDate,
                request.ValidityPeriod.DueDate);

                if (!changeValidityPeriodResult.IsSuccessful) return changeValidityPeriodResult.Error;
            }

            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();

            return Result.Success();
        }
    }
}
