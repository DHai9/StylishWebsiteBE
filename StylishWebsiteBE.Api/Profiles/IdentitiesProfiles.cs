using AutoMapper;
using StylishWebsiteBE.Api.ViewModels.Identities;
using StylishWebsiteBE.Domain.DTOs.Identities;

namespace StylishWebsiteBE.Api.Profiles {
    public class IdentitiesProfiles : Profile {
        public IdentitiesProfiles()
        {
            CreateMap<UserViewModel,UserDto>().ReverseMap();
        }
    }
}
