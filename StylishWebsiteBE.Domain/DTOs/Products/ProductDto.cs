using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Domain.DTOs.Products {
    public class ProductDto : FullAuditedEntity<Guid>
    {
        public ProductDto(Guid id)
        {
            Id = id;
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ShortDescription { get; set; }
        public string Brand { get; set; }
        public double Rate { get; set; }
        public double Sale { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public virtual List<ProductVariantDto> ProductVariants { get; set; }
        public virtual List<ProductOptionDto> ProductOptions { get; set; }

        public void GetFullInfoVariantValue()
        {
            ProductVariants.ForEach(x => x.GetFullInfoVariantValue());
        }
    }
}
