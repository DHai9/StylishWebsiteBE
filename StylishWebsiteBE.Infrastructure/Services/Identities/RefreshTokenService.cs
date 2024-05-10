using AutoMapper;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Identities;

namespace StylishWebsiteBE.Infrastructure.Services.Identities {
    public class RefreshTokenService : IRefreshTokenService {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
        {
            _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
