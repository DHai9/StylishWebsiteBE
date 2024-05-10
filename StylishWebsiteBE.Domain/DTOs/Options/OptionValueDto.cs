using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.DTOs.Options {
    public class OptionValueDto : FullAuditedEntity<Guid>{
        public OptionValueDto(Guid id)
        {
            Id = id;
        }
        public Guid OptionId { get; set; }
        public string Value { get; set; }
    }
}
