using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Orders;
using StylishWebsiteBE.Infrastructure.IRepositories.Orders;

namespace StylishWebsiteBE.Infrastructure.Repositories.Orders {
    public class OrderRepository : RepositoryBase<OrderReadModel>, IOrderRepository {
        private readonly ApplicationDbContext _dbContext;
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
