using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Domain.ReadModels.Options;
using StylishWebsiteBE.Infrastructure.IRepositories.Options;
using StylishWebsiteBE.Infrastructure.IServices.Options;

namespace StylishWebsiteBE.Infrastructure.Services.Options {
    public class OptionService : ServiceBase<OptionReadModel, OptionDto>, IOptionService {
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;
        public OptionService(IOptionRepository optionRepository, IMapper mapper) : base(optionRepository, mapper)
        {
            _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
