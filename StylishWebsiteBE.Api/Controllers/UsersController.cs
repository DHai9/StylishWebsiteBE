using AutoMapper;
using PupuCore.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using PupuCore.AspNetCore.Http;
using PupuCore.Extensions;
using PupuCore.Utilities;
using StylishWebsiteBE.Api.ViewModels.Identities;
using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Identities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StylishWebsiteBE.Infrastructure.IServices.Carts;
using StylishWebsiteBE.Domain.DTOs.Cards;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IUserService _services;
        private readonly IRoleService _roleService;
        private readonly ICartService _cartServices;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserService service, IRoleService roleService, ICartService cartServices, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = service ?? throw new ArgumentNullException(nameof(service));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _cartServices = cartServices ?? throw new ArgumentNullException(nameof(cartServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }


        [HttpPost("Register")]
        [EnableQuery]
        [AllowAnonymous]
        public async Task<ResultMessageBase> RegisterAsync([FromBody] UserViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.UserName))
                throw new ArgumentNullException("Identity UserName");
            if (string.IsNullOrEmpty(viewModel.Password))
                throw new ArgumentNullException("Identity Password");
            if (string.IsNullOrEmpty(viewModel.ConfirmPassword))
                throw new ArgumentNullException("Identity ConfirmPassword");
            if (!string.Equals(viewModel.Password, viewModel.ConfirmPassword))
                throw new ArgumentNullException("PasswWord dosen't match");

            var checkPassword = new Regex(@"^(?!.* )(?=.*\d)(?=.*[A-Z]).{6,20}$");
            if (!checkPassword.IsMatch(viewModel.Password) || !checkPassword.IsMatch(viewModel.ConfirmPassword))
                throw new BusinessException("Identity", "IncorrectPasswordFormat", 403, "Please Check Password Entered");

            var id = Guid.NewGuid();

            var checkEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var checkPhoneNumber = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");

            var user = await _services.GetUserByNameAsync(viewModel.UserName);

            if (!user.IsNullOrDefault() && !user.Id.IsNullOrDefault() && user.PhoneNumberConfirmed)
                throw new BusinessException("Identity", "IncorrectPasswordFormat", 403, "Please Check Password Entered");
            if (!user.IsNullOrDefault() && !user.Id.IsNullOrDefault() && !user.PhoneNumberConfirmed)
                throw new BusinessException("ShopFComputerBackEnd.Identity.Api", "AccountIsNotActive", 400, "You need confirm otp");
            if (!user.IsNullOrDefault() && !user.Id.IsNullOrDefault() && user.IsDeleted)
                throw new BusinessException("ShopFComputerBackEnd.Identity.Api", "UserNotAvailable", 400, "User IsDeleted");

            var entityDto = new UserDto();
            _mapper.Map(viewModel, entityDto);
            entityDto.Id = id;

            if (checkEmail.IsMatch(viewModel.UserName))
            {
                entityDto.Email = viewModel.UserName;
                var replaceCharacter = "User" + id;
                viewModel.UserName = replaceCharacter.Replace("-", "");
            }
            else if (checkPhoneNumber.IsMatch(viewModel.UserName))
            {
                entityDto.PhoneNumber = viewModel.UserName;
                var replaceCharacter = "User" + id;
                viewModel.UserName = replaceCharacter.Replace("-", "");
            }
            else
            {
                entityDto.UserName = viewModel.UserName;
            }
            entityDto.CreatedBy = id;
            var random = new RandomStringGenerator(false, false, true, false);
            entityDto.OtpVerify = random.Generate(6);
            var result = await _services.CreateAsync(entityDto, viewModel.Password);

            var roleNameCollection = new List<string>();
            roleNameCollection.Add("Administrators");

            #region Check Role

            var resultQueryCheckRole = await _roleService.GetRoleByName("Administrators");
            if (resultQueryCheckRole.IsNullOrDefault())
                throw new EntityNotFoundException();
            var isInRole = await _services.IsInRoleAsync(id, "Administrators");
            if (isInRole)
                throw new EntityNotFoundException();

            await _services.AddToRoleAsync(id, roleNameCollection);
            #endregion

            #region Create cart

            var cartEntityDto = new CartDto(Guid.NewGuid());
            cartEntityDto.UserId = id;
            cartEntityDto.CreatedBy = id;
            cartEntityDto.CreatedTime = DateTimeOffset.UtcNow;
            _cartServices.Create(cartEntityDto);

            #endregion

            return SingleResultMessage.Success(result);
        }

        [HttpPost("SignIn")]
        [EnableQuery]
        [AllowAnonymous]
        public async Task<ResultMessageBase> SignInAsync([FromBody] LoginViewModel viewModel)
        {
            var user = new UserDto();
            
            // validation
            if (string.IsNullOrEmpty(viewModel.UserName))
                throw new ArgumentNullException("Identity UserName");
            if (string.IsNullOrEmpty(viewModel.Password))
                throw new ArgumentNullException("Identity Password");
            // check username password
            var isExitUser = await _services.IsExistedUserAsync(viewModel.UserName, viewModel.Password);
            if (!isExitUser.IsNullOrDefault())
            {
                user = await _services.SignInAsync(isExitUser);
            }
            return SingleResultMessage.Success(user);
        }
    }
}
