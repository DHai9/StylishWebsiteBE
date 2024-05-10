﻿using PupuCore.EntityServices.Library.Interfaces;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Domain.ReadModels.Options;

namespace StylishWebsiteBE.Infrastructure.IServices.Options {
    public interface IOptionValueService : IServiceBase<OptionValueReadModel, OptionValueDto> {
    }
}
