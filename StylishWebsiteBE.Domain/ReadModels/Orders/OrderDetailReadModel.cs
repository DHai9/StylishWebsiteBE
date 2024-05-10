using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Domain.ReadModels.Orders {
    public class OrderDetailReadModel : FullAuditedEntity<Guid>{
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ImportPrice { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public virtual OrderReadModel Order { get; set; }
        public virtual ProductVariantReadModel ProductVariant { get; set; }
    }
}
