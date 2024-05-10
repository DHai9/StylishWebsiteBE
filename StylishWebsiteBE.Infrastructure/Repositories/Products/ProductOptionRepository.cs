using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;

namespace StylishWebsiteBE.Infrastructure.Repositories.Products
{
    public class ProductOptionRepository : RepositoryBase<ProductOptionReadModel>, IProductOptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductOptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
