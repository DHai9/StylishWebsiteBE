using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Domain.DTOs.Products {
    public class ProductVariantDto : FullAuditedEntity<Guid> {
        public ProductVariantDto(Guid id)
        {
            Id = id;
        }
        public Guid ProductId { get; set; }
        public string SkuId { get; set; }
        public long ImportPrice { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Sale { get; set; }
        public ICollection<string> Images { get; set; }
        public virtual ICollection<VariantValueDto> VariantValues { get; set; }
        public void GetFullInfoVariantValue()
        {
            foreach (var item in VariantValues)
            {
                item.OptionName = item.Options.Name;
                item.OptionValue = item.OptionValues.Value;
            }
        }
    }
}
