using AG.Products.API.Domain.Entities;
using AG.Products.API.Domain.Repositories;
using AG.Products.API.Infra.Data;
using AG.Products.API.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AG.Products.API.Infra.Data.Context
{
    public class ProductContext : DbContext, IUnitOfWork
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductContextMapping());
        }

        public async Task<bool> Commit()
        {
            var productEntries = ChangeTracker.Entries<Product>().Where(e => e.State == EntityState.Modified);

            foreach (var entry in productEntries)
            {
                entry.Property("Code").IsModified = false;
            }

            return await SaveChangesAsync() > 0;
        }
    }
}
