using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;
using StylishWebsiteBE.Infrastructure.IServices.Products;

namespace StylishWebsiteBE.Infrastructure.Services.Products {
    public class ProductOptionService : ServiceBase<ProductOptionReadModel, ProductOptionDto>, IProductOptionService {
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IMapper _mapper;
        public ProductOptionService(IProductOptionRepository productOptionRepository, IMapper mapper) : base(productOptionRepository, mapper)
        {
            _productOptionRepository = productOptionRepository ?? throw new ArgumentNullException(nameof(productOptionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
