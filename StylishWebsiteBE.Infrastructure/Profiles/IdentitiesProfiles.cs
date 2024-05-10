using AutoMapper;
using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Domain.ReadModels.Identities;

namespace StylishWebsiteBE.Infrastructure.Profiles {
    public class IdentitiesProfiles : Profile{
        public IdentitiesProfiles()
        {
            CreateMap<ApplicationUserReadModel, UserDto>().ReverseMap();            
            CreateMap<ApplicationRoleReadModel, RoleDto>().ReverseMap();            
        }
    }
}
