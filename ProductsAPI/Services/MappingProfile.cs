using AutoMapper;
using ProductsAPI.Data.Entities;
using ProductsAPI.Models.RequestDto;
using ProductsAPI.Models.ResponseDto;

namespace ProductsAPI.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<AddProductDto, Product>();
        }
    }
}
