using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AG.Products.API.Infra.DependencyInjection
{
    public static class HealthCheckConfig
    {
        public static void AddHealthCheckConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "api" });
        }

        //public static IApplicationBuilder UseDefaultHealthcheck(this IApplicationBuilder app)
        //{
        //    app.UseHealthChecks("/healthz", new HealthCheckOptions
        //    {
        //        Predicate = r => r.Tags.Contains("api"),
        //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //    });
        //    app.UseHealthChecks("/healthz-infra", new HealthCheckOptions
        //    {
        //        Predicate = r => r.Tags.Contains("infra"),
        //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //    });

        //    return app;
        //}
    }
}
