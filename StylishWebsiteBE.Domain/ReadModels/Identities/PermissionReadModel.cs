using PupuCore.Domain.Implements;
using StylishWebsiteBE.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StylishWebsiteBE.Domain.ReadModels.Identities {
    public class PermissionReadModel : FullAuditedEntity<Guid>{
        public PermissionReadModel(PermissionType type, Guid typeId, Guid functionId)
        {
            Type = type;
            TypeId = typeId;
            FunctionId = functionId;
        }
        public PermissionType Type { get; set; }
        public Guid TypeId { get; set; }
        [ForeignKey("Function")]
        public Guid FunctionId { get; set; }
        public FunctionReadModel Function { get; set; }
    }
}
