using AutoMapper;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Domain.EntityDtos.Options;
using StylishWebsiteBE.Domain.ReadModels.Options;

namespace StylishWebsiteBE.Infrastructure.Profiles
{
    public class OptionProfiles : Profile {
        public OptionProfiles()
        {
            CreateMap<OptionReadModel, OptionDto>().ReverseMap();
            CreateMap<OptionReadModel, OptionEntityDto>().ReverseMap();
            CreateMap<OptionDto, OptionEntityDto>().ReverseMap();
            CreateMap<OptionValueReadModel, OptionValueDto>().ReverseMap();
            CreateMap<OptionValueReadModel, OptionValuesEntityDto>().ReverseMap();
        }
    }
}
