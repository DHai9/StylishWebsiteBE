using System;

namespace StylishWebsiteBE.Api.ViewModels.Products {
    public class ProductOptionViewModel {
        public ProductOptionViewModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }
    }
}
