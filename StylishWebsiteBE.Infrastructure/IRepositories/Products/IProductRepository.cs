using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Infrastructure.IRepositories.Products {
    public interface IProductRepository : IRepositoryBase<ProductReadModel> {
        public Task<bool> CheckNameExitAsync(string name);
    }
}