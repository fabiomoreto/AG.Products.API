using AG.Products.API.Domain.Repositories;
using AG.Products.API.Domain.Shared;
using MediatR;

namespace AG.Products.API.Application.Commands
{
    public record UndeleteProductCommand(Guid Id) : IRequest<Result>;

    public class UndeleteProductCommandHandler : IRequestHandler<UndeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public UndeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UndeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id, cancellationToken);

            if (product is null) return Result.Failure(Error.NotFound);

            product.Undelete();

            await _productRepository.UnitOfWork.Commit();

            return Result.Success();
        }
    }
}
