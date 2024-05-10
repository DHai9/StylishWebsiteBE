using PupuCore.Data.Relational.Repositories.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;

namespace StylishWebsiteBE.Infrastructure.Repositories.Identities {
    public class RoleRepository : Repository<ApplicationRoleReadModel>, IRoleRepository {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
