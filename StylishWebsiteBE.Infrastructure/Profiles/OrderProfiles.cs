using AutoMapper;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.DTOs.Statisticals;
using StylishWebsiteBE.Domain.EntityDtos.Orders;
using StylishWebsiteBE.Domain.ReadModels.Orders;

namespace StylishWebsiteBE.Infrastructure.Profiles {
    public class OrderProfiles : Profile {
        public OrderProfiles()
        {
            CreateMap<OrderReadModel, OrderDto>().ReverseMap();
            CreateMap<OrderReadModel, OrderEntityDto>().ReverseMap();

            CreateMap<OrderDetailReadModel, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetailReadModel, OrderDetailEntityDto>().ReverseMap();

            CreateMap<OrderDetailDto, OrderDetailStatisticalDto>().ReverseMap();
        }
    }
}
