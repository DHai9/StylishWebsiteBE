using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.ReadModels.Orders;
using StylishWebsiteBE.Infrastructure.IRepositories.Orders;
using StylishWebsiteBE.Infrastructure.IServices.Orders;

namespace StylishWebsiteBE.Infrastructure.Services.Orders {
    public class OrderService : ServiceBase<OrderReadModel, OrderDto>, IOrderService {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<List<OrderDto>> GetOrderByUserId(Guid userId)
        {
            var results = _orderRepository.TableNoTracking.Where(x => x.UserId == userId);
            return Task.FromResult(_mapper.Map<List<OrderDto>>(results));
        }
    }
}
