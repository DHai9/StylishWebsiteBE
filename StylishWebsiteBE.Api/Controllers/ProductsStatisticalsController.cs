using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PupuCore.AspNetCore.Http;
using PupuCore.Extensions;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.DTOs.Statisticals;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Orders;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsStatisticalsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IOrderService _services;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductVariantService _productVariantService;
        private readonly ILogger<ProductsStatisticalsController> _logger;

        public ProductsStatisticalsController(ILogger<ProductsStatisticalsController> logger, IMapper mapper,
            IOrderService orderService, IOrderDetailService orderDetailService,
            IUserService userService, IProductService productService,
            ICartService cartService, ICartRepository cartRepository
            , IProductVariantService productVariantService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _productVariantService = productVariantService ?? throw new ArgumentNullException(nameof(productVariantService));
            _services = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        // Get statistical products bestsaller
        [HttpGet]
        //[EnableQuery]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<ProductsStatisticalDto> queryOptions)
        {
            var results = new List<ProductsStatisticalDto>();
            // Get Info product bestsaller
            var products = await _productService.GetAll().ToListAsync();
            var detailOrders = await _orderDetailService.GetAll().ToListAsync();
            var productVariants = await _productVariantService.GetAll().ToListAsync();
            foreach (var variant in productVariants)
            {
                var product = products.FirstOrDefault(entity => entity.Id == variant.ProductId);
                var orderVariants = detailOrders.Where(entity => entity.ProductVariantId == variant.Id);
                var statistical = new ProductsStatisticalDto();
                statistical.Id = variant.Id;
                statistical.SkuId = variant.SkuId;
                statistical.Name = $"{product.Name}-{variant.SkuId}";
                statistical.TotalSale = orderVariants.Sum(entity => entity.Quantity);
                statistical.Revenue = (long)orderVariants.Sum(entity => (entity.Price - entity.ImportPrice) * entity.Quantity);
                results.Add(statistical);
            }
            results = results.OrderByDescending(entity => entity.Revenue).Where(entity => entity.Revenue > 0).ToList();
            var finalResult = queryOptions.ApplyTo(results.AsQueryable());
            var odataFeature = HttpContext.ODataFeature();
            return PageResultMessage.Success(finalResult, odataFeature.TotalCount);
        }
        // Get statistical products bestsaller
        [HttpGet("GetProductsBestSeller")]
        //[EnableQuery]
        public async Task<ResultMessageBase> GetProductsBestSellerAsync([FromODataUri] int? month, [FromODataUri] int? year, [FromODataUri] string skuId)
        {
            var results = new List<ProductsStatisticalDto>();
            // Get Info product bestsaller
            var products = await _productService.GetAll().ToListAsync();
            var detailOrders = await _orderDetailService.GetAll().ToListAsync();
            if (month != null)
                detailOrders = detailOrders.Where(entity => entity.CreatedTime.Month == month).ToList();
            if (year != null)
                detailOrders = detailOrders.Where(entity => entity.CreatedTime.Year == year).ToList();
            var productVariants = new List<ProductVariantDto>();
            if (!skuId.IsNullOrDefault())
                productVariants = await _productVariantService.GetAll().Where(entity => entity.SkuId.Equals(skuId)).ToListAsync();
            else
                productVariants = await _productVariantService.GetAll().ToListAsync();
            foreach (var variant in productVariants)
            {
                var product = products.FirstOrDefault(entity => entity.Id == variant.ProductId);
                var orderVariants = detailOrders.Where(entity => entity.ProductVariantId == variant.Id);
                var statistical = new ProductsStatisticalDto();
                statistical.Id = variant.Id;
                statistical.SkuId = variant.SkuId;
                statistical.Name = $"{product.Name}-{variant.SkuId}";     
                statistical.TotalSale = orderVariants.Sum(entity => entity.Quantity);
                statistical.Revenue = (long)orderVariants.Sum(entity => (entity.Price - entity.ImportPrice));

				statistical.OrderDetails = _mapper.Map<List<OrderDetailDto>, List<OrderDetailStatisticalDto>>(orderVariants.ToList());
                statistical.OrderDetails.ForEach(entity => entity.OrderCode = _services.GetById(entity.OrderId)?.OrderCode);
                results.Add(statistical);
			}
            results = results.Where(entity => entity.Revenue > 0).OrderByDescending(entity => entity.Revenue).ToList();
            return SingleResultMessage.Success(results);
        }
    }
}