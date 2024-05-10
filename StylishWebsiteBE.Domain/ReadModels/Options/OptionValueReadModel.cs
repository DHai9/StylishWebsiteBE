using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Products;

namespace StylishWebsiteBE.Domain.ReadModels.Options {
    public class OptionValueReadModel : FullAuditedEntity<Guid> {
        public Guid OptionId { get; set; }
        public string Value { get; set; }
        public virtual OptionReadModel Option { get; set; }
        public virtual ICollection<VariantValueReadModel> VariantValues { get; set; }
    }
}
