using PupuCore.Data.Relational.Repositories.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;

namespace StylishWebsiteBE.Infrastructure.Repositories.Identities {
    public class UserRepository : Repository<ApplicationUserReadModel>, IUserRepository {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
