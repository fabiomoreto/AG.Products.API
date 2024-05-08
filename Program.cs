using AG.Products.API.Application.Mappings;
using AG.Products.API.Application.Queries;
using AG.Products.API.Infra.DependencyInjection;
using AG.Products.API.Infra.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Auto mapper
builder.Services.AddAutoMapper(typeof(ModelMappingsProfile));
//Global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
//Queries
builder.Services.AddScoped<IProductQueries, ProductQueries>();
//MediatR
builder.Services.AddMediatR(cfg => 
{ 
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); 
});

builder.AddSwaggerConfiguration();

builder.AddDataConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();