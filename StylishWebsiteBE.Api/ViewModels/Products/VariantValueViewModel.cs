using System;

namespace StylishWebsiteBE.Api.ViewModels.Products {
    public class VariantValueViewModel {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid ProductOptionId { get; set; }
        public Guid OptionId { get; set; }
        public Guid ValueId { get; set; }
    }
}
