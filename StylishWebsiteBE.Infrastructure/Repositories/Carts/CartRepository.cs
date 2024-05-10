using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Cards;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;

namespace StylishWebsiteBE.Infrastructure.Repositories.Carts {
    public class CartRepository : RepositoryBase<CartReadModel> , ICartRepository{
        private readonly ApplicationDbContext _dbContext;
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
