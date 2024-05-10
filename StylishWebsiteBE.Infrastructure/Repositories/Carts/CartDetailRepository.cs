using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Cards;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;

namespace StylishWebsiteBE.Infrastructure.Repositories.Carts {
    internal class CartDetailRepository : RepositoryBase<CartDetailReadModel>, ICartDetailRepository {
        private readonly ApplicationDbContext _dbContext;
        public CartDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}