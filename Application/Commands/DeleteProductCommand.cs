using AG.Products.API.Domain.Repositories;
using AG.Products.API.Domain.Shared;
using MediatR;

namespace AG.Products.API.Application.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Result>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id, cancellationToken);

            if (product is null) return Result.Failure(Error.NotFound);

            product.Delete();

            await _productRepository.UnitOfWork.Commit();

            return Result.Success();
        }
    }
}
