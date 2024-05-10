using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.ReadModels.Orders;

namespace StylishWebsiteBE.Infrastructure.IServices.Orders {
    public interface IOrderService : IServiceBase<OrderReadModel, OrderDto> {
        public Task<List<OrderDto>> GetOrderByUserId(Guid userId);
    }
}
