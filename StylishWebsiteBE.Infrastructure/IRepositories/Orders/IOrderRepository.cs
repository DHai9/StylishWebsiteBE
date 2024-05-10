using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.ReadModels.Orders;

namespace StylishWebsiteBE.Infrastructure.IRepositories.Orders {
    public interface IOrderRepository : IRepositoryBase<OrderReadModel> {
    }
}
