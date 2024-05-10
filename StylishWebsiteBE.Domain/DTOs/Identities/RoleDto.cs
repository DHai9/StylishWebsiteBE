using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.DTOs.Identities {
    public class RoleDto : FullAuditedEntity {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}