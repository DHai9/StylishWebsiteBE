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
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StylishWebsiteBE.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductOptionsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IProductOptionService _services;
        private readonly IVariantValueService _variantValueService;
        private readonly ILogger<ProductOptionsController> _logger;

        public ProductOptionsController(ILogger<ProductOptionsController> logger, IProductOptionService productOptionServices, IVariantValueService variantValueService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = productOptionServices ?? throw new ArgumentNullException(nameof(productOptionServices));
            _variantValueService = variantValueService ?? throw new ArgumentNullException(nameof(variantValueService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(ProductOptionViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var entityDto = new ProductOptionDto(Guid.NewGuid());
            _mapper.Map<ProductOptionViewModel, ProductOptionDto>(viewModel, entityDto);
            //entity.CreatedBy = userId;
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            var result = _services.Create(entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<ProductOptionDto> queryOptions)
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
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, ProductOptionViewModel viewModel)
        {
            var entityDto = new ProductOptionDto(key);
            _mapper.Map<ProductOptionViewModel, ProductOptionDto>(viewModel, entityDto);
            var result = _services.Update(key, entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpDelete("key")]
        [EnableQuery]
        public async Task<ResultMessageBase> DeleteAsync([FromODataUri] Guid key)
        {
            var variantValueIds = _variantValueService.GetAll().Where(vv => vv.ProductOptionId == key).Select(vv => vv.Id).ToList();
            await _variantValueService.DeleteAsync(variantValueIds);
            var result = await _services.DeleteAsync(key);
            return SingleResultMessage.Success(result);
        }
    }
}
