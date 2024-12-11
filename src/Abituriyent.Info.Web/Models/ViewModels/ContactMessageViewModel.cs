using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class ContactMessageViewModel
    {
        [Required(ErrorMessage = "Ad və soyadı daxil etmək tələb olunur")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email ünvanı daxil etmək tələb olunur")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email ünvanı düzgün daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mövzunu daxil etmək tələb olunur")]
        [StringLength(50, ErrorMessage = "{0} ən azı {2} və ən çoxu {1} simvoldan ibarət olmalıdır.", MinimumLength = 5)]
        [Display(Name="Mövzu")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Masajı daxil etmək tələb olunur")]
        [StringLength(500, ErrorMessage = "{0} ən azı {2} və ən çoxu {1} simvoldan ibarət olmalıdır.", MinimumLength = 10)]
        public string Message { get; set; }
    }
}