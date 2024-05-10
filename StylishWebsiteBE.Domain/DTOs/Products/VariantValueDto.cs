using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Domain.EntityDtos.Options;
using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Domain.DTOs.Products {
    public class VariantValueDto : FullAuditedEntity<Guid> {
        public VariantValueDto(Guid id)
        {
            Id = id;
        }
        public Guid ProductId { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid ProductOptionId { get; set; }
        public Guid OptionId { get; set; }
        public Guid ValueId { get; set; }
        public string OptionName { get; set; } = "";
        public string OptionValue { get; set; } = "";
        public virtual ProductOptionDto ProductOptions { get; set; }
        public virtual OptionDto Options { get; set; }
        public virtual OptionValueDto OptionValues { get; set; }
        public virtual ProductVariantDto ProductVariant { get; set; }
    }
}
