using StylishWebsiteBE.Domain.DTOs.Identities;

namespace StylishWebsiteBE.Infrastructure.IServices.Identities {
    public interface IRoleService {
        public Task<RoleDto> GetRoleByName(string name);
        public Task<RoleDto> CreateAsync(RoleDto role);
    }
}
