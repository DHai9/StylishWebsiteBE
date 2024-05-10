using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PupuCore.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using StylishWebsiteBE.Api.ViewModels.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using StylishWebsiteBE.Domain.DTOs.Products;
using System.Linq;
using System.Collections.Generic;
using PupuCore.Infrastructure.Exceptions;
using System.Text;
using PupuCore.Extensions;
using StylishWebsiteBE.Infrastructure.IServices.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StylishWebsiteBE.Domain.EntityDtos.Products;

namespace StylishWebsiteBE.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IProductService _services;
        private readonly IProductOptionService _productOptionServices;
        private readonly IProductVariantService _productVariantService;
        private readonly IVariantValueService _variantValueService;
        private readonly IOptionService _optionService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IProductService productServices,
            IProductOptionService productOptionServices, IProductVariantService productVariantService,
            IVariantValueService variantValueService, IOptionService optionService,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = productServices ?? throw new ArgumentNullException(nameof(productServices));
            _productOptionServices = productOptionServices ?? throw new ArgumentNullException(nameof(productOptionServices));
            _productVariantService = productVariantService ?? throw new ArgumentNullException(nameof(productVariantService));
            _variantValueService = variantValueService ?? throw new ArgumentNullException(nameof(variantValueService));
            _optionService = optionService ?? throw new ArgumentNullException(nameof(optionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ResultMessageBase> PostAsync([FromBody] ProductViewModel viewModel)
        {
            //if (!Guid.TryParse(User.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier))?.Value, out var userId))
            //    throw new ArgumentNullException("User id");
            if (viewModel.Name.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(viewModel.Name));
            if (await _services.CheckNameExitAsync(viewModel.Name))
            {
                throw new BusinessException("Create Error", "304", 304, "Product name already exits");
            }
            var entityDto = new ProductDto(Guid.NewGuid());
            // Init ProductVariants Id
            viewModel.ProductVariants.ForEach(entity => { entity.Id = Guid.NewGuid(); entity.SkuId = GeneratorSku(); entity.ProductId = entityDto.Id; });
            // Create product
            entityDto.CreatedTime = DateTimeOffset.UtcNow;
            //entity.CreatedBy = userId;

            _mapper.Map(viewModel, entityDto);
            entityDto.ProductOptions = viewModel.ProductOptions.Select(x => new ProductOptionDto(Guid.NewGuid())
            {
                ProductId = entityDto.Id,
                OptionId = x.OptionId
            }).ToList();

            foreach (var entity in entityDto.ProductVariants)
            {
                foreach (var vv in entity.VariantValues)
                {
                    var productOption = entityDto.ProductOptions.FirstOrDefault(x => x.OptionId == vv.OptionId);
                    vv.ProductId = entityDto.Id;
                    vv.ProductVariantId = entity.Id;
                    vv.ProductOptionId = productOption.Id;
                }
            }
            var result = _services.Create(entityDto);
            var productOptions = _productOptionServices.GetAll().Where(e => e.ProductId == result.Id);
            var productVariants = _productVariantService.GetAll().Where(e => e.ProductId == result.Id);
            result.ProductVariants.Clear();
            _mapper.Map(productVariants.ToList(), result.ProductVariants);
            _mapper.Map(productOptions.ToList(), result.ProductOptions);
            return SingleResultMessage.Success(result);
        }

        [HttpGet]
        public async Task<ResultMessageBase> GetAsync(ODataQueryOptions<ProductDto> queryOptions)
        {
            var results = _services.GetAll();
            var resQuery = new List<ProductDto>();
            foreach (var result in results)
            {
                if (result.ProductVariants.Count > 0)
                {
                    result.MinPrice = result.ProductVariants.Min(pv => pv.Price);
                    result.MaxPrice = result.ProductVariants.Max(pv => pv.Price);
                }
                result.GetFullInfoVariantValue();
                resQuery.Add(result);
            }
            resQuery = resQuery.OrderByDescending(entity => entity.CreatedTime).ToList();
            var finalResult = queryOptions.ApplyTo(resQuery.AsQueryable());
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
        public async Task<ResultMessageBase> PutAsync([FromODataUri] Guid key, [FromBody] ProductViewModel viewModel)
        {
            var entityDto = new ProductDto(key);
            _mapper.Map(viewModel, entityDto);

            var newPOs = new List<ProductOptionDto>();
            #region update product options
            foreach (var option in viewModel.ProductOptions)
            {
                if (option.Id.Equals(Guid.Empty))
                {
                    option.Id = Guid.NewGuid();
                    var newOption = _mapper.Map<ProductOptionViewModel, ProductOptionDto>(option);
                    newOption.ProductId = key;
                    var newRes = await _productOptionServices.CreateAsync(newOption);
                    newPOs.Add(newRes);
                }
                else
                {
                    var newOption = _mapper.Map<ProductOptionViewModel, ProductOptionDto>(option);
                    var newRes = await _productOptionServices.UpdateAsync(newOption);
                    newPOs.Add(newRes);
                }
            }
            #endregion

            #region update product variants
            foreach (var productVariant in viewModel.ProductVariants)
            {
                var newVVs = productVariant.VariantValues;
                if (productVariant.Id.IsNullOrDefault())
                {
                    productVariant.Id = Guid.NewGuid();
                    var newProductVariant = _mapper.Map<ProductVariantViewModel, ProductVariantDto>(productVariant);
                    newProductVariant.ProductId = key;
                    newProductVariant.SkuId = GeneratorSku();
                    newProductVariant.VariantValues.Clear();
                    await _productVariantService.CreateAsync(newProductVariant);
                }
                else
                {

                    var newProductVariant = _mapper.Map<ProductVariantViewModel, ProductVariantDto>(productVariant);
                    newProductVariant.VariantValues.Clear();
                    await _productVariantService.UpdateAsync(newProductVariant);
                }
                newVVs.ForEach(vv => vv.ProductVariantId = productVariant.Id);

                #region update variant values
                foreach (var variantValue in newVVs)
                {
                    if (variantValue.ProductOptionId.IsNullOrDefault())
                    {
                        variantValue.ProductOptionId = newPOs.FirstOrDefault(po => po.OptionId == variantValue.OptionId).Id;
                        var newVariantValue = await _variantValueService.CreateAsync(_mapper.Map(variantValue, new VariantValueDto(Guid.NewGuid())));
                    }
                    else
                    {
                        var newVariantValue = await _variantValueService.UpdateAsync(_mapper.Map<VariantValueViewModel, VariantValueDto>(variantValue));
                    }
                }
                #endregion
            }
            #endregion
            entityDto.ProductOptions.Clear();
            entityDto.ProductVariants.Clear();
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


        // System method
        private string GeneratorSku()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            // Bảng chữ cái tiếng Anh viết hoa
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // Lấy ngẫu nhiên 3 ký tự từ bảng chữ cái và thêm vào chuỗi kết quả
            for (int i = 0; i < 3; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }
            var date_now = DateTimeOffset.UtcNow;
            var res = date_now.Day + date_now.Month + builder.ToString() + date_now.Year;
            return GenerateRandomChars() + "-" + res;
        }

        static string GenerateRandomChars()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            // Bảng chữ cái tiếng Anh viết hoa
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // Lấy ngẫu nhiên 3 ký tự từ bảng chữ cái và thêm vào chuỗi kết quả
            for (int i = 0; i < 3; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }
            return builder.ToString();
        }
    }
}