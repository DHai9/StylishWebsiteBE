using PupuCore.Data.Relational.Repositories.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;

namespace StylishWebsiteBE.Infrastructure.Repositories.Identities {
    public class FunctionRepository : Repository<FunctionReadModel>, IFunctionRepository {
        private readonly ApplicationDbContext _dbContext;
        public FunctionRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
