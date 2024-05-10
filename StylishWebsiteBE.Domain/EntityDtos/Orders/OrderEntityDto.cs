namespace StylishWebsiteBE.Domain.EntityDtos.Orders {
    public class OrderEntityDto {
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string DetailAddress { get; set; }
        public string? Note { get; set; }
        public string? NoteCancel { get; set; }
        public short Status { get; set; }
        public decimal TotalAmount { get; set; }
        public short TotalSale { get; set; }
        public ICollection<OrderDetailEntityDto> Details { get; set; }
    }
}
