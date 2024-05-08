using AG.Products.API.Application.DTO;
using AG.Products.API.Domain.Entities;
using AG.Products.API.Domain.Shared;
using AG.Products.API.Domain.ValueObjects;
using AutoMapper;

namespace AG.Products.API.Application.Mappings
{
    public class ModelMappingsProfile : Profile
    {
        public ModelMappingsProfile()
        {
            CreateMap<Product, ProductDTO>();

            CreateMap<Supplier, SupplierDTO>();

            CreateMap<ValidityPeriod, ValidityPeriodDTO>();

            CreateMap<PagedList<Product>, PagedList<ProductDTO>>();
        }
    }
}
