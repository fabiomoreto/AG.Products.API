using AG.Products.API.Domain.Entities;
using AG.Products.API.Domain.Shared;

namespace AG.Products.API.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetById(Guid id, CancellationToken cancellationToken);
        Task<PagedList<Product>> GetAll(CancellationToken cancellationToken, int page, int pageSize, string? searchTerm = null, bool activeOnly = true);
        void Add(Product product);
        void Update(Product product);
        void Delete(Guid id, CancellationToken cancellationToken);
    }
}
