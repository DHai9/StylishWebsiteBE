using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.ReadModels.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StylishWebsiteBE.Domain.ReadModels.Products {
    public class ProductOptionReadModel : FullAuditedEntity<Guid>{
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }
        public virtual ProductReadModel Product { get; set; }
        public virtual OptionReadModel Option { get; set; }
    }
}
