using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Cari şifrəni daxil edin")]
        [DataType(DataType.Password)]
        [Display(Name = "Cari Şifrə")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifrəni daxil edin")]
        [StringLength(100, ErrorMessage = "{0} ən azı {2} və ən çoxu {1} simvoldan ibarət olmalıdır", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni şifrə")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Yeni şifrəni təsdiqləyin")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifrə və təsdiqləmə şifrəsi uyğun gəmir.")]
        public string ConfirmPassword { get; set; }
    }
}
