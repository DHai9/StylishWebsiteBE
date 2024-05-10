using PupuCore.Data.Relational.Repositories.Interfaces;
using StylishWebsiteBE.Domain.ReadModels.Identities;

namespace StylishWebsiteBE.Infrastructure.IRepositories.Identities {
    public interface IPermissionRepository : IRepository<PermissionReadModel> {
    }
}
