using AutoMapper;
using StylishWebsiteBE.Api.ViewModels.Orders;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.EntityDtos.Orders;

namespace StylishWebsiteBE.Api.Profiles {
    public class OrderProfiles : Profile {
        public OrderProfiles()
        {
            CreateMap<OrderViewModel, OrderDto>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetailEntityDto>().ReverseMap();
            CreateMap<OrderDetailViewModel, OrderDetailEntityDto>().ReverseMap();
            CreateMap<OrderDetailViewModel, OrderDetailDto>().ReverseMap();
        }
    }
}
