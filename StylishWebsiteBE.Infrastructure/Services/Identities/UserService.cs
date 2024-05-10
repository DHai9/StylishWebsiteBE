using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PupuCore.Extensions;
using PupuCore.Infrastructure.Exceptions;
using PupuCore.Utilities;
using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Identities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StylishWebsiteBE.Infrastructure.Services.Identities {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUserReadModel> _manager;
        private readonly IConfiguration _configuration;
        private readonly string PasswordSalt = "HTP_02_MA";
        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, UserManager<ApplicationUserReadModel> manager)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public string PasswordMD5(string password)
        {
            var encryptPassword = (password.Md5Hash() + GetPasswordSalt()).Md5Hash();
            return encryptPassword;
        }

        public string GetPasswordSalt()
        {
            return PasswordSalt;
        }

        public UserDto GetUserByName(string userName)
        {
            var currUser = _userRepository.AsQueryable().AsNoTracking().FirstOrDefault(user => user.UserName.Equals(userName));
            return _mapper.Map<UserDto>(currUser);
        }

        public async Task<UserDto> GetUserByNameAsync(string userName)
        {
            var currUser = await _userRepository.AsQueryable().AsNoTracking().FirstOrDefaultAsync(user => user.UserName.Equals(userName));
            return _mapper.Map<UserDto>(currUser);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto, string encryptPassword)
        {
            var entity = _mapper.Map<ApplicationUserReadModel>(userDto);
            entity.PasswordSalt = new RandomStringGenerator().GenerateUnique();
            var newEncryptPassword = (encryptPassword.Md5Hash() + PasswordSalt).Md5Hash() + entity.PasswordSalt.Md5Hash();

            await _manager.CreateAsync(entity, newEncryptPassword);
            return await GetUserByIdAsync(userDto.Id);
        }

        public async Task AddToRoleAsync(Guid id, List<string> roles)
        {
            var user = await _manager.FindByIdAsync(id.ToString());
            if (user.IsNullOrDefault())
                throw new ArgumentNullException("User");
            foreach (var role in roles)
            {
                if (await _manager.IsInRoleAsync(user, role))
                    continue;
                else
                    await _manager.AddToRoleAsync(user, role);
            }
        }

        public async Task<bool> IsInRoleAsync(Guid id, string role)
        {
            var user = await _manager.FindByIdAsync(id.ToString());
            if (user.IsNullOrDefault())
                throw new ArgumentNullException("User");
            return await _manager.IsInRoleAsync(user, role);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var result = await _userRepository.AsQueryable().AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<ApplicationUserReadModel> IsExistedUserAsync(string userName, string password)
        {
            var user = await _manager.FindByNameAsync(userName);

            if (user.IsNullOrDefault())
                user = await _manager.FindByEmailAsync(userName);
            if (user.IsNullOrDefault())
            {
                var users = _userRepository.AsQueryable().AsNoTracking();
                user = users.FirstOrDefault(entity => string.Equals(entity.PhoneNumber, userName));
                if (user.IsNullOrDefault())
                    throw new BusinessException("Identity.Infrastructure", "IncorrectAccountOrPassword", 400, "Incorrect Password Or Account");
                if (!user.PhoneNumberConfirmed)
                    throw new BusinessException("Identity.Infrastructure", "AccountIsNotActive", 400, "You need confirm otp");
            }
            if (!user.IsNullOrDefault())
            {
                var passwordSalt = user.PasswordSalt;
                var encryptPassword = (password.Md5Hash() + PasswordSalt).Md5Hash() + passwordSalt.Md5Hash();
                if (!await _manager.CheckPasswordAsync(user, encryptPassword))
                    throw new BusinessException("Identity.Infrastructure", "WrongPassword", 400, "Incorrect Password");
                return user;
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task<UserDto> SignInAsync(ApplicationUserReadModel user)
        {
            var result = new UserDto();
            if (user.IsNullOrDefault())
                return result;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim("username" , user.UserName),
                new Claim("nameIdentifier" ,user.Id.ToString()),
            };

            var userRoles = await _manager.GetRolesAsync(user);
            // swicth role for url web path 
            foreach (var role in userRoles)
            {
                switch (role)
                {
                    case "Administrators":
                        {
                            authClaims.Add(new Claim("sw_path_property", "mgn_path_type_admin"));
                            authClaims.Add(new Claim("sw_numpath_property", "0"));
                            break;
                        }
                    default:
                        {
                            authClaims.Add(new Claim("sw_path_property", "mgn_path_type_user"));
                            authClaims.Add(new Claim("sw_numpath_property", "1"));
                            break;
                        }
                }
            }

            var expireTime = DateTime.Now.AddHours(_configuration["Jwt:ExpiresInHours"].ConvertTo<double>());

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: expireTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            result = _mapper.Map<UserDto>(user);
            result.AccessTokenExpireTime = expireTime;
            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }

        public Task<List<UserDto>> GetAll()
        {
            var results = _userRepository.Entities.ToList();
            return Task.FromResult(_mapper.Map<List<UserDto>>(results));
        }
    }
}
