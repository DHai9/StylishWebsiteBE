using AutoMapper;
using StylishWebsiteBE.Api.ViewModels.Products;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Api.Profiles {
    public class ProductProfiles : Profile {
        public ProductProfiles()
        {
            CreateMap<ProductViewModel, ProductDto>().ReverseMap();
            CreateMap<ProductOptionViewModel, ProductOptionDto>().ReverseMap();
            CreateMap<ProductVariantViewModel, ProductVariantDto>().ReverseMap();
            CreateMap<ProductVariantViewModel, ProductVariantEntityDto>().ReverseMap();
            CreateMap<VariantValueDto, VariantValueEntityDto>().ReverseMap();
            CreateMap<VariantValueViewModel, VariantValueDto>().ReverseMap();
            CreateMap<VariantValueViewModel, VariantValueEntityDto>().ReverseMap();
        }
    }
}
