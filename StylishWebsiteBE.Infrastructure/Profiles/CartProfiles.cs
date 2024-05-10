using AutoMapper;
using StylishWebsiteBE.Domain.DTOs.Cards;
using StylishWebsiteBE.Domain.ReadModels.Cards;

namespace StylishWebsiteBE.Infrastructure.Profiles {
    public class CartProfiles : Profile{
        public CartProfiles()
        {
            CreateMap<CartReadModel,CartDto>().ReverseMap();
            CreateMap<CartDetailReadModel,CartDetailDto>().ReverseMap();
        }
    }
}
