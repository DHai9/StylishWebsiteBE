using Microsoft.EntityFrameworkCore;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;

namespace StylishWebsiteBE.Infrastructure.Repositories.Products
{
    public class ProductRepository : RepositoryBase<ProductReadModel>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<bool> CheckNameExitAsync(string name)
        {
            return _dbContext.Products.AnyAsync( e => e.Name.Equals(name));
        }
    }
}
