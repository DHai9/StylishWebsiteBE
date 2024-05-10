using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Domain.DTOs.Orders {
    public class OrderDetailDto : FullAuditedEntity<Guid> {
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ImportPrice { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public virtual ProductVariantDto ProductVariant { get; set; }
    }
}
