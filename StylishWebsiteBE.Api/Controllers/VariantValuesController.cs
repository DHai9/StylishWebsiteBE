using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class VariantValuesController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IVariantValueService _services;
        private readonly ILogger<VariantValuesController> _logger;

        public VariantValuesController(ILogger<VariantValuesController> logger, IVariantValueService variantValueServices, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = variantValueServices ?? throw new ArgumentNullException(nameof(variantValueServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(VariantValueViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var entityDto = new VariantValueDto(Guid.NewGuid());
            _mapper.Map<VariantValueViewModel, VariantValueDto>(viewModel, entityDto);
            //entity.CreatedBy = userId;
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            var result = _services.Create(entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<VariantValueDto> queryOptions)
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
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, VariantValueViewModel viewModel)
        {
            var entityDto = new VariantValueDto(key);
            _mapper.Map<VariantValueViewModel, VariantValueDto>(viewModel, entityDto);
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
