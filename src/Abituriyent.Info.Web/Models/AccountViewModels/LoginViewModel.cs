using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "İstifadəçi adını daxil edin")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifrəni daxil edin")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Məni Xatırla?")]
        public bool RememberMe { get; set; }
    }
}
