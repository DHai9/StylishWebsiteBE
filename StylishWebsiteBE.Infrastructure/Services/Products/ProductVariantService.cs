using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;
using StylishWebsiteBE.Infrastructure.IServices.Products;

namespace StylishWebsiteBE.Infrastructure.Services.Products {
    public class ProductVariantService : ServiceBase<ProductVariantReadModel, ProductVariantDto>, IProductVariantService {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;
        public ProductVariantService(IProductVariantRepository productVariantRepository, IMapper mapper) : base(productVariantRepository, mapper)
        {
            _productVariantRepository = productVariantRepository ?? throw new ArgumentNullException(nameof(productVariantRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> UpdateQuantity(int type, ICollection<ProductVariantDto> entiteDto)
        {
            var ids = entiteDto.Select(entity => entity.Id);
            var entities = _productVariantRepository.GetAll().Where(entity => ids.Contains(entity.Id));
            foreach (var entity in entities)
            {
                if (type == 0)
                {
                    entity.Quantity -= entiteDto.FirstOrDefault(item => item.Id == entity.Id).Quantity;
                }
                else
                {
                    entity.Quantity += entiteDto.FirstOrDefault(item => item.Id == entity.Id).Quantity;
                }
            }
            return await _productVariantRepository.Update(entities);
        }
    }
}
