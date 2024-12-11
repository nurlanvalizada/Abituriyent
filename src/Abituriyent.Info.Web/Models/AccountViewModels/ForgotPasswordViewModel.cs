using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email ünvanınızı daxil edin")]
        [EmailAddress(ErrorMessage = "Email ünvanınızı düzgün daxil edin")]
        public string Email { get; set; }
    }
}
