using StylishWebsiteBE.Domain.EntityDtos.Options;

namespace StylishWebsiteBE.Domain.EntityDtos.Products {
    public class VariantValueEntityDto {
        public VariantValueEntityDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid ProductOptionId { get; set; }
        public Guid OptionId { get; set; }
        public Guid ValueId { get; set; }
        public string OptionName { get; set; } = "";
        public string OptionValue { get; set; } = "";
        public virtual OptionEntityDto Options { get; set; }
        public virtual OptionValuesEntityDto OptionValues { get; set; }
    }
}
