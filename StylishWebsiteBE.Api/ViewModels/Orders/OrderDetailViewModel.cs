using System;

namespace StylishWebsiteBE.Api.ViewModels.Orders {
    public class OrderDetailViewModel {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal TotalAmount { get; set; }
        public short TotalSale { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
