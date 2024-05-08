using AG.Products.API.Domain.Entities;
using AG.Products.API.Domain.Repositories;
using AG.Products.API.Domain.Shared;
using AG.Products.API.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AG.Products.API.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(id, cancellationToken);
        }

        public async Task<PagedList<Product>> GetAll(CancellationToken cancellationToken, int page, int pageSize, string? searchTerm, bool activeOnly = true)
        {
            var query = _context.Products.AsQueryable();

            if (searchTerm is not null)
            {
                query = query.Where(x => x.Description.Contains(searchTerm));
            }

            if (activeOnly)
            {
                query = query.Where(x => x.IsActive);
            }

            query = query.OrderBy(x => x.Description).AsNoTracking();

            return await PagedList<Product>.CreateAsync(query, page, pageSize, cancellationToken);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async void Delete(Guid id, CancellationToken cancellationToken)
        {
            var product = await GetById(id, cancellationToken);
            product?.Delete();
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
