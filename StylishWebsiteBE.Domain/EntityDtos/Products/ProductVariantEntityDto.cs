namespace StylishWebsiteBE.Domain.EntityDtos.Products {
    public class ProductVariantEntityDto {
        public ProductVariantEntityDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string SkuId { get; set; }
        public long ImportPrice { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<string> Images { get; set; }
        public ICollection<VariantValueEntityDto> VariantValues { get; set; }
    
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
