using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Domain.EntityDtos.Orders {
    public class OrderDetailEntityDto {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ImportPrice { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public virtual ProductVariantEntityDto ProductVariant { get; set; }
    }
}
