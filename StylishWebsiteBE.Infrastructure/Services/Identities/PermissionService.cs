using AutoMapper;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Identities;

namespace StylishWebsiteBE.Infrastructure.Services.Identities {
    public class PermissionService : IPermissionService {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
