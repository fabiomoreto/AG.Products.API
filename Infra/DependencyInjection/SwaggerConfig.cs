using Microsoft.OpenApi.Models;

namespace AG.Products.API.Infra.DependencyInjection
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auto Glass Products API",
                    Version = "v1"
                });
                c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
