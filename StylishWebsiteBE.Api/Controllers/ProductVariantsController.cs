using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using StylishWebsiteBE.Api.ViewModels.Products;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using System.Threading.Tasks;
using System;
using PupuCore.AspNetCore.Http;

namespace StylishWebsiteBE.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductVariantsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IProductVariantService _services;
        private readonly ILogger<ProductVariantsController> _logger;

        public ProductVariantsController(ILogger<ProductVariantsController> logger, IProductVariantService productVariantServices, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = productVariantServices ?? throw new ArgumentNullException(nameof(productVariantServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(ProductVariantViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var entityDto = new ProductVariantDto(Guid.NewGuid());
            _mapper.Map<ProductVariantViewModel, ProductVariantDto>(viewModel, entityDto);
            //entity.CreatedBy = userId;
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            var result = _services.Create(entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<ProductVariantDto> queryOptions)
        {
            var results = _services.GetAll();
            var finalResult = queryOptions.ApplyTo(results);
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
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, ProductVariantViewModel viewModel)
        {
            var entityDto = new ProductVariantDto(key);
            _mapper.Map<ProductVariantViewModel, ProductVariantDto>(viewModel, entityDto);
            var result = _services.Update(key, entityDto);
            return SingleResultMessage.Success(result);
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
