using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Cards;
using StylishWebsiteBE.Domain.ReadModels.Orders;

namespace StylishWebsiteBE.Domain.ReadModels.Products {
    public class ProductVariantReadModel : FullAuditedEntity<Guid> {
        public ProductVariantReadModel(Guid id)
        {
            Id = id;
        }
        public Guid ProductId { get; set; }
        public string SkuId { get; set; }
        public long ImportPrice { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<string> Images { get; set; }
        public virtual ProductReadModel Product { get; set; }
        public virtual ICollection<VariantValueReadModel> VariantValues { get; set; }
        public virtual ICollection<CartDetailReadModel> CartDetails { get; set; }
        public virtual ICollection<OrderDetailReadModel> OrderDetails { get; set; }
    }
}
