using System;
using System.Collections.Generic;

namespace StylishWebsiteBE.Api.ViewModels.Orders {
    public class OrderViewModel {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string DetailAddress { get; set; }
        public string? Note { get; set; }
        public string? NoteCancel { get; set; }
        public short Status { get; set; }
        public decimal TotalAmount { get; set; }
        public short TotalSale { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
