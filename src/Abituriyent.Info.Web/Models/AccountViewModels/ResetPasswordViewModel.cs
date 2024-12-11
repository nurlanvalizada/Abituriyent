using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email ünvanınız daxil edin")]
        [EmailAddress(ErrorMessage = "Email ünvanınızı düzgün daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrəni daxil edin")]
        [StringLength(100, ErrorMessage = "{0} ən azı {2} və ən çoxu {1} simvoldan ibarət olmalıdır", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifrəni təsdiqlə")]
        [Compare("Password", ErrorMessage = "Şifrə və təsdiqləmə şifrəsi uyğun gəmir")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
