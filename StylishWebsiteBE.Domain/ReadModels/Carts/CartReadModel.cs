using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.ReadModels.Cards {
    public class CartReadModel : FullAuditedEntity<Guid> {
        public Guid UserId { get; set; }
        public ICollection<CartDetailReadModel> CartDetails { get; set; }
    }
}
