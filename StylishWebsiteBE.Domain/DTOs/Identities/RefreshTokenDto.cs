using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.DTOs.Identities {
    public class RefreshTokenDto : FullAuditedEntity<Guid> {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpireTime { get; set; }
        public DateTimeOffset AccessTokenExpireTime { get; set; }
        public Guid UserId { get; set; }
    }

}
