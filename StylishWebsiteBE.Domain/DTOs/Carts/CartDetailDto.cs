using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.DTOs.Products;

namespace StylishWebsiteBE.Domain.DTOs.Cards {
    public class CartDetailDto : FullAuditedEntity<Guid> {
        public CartDetailDto(Guid id)
        {
            Id = id;
        }
        public Guid CartId {  get; set; }
        public Guid ProductVariantId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual ProductVariantDto ProductVariants { get; set; }
    }
}
