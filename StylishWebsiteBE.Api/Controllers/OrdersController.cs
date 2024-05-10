using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using StylishWebsiteBE.Api.ViewModels.Orders;
using PupuCore.AspNetCore.Http;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Infrastructure.IServices.Orders;
using System.Collections.Generic;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;
using StylishWebsiteBE.Infrastructure.Repositories.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using StylishWebsiteBE.Domain.EntityDtos.Products;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PupuCore.Extensions;
using StylishWebsiteBE.Domain.EntityDtos.Orders;
using PupuCore.Infrastructure.Exceptions;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IOrderService _services;
        private readonly IOrderDetailService _servicesDetail;
        private readonly IProductVariantService _productVariantService;
        private readonly ICartService _cartService;
        private readonly ILogger<OptionsController> _logger;

        public OrdersController(ILogger<OptionsController> logger, IMapper mapper,
            IOrderService orderService, IOrderDetailService orderDetailService,
            ICartService cartService, ICartRepository cartRepository,
            IProductVariantService productVariantService
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _servicesDetail = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
            _productVariantService = productVariantService ?? throw new ArgumentNullException(nameof(productVariantService));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(OrderViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");

            // Create Order
            var entityDto = new OrderDto(Guid.NewGuid());
            viewModel.OrderDetails.ForEach(entity => { entity.Id = Guid.NewGuid(); entity.OrderId = entityDto.Id; });
            _mapper.Map<OrderViewModel, OrderDto>(viewModel, entityDto);
            //entity.CreatedBy = userId;
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            entityDto.Status = 0;
            entityDto.TotalAmount = viewModel.OrderDetails.Sum(entity => entity.Quantity * entity.Price);
            var result = _services.Create(entityDto);
            // delete all item in cart
            _cartService.EmptyCartItem(viewModel.CartId);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<OrderDto> queryOptions)
        {
            var results = _services.GetAll();
            var newRes = results.ToList();
            var finalResult = queryOptions.ApplyTo(newRes.AsQueryable());
            var odataFeature = HttpContext.ODataFeature();
            return PageResultMessage.Success(finalResult, odataFeature.TotalCount);
        }

        [HttpGet("key")]
        public async Task<ResultMessageBase> GetAsync([FromODataUri] Guid key)
        {
            var result = _services.GetById(key);
            return SingleResultMessage.Success(result);
        }

        [HttpPut("{key}")]
        [EnableQuery]
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, OrderViewModel viewModel)
        {
            var entityDto = _services.GetById(key);
            var newItems = viewModel.OrderDetails.Where(entity => entity.Id.IsNullOrDefault()).ToList();
            if (entityDto.Status >= 2 && viewModel.Status == 8)
            {
                var ids = viewModel.OrderDetails.ToList().Select(entity => entity.ProductVariantId);
                var entities = _productVariantService.GetAll().Where(entity => ids.Contains(entity.Id));
                foreach (var itemOrder in entityDto.OrderDetails)
                {
                    var entity = entities.FirstOrDefault(e => e.Id == itemOrder.ProductVariantId);
                    entity.Quantity -= itemOrder.Quantity;
                    await _productVariantService.UpdateAsync(entity);
                }
            }
            _mapper.Map<OrderViewModel, OrderDto>(viewModel, entityDto);
            var newOld = viewModel.OrderDetails.Where(entity => !entity.Id.IsNullOrDefault()).ToList();
            if (newItems.Count > 0)
            {
                await _servicesDetail.CreateAsync(_mapper.Map<List<OrderDetailViewModel>, List<OrderDetailDto>>(newItems));
            }
            // check status order == 2 "tiếp nhận" cập nhật số lượng vào database
            if (entityDto.Status == 2)
            {
                var ids = viewModel.OrderDetails.ToList().Select(entity => entity.ProductVariantId);
                var entities = _productVariantService.GetAll().Where(entity => ids.Contains(entity.Id));
                foreach (var itemOrder in viewModel.OrderDetails)
                {
                    var entity = entities.FirstOrDefault(e => e.Id == itemOrder.ProductVariantId);
                    entity.Quantity -= itemOrder.Quantity;
                    await _productVariantService.UpdateAsync(entity);;
                }
            }
            // update total amount
            entityDto.TotalAmount = viewModel.OrderDetails.Sum(entity => entity.Quantity * entity.Price);
            var result = _services.Update(key, entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpPost("DeleteItem")]
        [EnableQuery]
        public async Task<ResultMessageBase> DeleteItemAsync(DeleteItemOrderViewModel viewModel)
        {
            var itemDeleted = await _servicesDetail.DeleteAsync(viewModel.ItemId);
            var orderUpdate = _services.GetById(viewModel.OrderId);
            if (orderUpdate != null)
            {
                orderUpdate.TotalAmount -= itemDeleted.Quantity * itemDeleted.Price;
            }
            var orderUpdated = await _services.UpdateAsync(orderUpdate);
            orderUpdated.OrderDetails = _servicesDetail.GetAll().Where(entity => entity.OrderId == orderUpdated.Id).ToList();
            return SingleResultMessage.Success(orderUpdated);
        }

        [HttpDelete("key")]
        [EnableQuery]
        public async Task<ResultMessageBase> DeleteAsync([FromODataUri] Guid key)
        {
            var result = await _services.DeleteAsync(key);
            return SingleResultMessage.Success(result);
        }
    }
}
