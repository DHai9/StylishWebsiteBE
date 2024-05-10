using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.Enums;

namespace StylishWebsiteBE.Domain.DTOs.Identities {
    public class PermissionDto : FullAuditedEntity<Guid>{
        public PermissionType Type { get; set; }
        public Guid TypeId { get; set; }
        public Guid FunctionId { get; set; }
    }
}
