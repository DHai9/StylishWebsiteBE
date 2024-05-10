using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.Enums;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Domain.ReadModels.Options {
    public class OptionReadModel : FullAuditedEntity<Guid> {
        public string Name { get; set; }
        public OptionType OptionType { get; set; }
        public virtual ICollection<VariantValueReadModel> VariantValues { get; set; }
        public virtual ICollection<OptionValueReadModel> OptionValues { get; set; }
        public virtual ICollection<ProductOptionReadModel> ProductOptions { get; set; }
    }
}
