using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.EntityDtos.Options;
using StylishWebsiteBE.Domain.Enums;

namespace StylishWebsiteBE.Domain.DTOs.Options {
    public class OptionDto : FullAuditedEntity<Guid> {
        public OptionDto(Guid id)
        {
            Id = id;    
        }
        public string Name { get; set; }
        public OptionType OptionType { get; set; }
        public virtual ICollection<OptionValueDto> OptionValues { get; set; }
    }
}
