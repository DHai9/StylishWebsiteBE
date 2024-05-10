using System;

namespace StylishWebsiteBE.Api.ViewModels.Options {
    public class OptionValueViewModel {
        public OptionValueViewModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid OptionId { get; set; }
        public string Value { get; set; }
    }
}
