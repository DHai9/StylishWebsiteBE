using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using PupuCore.Extensions;
using StylishWebsiteBE.Domain.DTOs.Cards;
using StylishWebsiteBE.Domain.ReadModels.Cards;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Carts;

namespace StylishWebsiteBE.Infrastructure.Services.Carts {
    public class CartService : ServiceBase<CartReadModel, CartDto>, ICartService {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository, IMapper mapper) : base(cartRepository, mapper)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _cartDetailRepository = cartDetailRepository ?? throw new ArgumentNullException(nameof(cartDetailRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void EmptyCartItem(Guid id)
        {
            var details = _cartDetailRepository.GetAll().Where(entity => entity.CartId == id);
            if (!details.IsNullOrDefault())
            {
                _cartDetailRepository.Delete(details);
                _cartDetailRepository.SaveChange();
            }
        }
    }
}
