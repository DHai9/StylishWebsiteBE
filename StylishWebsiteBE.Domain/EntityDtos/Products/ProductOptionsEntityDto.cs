using StylishWebsiteBE.Domain.EntityDtos.Options;

namespace StylishWebsiteBE.Domain.EntityDtos.Products {
    public class ProductOptionsEntityDto
    {
        public ProductOptionsEntityDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }
        public virtual ProductEntityDto Product { get; set; }
        public virtual OptionEntityDto Option { get; set; }
    }
}
