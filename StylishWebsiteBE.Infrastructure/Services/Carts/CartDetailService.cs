using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Cards;
using StylishWebsiteBE.Domain.ReadModels.Cards;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Carts;

namespace StylishWebsiteBE.Infrastructure.Services.Carts {
    public class CartDetailService: ServiceBase<CartDetailReadModel,CartDetailDto>, ICartDetailService {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IMapper _mapper;
        public CartDetailService(ICartDetailRepository cartDetailRepository, IMapper mapper) : base(cartDetailRepository, mapper)
        {
            _cartDetailRepository = cartDetailRepository ?? throw new ArgumentNullException(nameof(cartDetailRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
