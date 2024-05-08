using AG.Products.API.Application.DTO;
using AG.Products.API.Domain.Repositories;
using AG.Products.API.Domain.Shared;
using AutoMapper;

namespace AG.Products.API.Application.Queries
{
    public interface IProductQueries
    {
        Task<ProductDTO> GetProductById(Guid id, CancellationToken cancellationToken);
        Task<PagedList<ProductDTO>> GetAllProducts(CancellationToken cancellationToken, int page, int pageSize, string? searchTerm = null, bool activeOnly = true);
    }

    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductQueries(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(id, cancellationToken);
            
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<PagedList<ProductDTO>> GetAllProducts(CancellationToken cancellationToken, int page, int pageSize, string? searchTerm = null, bool activeOnly = true)
        {
            var products = await _productRepository.GetAll(cancellationToken, page, pageSize, searchTerm, activeOnly);
            return _mapper.Map<PagedList<ProductDTO>>(products);
        }
    }
}
