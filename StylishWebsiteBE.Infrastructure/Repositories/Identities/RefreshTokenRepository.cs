using PupuCore.Data.Relational.Repositories.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;

namespace StylishWebsiteBE.Infrastructure.Repositories.Identities {
    public class RefreshTokenRepository : Repository<RefreshTokenReadModel>, IRefreshTokenRepository {
        private readonly ApplicationDbContext _dbContext;
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
