using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using PupuCore.AspNetCore.Http;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.DTOs.Statisticals;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Orders;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticalsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductVariantService _productVariantService;
        private readonly ILogger<UserStatisticalsController> _logger;

        public UserStatisticalsController(ILogger<UserStatisticalsController> logger, IMapper mapper,
            IOrderService orderService, IOrderDetailService orderDetailService,
            IUserService userService, IProductService productService,
            ICartService cartService, ICartRepository cartRepository
            , IProductVariantService productVariantService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _productVariantService = productVariantService ?? throw new ArgumentNullException(nameof(productVariantService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        //[EnableQuery]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<UserStatisticalsDto> queryOptions)
        {
            var results = new List<UserStatisticalsDto>();
            var users = await _userService.GetAll();
            foreach (var user in users)
            {
                var orderUsers = await _orderService.GetOrderByUserId(user.Id);
                orderUsers = orderUsers.Where(entity => entity.Status == 4).ToList();
                if (orderUsers.Count() == 0)
                    continue;
                var data = new UserStatisticalsDto();
                data.Id = user.Id;
                data.UserName = user.UserName;
                data.CountOrder = orderUsers.Count();
                data.SumMoney = orderUsers.Sum(entity => entity.TotalAmount);
                data.MinDate = orderUsers.Min(entity => entity.CreatedTime);
                data.MaxDate = orderUsers.Max(entity => entity.CreatedTime);
                results.Add(data);
            }
            var finalResult = queryOptions.ApplyTo(results.AsQueryable());
            var odataFeature = HttpContext.ODataFeature();
            return PageResultMessage.Success(finalResult, odataFeature.TotalCount);
        }
        // Get statistical products bestsaller
        [HttpGet("GetUserUserStatisticals")]
        public async Task<ResultMessageBase> GetUserUserStatisticalsAsync([FromODataUri] int month, [FromODataUri] int year)
        {
            var results = new List<UserStatisticalsDto>();
            var users = await _userService.GetAll();
            foreach (var user in users)
            {
                var orderUsers = await _orderService.GetOrderByUserId(user.Id);
                orderUsers = orderUsers.Where(entity => entity.Status == 4).Where(entity => entity.CreatedTime.Month == month && entity.CreatedTime.Year == year).ToList();
                if (orderUsers.Count() == 0)
                    continue;
                var data = new UserStatisticalsDto();
                data.Id = user.Id;
                data.UserName = user.UserName;
                data.CountOrder = orderUsers.Count();
                data.SumMoney = orderUsers.Sum(entity => entity.TotalAmount);
                data.MinDate = orderUsers.Min(entity => entity.CreatedTime);
                data.MaxDate = orderUsers.Max(entity => entity.CreatedTime);
                results.Add(data);
            }
            return SingleResultMessage.Success(results);
        }
    }
}
