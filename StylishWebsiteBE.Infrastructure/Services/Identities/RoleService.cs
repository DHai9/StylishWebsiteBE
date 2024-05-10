using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Identities;

namespace StylishWebsiteBE.Infrastructure.Services.Identities {
    public class RoleService : IRoleService {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<ApplicationRoleReadModel> _manager;

        public RoleService(IRoleRepository roleRepository, RoleManager<ApplicationRoleReadModel> manager, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RoleDto> GetRoleByName(string name)
        {
            var result = await _roleRepository.AsQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
            return _mapper.Map<RoleDto>(result); ;
        }

        public async Task<RoleDto> CreateAsync(RoleDto role)
        {
            var entity = _mapper.Map<ApplicationRoleReadModel>(role);
            await _manager.CreateAsync(entity);
            return await GetRoleByName(entity.Name);
        }

    }
}
