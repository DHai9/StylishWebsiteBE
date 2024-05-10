using System;

namespace StylishWebsiteBE.Api.ViewModels.Carts {
    public class AddItemCartViewModel {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int? Quantity { get; set;}
    }
}
