using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Infrastructure.IServices.Products {
    public interface IProductVariantService : IServiceBase<ProductVariantReadModel, ProductVariantDto> {
        public Task<bool> UpdateQuantity(int type, ICollection<ProductVariantDto> entiteDto);
    }
}
