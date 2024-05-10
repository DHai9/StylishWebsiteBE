using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Cards;

namespace StylishWebsiteBE.Domain.DTOs.Cards {
    public class CartDto : FullAuditedEntity<Guid> {
        public CartDto(Guid id)
        {
            Id = id;
        }
        public Guid UserId { get; set; }
        public ICollection<CartDetailDto> CartDetails { get; set; }
    }
}
