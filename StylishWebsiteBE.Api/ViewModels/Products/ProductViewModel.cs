using StylishWebsiteBE.Api.ViewModels.Options;
using System.Collections.Generic;

namespace StylishWebsiteBE.Api.ViewModels.Products {
    public class ProductViewModel {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductOptionViewModel> ProductOptions { get; set; }
        public List<ProductVariantViewModel> ProductVariants { get; set; }
    }
}
