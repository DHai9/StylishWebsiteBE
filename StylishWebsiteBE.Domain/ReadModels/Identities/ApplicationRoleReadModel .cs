﻿using Microsoft.AspNetCore.Identity;
using PupuCore.Domain.Interfaces.Audited;

namespace StylishWebsiteBE.Domain.ReadModels.Identities {
    public class ApplicationRoleReadModel : IdentityRole<Guid>, IAuditedEntity {
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
    }
}