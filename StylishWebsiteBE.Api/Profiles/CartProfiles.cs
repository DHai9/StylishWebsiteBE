using AutoMapper;
using StylishWebsiteBE.Api.ViewModels.Carts;
using StylishWebsiteBE.Domain.DTOs.Cards;

namespace StylishWebsiteBE.Api.Profiles {
    public class CartProfiles : Profile {
        public CartProfiles()
        {
            CreateMap<CartViewModel,CartDto>().ReverseMap();
            CreateMap<CartDetailViewModel,CartDetailDto>().ReverseMap();
        }
    }
}
