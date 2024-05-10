using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Options;

namespace StylishWebsiteBE.Domain.ReadModels.Products {
    public class VariantValueReadModel : FullAuditedEntity<Guid>{
        public VariantValueReadModel(Guid id)
        {
            Id = id;
        }
        public Guid ProductId { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid ProductOptionId { get; set; }
        public Guid OptionId { get; set; }
        public Guid ValueId { get; set; }

        public virtual ProductReadModel Products { get; set; }
        public virtual ProductVariantReadModel ProductVariants { get; set; }
        public virtual OptionReadModel Options { get; set; }
        public virtual OptionValueReadModel OptionValues { get; set; }
    }
}
