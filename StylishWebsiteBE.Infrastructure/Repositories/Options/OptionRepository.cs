using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Options;
using StylishWebsiteBE.Infrastructure.IRepositories.Options;

namespace StylishWebsiteBE.Infrastructure.Repositories.Options {
    public class OptionRepository : RepositoryBase<OptionReadModel>, IOptionRepository {
        private readonly ApplicationDbContext _dbContext;
        public OptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
