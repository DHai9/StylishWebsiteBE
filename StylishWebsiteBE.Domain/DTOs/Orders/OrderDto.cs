using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.EntityDtos.Orders;

namespace StylishWebsiteBE.Domain.DTOs.Orders {
    public class OrderDto : FullAuditedEntity<Guid>{
        public OrderDto(Guid id)
        {
            Id = id;
        }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string DetailAddress { get; set; }
        public string? Note { get; set; }
        public string? NoteCancel { get; set; }
        public short Status { get; set; }
        public decimal TotalAmount { get; set; }
        public short TotalSale {  get; set; }
        public ICollection<OrderDetailDto> OrderDetails { get; set; }
    }
}
