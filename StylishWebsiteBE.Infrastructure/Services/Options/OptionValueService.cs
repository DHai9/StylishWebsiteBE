using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Domain.ReadModels.Options;
using StylishWebsiteBE.Infrastructure.IRepositories.Options;
using StylishWebsiteBE.Infrastructure.IServices.Options;

namespace StylishWebsiteBE.Infrastructure.Services.Options {
    public class OptionValueService : ServiceBase<OptionValueReadModel, OptionValueDto>, IOptionValueService {
        private readonly IOptionValueRepository _optionValueRepository;
        private readonly IMapper _mapper;
        public OptionValueService(IOptionValueRepository optionValueRepository, IMapper mapper) : base(optionValueRepository, mapper)
        {
            _optionValueRepository = optionValueRepository ?? throw new ArgumentNullException(nameof(optionValueRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
