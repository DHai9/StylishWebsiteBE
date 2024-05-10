using System;
using System.Collections.Generic;

namespace StylishWebsiteBE.Api.ViewModels.Products {
    public class ProductVariantViewModel {
        public ProductVariantViewModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string SkuId { get; set; }
        public long ImportPrice { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Sale { get; set; }
        public ICollection<string> Images { get; set; }
        public List<VariantValueViewModel> VariantValues { get; set; }
    }
}
