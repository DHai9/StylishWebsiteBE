using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;
using StylishWebsiteBE.Infrastructure.IServices.Products;

namespace StylishWebsiteBE.Infrastructure.Services.Products {
    public class VariantValueService : ServiceBase<VariantValueReadModel, VariantValueDto>, IVariantValueService {
        private readonly IVariantValueRepository _variantValueRepository;
        private readonly IMapper _mapper;
        public VariantValueService(IVariantValueRepository variantValueRepository, IMapper mapper) : base(variantValueRepository, mapper)
        {
            _variantValueRepository = variantValueRepository ?? throw new ArgumentNullException(nameof(variantValueRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
