using System;

namespace StylishWebsiteBE.Api.ViewModels.Carts {
    public class CartDetailViewModel {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
