using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Infrastructure.IServices.Products {
    public interface IProductOptionService : IServiceBase<ProductOptionReadModel, ProductOptionDto> {
    }
}
