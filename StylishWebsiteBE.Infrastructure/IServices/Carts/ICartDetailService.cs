using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.DTOs.Cards;
using StylishWebsiteBE.Domain.ReadModels.Cards;

namespace StylishWebsiteBE.Infrastructure.IServices.Carts {
    public interface ICartDetailService : IServiceBase<CartDetailReadModel, CartDetailDto> {
    }
}
