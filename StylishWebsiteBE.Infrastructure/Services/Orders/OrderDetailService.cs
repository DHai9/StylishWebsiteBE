using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.ReadModels.Orders;
using StylishWebsiteBE.Infrastructure.IRepositories.Orders;
using StylishWebsiteBE.Infrastructure.IServices.Orders;

namespace StylishWebsiteBE.Infrastructure.Services.Orders {
    public class OrderDetailService : ServiceBase<OrderDetailReadModel, OrderDetailDto>, IOrderDetailService {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper) : base(orderDetailRepository, mapper)
        {
            _orderDetailRepository = orderDetailRepository ?? throw new ArgumentNullException(nameof(orderDetailRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
