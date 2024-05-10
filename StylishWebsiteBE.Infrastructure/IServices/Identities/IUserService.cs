using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Domain.ReadModels.Identities;

namespace StylishWebsiteBE.Infrastructure.IServices.Identities {
    public interface IUserService {
        public string PasswordMD5(string password);
        public string GetPasswordSalt();
        public UserDto GetUserByName(string userName);
        public Task<UserDto> GetUserByNameAsync(string userName);
        public Task<UserDto> CreateAsync(UserDto userDto, string encryptPassword);
        public Task AddToRoleAsync(Guid id, List<string> roles);
        public Task<bool> IsInRoleAsync(Guid id, string role);
        public Task<UserDto> GetUserByIdAsync(Guid id);
        public Task<ApplicationUserReadModel> IsExistedUserAsync(string userName, string password);
        public Task<UserDto> SignInAsync(ApplicationUserReadModel user);
        public Task<List<UserDto>> GetAll();
    }
}
