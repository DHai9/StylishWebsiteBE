using AutoMapper;
using StylishWebsiteBE.Api.ViewModels.Options;
using StylishWebsiteBE.Domain.DTOs.Options;

namespace StylishWebsiteBE.Api.Profiles {
    public class OptionProfiles : Profile{
        public OptionProfiles()
        {
            CreateMap<OptionViewModel, OptionDto>().ReverseMap();
            CreateMap<OptionValueViewModel, OptionValueDto>().ReverseMap();
        }
    }
}
