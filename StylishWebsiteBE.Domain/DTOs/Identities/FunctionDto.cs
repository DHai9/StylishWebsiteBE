using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.DTOs.Identities {
    public class FunctionDto : FullAuditedEntity<Guid> {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public string FunctionName { get; set; }
    }
}
