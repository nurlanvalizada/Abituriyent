using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.AccountViewModels
{
    public class RegisterViewModel : AccountEditViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Şifrənin təsdiqi")]
        [Compare("Password", ErrorMessage = "Şifrə ilə təsdiqləmə şifrəsi uyğun gəlmir")]
        public string ConfirmPassword { get; set; }
    }
}
