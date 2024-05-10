using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.ReadModels.Orders;

namespace StylishWebsiteBE.Infrastructure.IServices.Orders {
    public interface IOrderDetailService : IServiceBase<OrderDetailReadModel, OrderDetailDto> {
    }
}
