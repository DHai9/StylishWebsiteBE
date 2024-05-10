using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.ReadModels.Products;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;
using StylishWebsiteBE.Infrastructure.IServices.Products;

namespace StylishWebsiteBE.Infrastructure.Services.Products {
    public class ProductService : ServiceBase<ProductReadModel, ProductDto>, IProductService {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper) : base(productRepository, mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<bool> CheckNameExitAsync(string name)
        {
            return _productRepository.CheckNameExitAsync(name);
        }
    }
}
