using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using PupuCore.AspNetCore.Http;
using StylishWebsiteBE.Infrastructure.IServices.Options;
using StylishWebsiteBE.Api.ViewModels.Options;
using StylishWebsiteBE.Domain.DTOs.Options;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OptionValuesController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IOptionValueService _services;
        private readonly ILogger<OptionValuesController> _logger;

        public OptionValuesController(ILogger<OptionValuesController> logger, IOptionValueService optionValueServices, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = optionValueServices ?? throw new ArgumentNullException(nameof(optionValueServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(OptionValueViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var entityDto = new OptionValueDto(Guid.NewGuid());
            _mapper.Map<OptionValueViewModel, OptionValueDto>(viewModel, entityDto);
            //entity.CreatedBy = userId;
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            var result = _services.Create(entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<OptionValueDto> queryOptions)
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
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, OptionValueViewModel viewModel)
        {
            var entityDto = new OptionValueDto(key);
            _mapper.Map<OptionValueViewModel, OptionValueDto>(viewModel, entityDto);
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
