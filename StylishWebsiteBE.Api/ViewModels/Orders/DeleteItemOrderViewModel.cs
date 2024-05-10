using System;

namespace StylishWebsiteBE.Api.ViewModels.Orders {
    public class DeleteItemOrderViewModel {
        public Guid OrderId { get; set;}
        public Guid ItemId { get; set;}
    }
}
