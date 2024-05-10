using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.ReadModels.Products {
    public class ProductReadModel : FullAuditedEntity<Guid> {
        public ProductReadModel(Guid id)
        {
            Id = id;
        }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public double Rate { get; set; }
        public double Sale { get; set; }
        public virtual ICollection<ProductVariantReadModel> ProductVariants { get; set; }
        public virtual ICollection<ProductOptionReadModel> ProductOptions { get; set; }
        public virtual ICollection<VariantValueReadModel> VariantValues { get; set; }
    }
}
