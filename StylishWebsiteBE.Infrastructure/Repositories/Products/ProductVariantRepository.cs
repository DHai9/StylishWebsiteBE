using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;

namespace StylishWebsiteBE.Infrastructure.Repositories.Products
{
    internal class ProductVariantRepository : RepositoryBase<ProductVariantReadModel>, IProductVariantRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductVariantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
