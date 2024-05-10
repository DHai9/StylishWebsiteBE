using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.EntityDtos.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StylishWebsiteBE.Domain.DTOs.Products
{
    public class ProductOptionDto : FullAuditedEntity<Guid>
    {
        public ProductOptionDto(Guid id)
        {
            Id = id;
        }
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }
        public OptionEntityDto Option { get; set; }
    }
}
