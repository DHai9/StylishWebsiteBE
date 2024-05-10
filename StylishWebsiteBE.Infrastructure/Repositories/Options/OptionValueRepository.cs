using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Options;
using StylishWebsiteBE.Infrastructure.IRepositories.Options;

namespace StylishWebsiteBE.Infrastructure.Repositories.Options {
    public class OptionValueRepository : RepositoryBase<OptionValueReadModel>, IOptionValueRepository {
        private readonly ApplicationDbContext _dbContext;
        public OptionValueRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
