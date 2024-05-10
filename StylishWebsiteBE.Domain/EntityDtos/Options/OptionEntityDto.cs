using StylishWebsiteBE.Domain.Enums;

namespace StylishWebsiteBE.Domain.EntityDtos.Options {
    public class OptionEntityDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OptionType OptionType { get; set; }
        public ICollection<OptionValuesEntityDto> OptionValues { get; set; } 
    }
}
