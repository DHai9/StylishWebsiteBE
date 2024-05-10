using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Domain.EntityDtos.Products {
    public class ProductEntityDto {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public double Rate { get; set; }
        public double Sale { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public virtual ICollection<ProductVariantEntityDto> ProductVariants { get; set; }
        public virtual ICollection<ProductOptionsEntityDto> ProductOptions { get; set; }
        public virtual ICollection<VariantValueEntityDto> VariantValues { get; set; }
    }
}
