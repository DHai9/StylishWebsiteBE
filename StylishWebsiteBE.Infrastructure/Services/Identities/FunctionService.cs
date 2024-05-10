using AutoMapper;
using PupuCore.EntityServices.Library.Implements;
using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Identities;

namespace StylishWebsiteBE.Infrastructure.Services.Identities {
    public class FunctionService : IFunctionService {
        private readonly IFunctionRepository _functionRepository;
        private readonly IMapper _mapper;
    }
}
