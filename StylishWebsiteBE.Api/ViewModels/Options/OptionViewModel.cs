using StylishWebsiteBE.Domain.Enums;
using System;
using System.Collections.Generic;

namespace StylishWebsiteBE.Api.ViewModels.Options {
    public class OptionViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OptionType OptionType { get; set; }
        public List<OptionValueViewModel> OptionValues { get; set;}
    }
}
