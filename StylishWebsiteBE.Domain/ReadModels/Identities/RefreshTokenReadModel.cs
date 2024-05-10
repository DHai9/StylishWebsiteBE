using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.ReadModels.Identities {
    public class RefreshTokenReadModel : FullAuditedEntity<Guid>{
        public RefreshTokenReadModel(Guid id, Guid userId, string deviceId, string refreshToken)
        {
            Id = id;
            UserId = userId;
            DeviceId = deviceId;
            RefreshToken = refreshToken;
            ExpiredTime = DateTimeOffset.UtcNow.AddDays(7);
            RevokedTime = DateTimeOffset.UtcNow;
        }
        public Guid UserId { get; set; }
        public string DeviceId { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpiredTime { get; set; }
        public DateTimeOffset RevokedTime { get; set; }
    }
}
