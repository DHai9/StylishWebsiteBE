using System.ComponentModel.DataAnnotations;

namespace StylishWebsiteBE.Api.ViewModels.Identities {
    public class LoginViewModel {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "PassWord is required")]
        public string Password { get; set; }
    }
}
