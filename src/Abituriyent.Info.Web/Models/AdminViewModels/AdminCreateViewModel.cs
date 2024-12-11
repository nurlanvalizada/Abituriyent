using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.AdminViewModels
{
    public class AdminCreateViewModel : AdminViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }
    }
}