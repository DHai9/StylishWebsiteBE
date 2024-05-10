using PupuCore.Domain.Implements;

namespace StylishWebsiteBE.Domain.DTOs.Identities {
    public class UserDto : FullAuditedEntity<Guid>{
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string OtpVerify { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset AccessTokenExpireTime { get; set; }
        public DateTimeOffset RefreshTokenExpireTime { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}
