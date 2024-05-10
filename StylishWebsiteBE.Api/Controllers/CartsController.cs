using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PupuCore.AspNetCore.Http;
using PupuCore.Extensions;
using StylishWebsiteBE.Api.ViewModels.Carts;
using StylishWebsiteBE.Domain.DTOs.Cards;
using StylishWebsiteBE.Infrastructure.IServices.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly ICartService _services;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductVariantService _productVariantService;
        private readonly ILogger<CartsController> _logger;

        public CartsController(ILogger<CartsController> logger, ICartService cartService, ICartDetailService cartDetailService, IProductVariantService productVariantService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _cartDetailService = cartDetailService ?? throw new ArgumentNullException(nameof(cartDetailService));
            _productVariantService = productVariantService ?? throw new ArgumentNullException(nameof(productVariantService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(CartViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var entityDto = new CartDto(Guid.NewGuid());
            viewModel.CartDetails?.ForEach(ov => { ov.CartId = entityDto.Id; ov.Id = Guid.NewGuid(); });
            _mapper.Map<CartViewModel, CartDto>(viewModel, entityDto);
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            //entity.CreatedBy = userId;
            var result = _services.Create(entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpPost("AddItem")]
        [EnableQuery]
        public async Task<ResultMessageBase> AddItemAsync(AddItemCartViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var results = _services.GetAll();
            var finalResults = await results.FirstOrDefaultAsync(el => el.UserId == viewModel.UserId);
            var entityDto = finalResults.CartDetails.FirstOrDefault(entity => entity.ProductVariantId.Equals(viewModel.ProductVariantId));
            if (entityDto.IsNullOrDefault())
            {
                entityDto = new CartDetailDto(Guid.NewGuid());
                entityDto.ProductVariantId = viewModel.ProductVariantId;
                entityDto.CartId = viewModel.CartId;
                entityDto.Quantity = 1;
                if (viewModel.Quantity != null)
                {
                    entityDto.Quantity = (int)viewModel.Quantity;
                }
                entityDto.Name = viewModel.ProductName;
                entityDto.ImageUrl = viewModel.ImageUrl;
                entityDto.CreatedTime = DateTimeOffset.UtcNow;
                //entity.CreatedBy = userId;
                await _cartDetailService.CreateAsync(entityDto);
            }
            else
            {
                entityDto.Quantity++;
                await _cartDetailService.UpdateAsync(entityDto);
            }
            results = _services.GetAll();
            finalResults = await results.FirstOrDefaultAsync(el => el.UserId == viewModel.UserId);
            return SingleResultMessage.Success(finalResults);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<CartDto> queryOptions)
        {
            var results = _services.GetAll();
            var finalResult = queryOptions.ApplyTo(results);
            var odataFeature = HttpContext.ODataFeature();
            return PageResultMessage.Success(finalResult, odataFeature.TotalCount);
        }

        [HttpGet("GetByUserId")]
        [EnableQuery]
        public async Task<ResultMessageBase> GetByUserIdAsync([FromODataUri] Guid userId)
        {
            var results = _services.GetAll();
            var finalResults = await results.FirstOrDefaultAsync(el => el.UserId == userId);
            return SingleResultMessage.Success(finalResults);
        }

        [HttpGet("key")]
        public async Task<ResultMessageBase> GetAsync([FromODataUri] Guid key)
        {
            var result = _services.GetById(key);
            return SingleResultMessage.Success(result);
        }

        [HttpPut("{key}")]
        [EnableQuery]
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, CartViewModel viewModel)
        {
            #region update option value
            viewModel.CartDetails.ForEach(ov =>
            {
                if (ov.Id.IsNullOrDefault())
                {
                    ov.Id = Guid.NewGuid();
                    ov.CartId = key;
                    _cartDetailService.Create(_mapper.Map<CartDetailViewModel, CartDetailDto>(ov));
                }
            });

            #endregion
            var entityDto = new CartDto(key);
            _mapper.Map<CartViewModel, CartDto>(viewModel, entityDto);
            var result = _services.Update(key, entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpDelete("{key}")]
        [EnableQuery]
        public async Task<ResultMessageBase> DeleteAsync([FromODataUri] Guid key)
        {
            var result = await _services.DeleteAsync(key);
            return SingleResultMessage.Success(result);
        }
        [HttpPost("DeleteItem")]
        [EnableQuery]
        public async Task<ResultMessageBase> DeleteItemAsync(DeleteItemViewModel viewModel)
        {
            var result = await _cartDetailService.DeleteAsync(viewModel.ItemId);
            var results = _services.GetAll();
            var finalResults = await results.FirstOrDefaultAsync(el => el.Id == result.CartId);
            return SingleResultMessage.Success(finalResults);
        }
    }
}
