namespace StylishWebsiteBE.Domain.DTOs.Statisticals {
    public class OrderDetailStatisticalDto {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ImportPrice { get; set; }
        public string Name { get; set; }
		public string OrderCode { get; set; }
		public string ImageUrl { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
