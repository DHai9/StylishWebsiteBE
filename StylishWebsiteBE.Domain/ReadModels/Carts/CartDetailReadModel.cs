using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Domain.ReadModels.Cards {
    public class CartDetailReadModel : FullAuditedEntity<Guid>{
        public Guid CartId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual ProductVariantReadModel ProductVariants { get; set; }
        public virtual CartReadModel Card { get; set; }
    }
}
