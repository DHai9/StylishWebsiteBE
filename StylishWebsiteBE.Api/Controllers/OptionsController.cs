using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using StylishWebsiteBE.Infrastructure.IServices.Options;
using PupuCore.AspNetCore.Http;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Api.ViewModels.Options;
using System.Linq;
using PupuCore.Extensions;

namespace StylishWebsiteBE.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IOptionService _services;
        private readonly IOptionValueService _optionValueService;
        private readonly ILogger<OptionsController> _logger;

        public OptionsController(ILogger<OptionsController> logger, IOptionService optionServices, IOptionValueService optionValueService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = optionServices ?? throw new ArgumentNullException(nameof(optionServices));
            _optionValueService = optionValueService ?? throw new ArgumentNullException(nameof(optionValueService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync(OptionViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            var entityDto = new OptionDto(Guid.NewGuid());
            viewModel.OptionValues.ForEach(ov => { ov.OptionId = entityDto.Id; ov.Id = Guid.NewGuid(); });
            _mapper.Map<OptionViewModel, OptionDto>(viewModel, entityDto);
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            //entity.CreatedBy = userId;
            var result = _services.Create(entityDto);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<OptionDto> queryOptions)
        {
            var results = _services.GetAll();
            var finalResult = queryOptions.ApplyTo(results);
            var odataFeature = HttpContext.ODataFeature();
            return PageResultMessage.Success(finalResult, odataFeature.TotalCount);
        }

        [HttpGet("GetByType/{type}")]
        [EnableQuery]
        public async Task<ResultMessageBase> GetByTypeAsync([FromODataUri] int type)
        {
            var results = _services.GetAll();
            var finalResults = results.Where(el => (int)el.OptionType == type);
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
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, OptionViewModel viewModel)
        {
            #region update option value
            viewModel.OptionValues.ForEach(ov =>
            {
                if (ov.Id.IsNullOrDefault())
                {
                    ov.Id = Guid.NewGuid();
                    ov.OptionId = key;
                    _optionValueService.Create(_mapper.Map<OptionValueViewModel, OptionValueDto>(ov));
                }
            });

            #endregion
            var entityDto = new OptionDto(key);
            _mapper.Map<OptionViewModel, OptionDto>(viewModel, entityDto);
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
