using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.ReadModels.Identities {
    public class FunctionReadModel : FullAuditedEntity<Guid> {
        public string ServiceName { get; set; }
        public string FunctionName { get; set; }
        public ICollection<PermissionReadModel> Permissions { get; set; }
    }
}
