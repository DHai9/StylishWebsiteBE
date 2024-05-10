using System;
using System.Collections.Generic;

namespace StylishWebsiteBE.Api.ViewModels.Carts {
    public class CartViewModel {
        public Guid UserId { get; set; }
        public List<CartDetailViewModel> CartDetails { get; set; }
    }
}
