using AutoMapper;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.EntityDtos.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Infrastructure.Profiles {
    public class ProductProfiles : Profile {
        public ProductProfiles()
        {
            CreateMap<ProductReadModel, ProductDto>().ReverseMap();
            CreateMap<ProductReadModel, ProductEntityDto>().ReverseMap();
            CreateMap<ProductVariantReadModel, ProductVariantDto>().ReverseMap();
            CreateMap<ProductVariantReadModel, ProductVariantEntityDto>().ReverseMap();
            CreateMap<ProductVariantDto, ProductVariantEntityDto>().ReverseMap();
            CreateMap<ProductOptionReadModel, ProductOptionDto>().ReverseMap();
            CreateMap<ProductOptionReadModel, ProductOptionsEntityDto>().ReverseMap();
            CreateMap<VariantValueReadModel, VariantValueDto>().ReverseMap();
            CreateMap<VariantValueReadModel, VariantValueEntityDto>().ReverseMap();
        }
    }
}