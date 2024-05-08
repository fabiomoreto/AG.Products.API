using AG.Products.API.Domain.Repositories;
using AG.Products.API.Infra.Data.Context;
using AG.Products.API.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace AG.Products.API.Infra.DependencyInjection
{
    public static class DataConfig
    {
        public static void AddDataConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ProductContext>();
        }
    }
}
