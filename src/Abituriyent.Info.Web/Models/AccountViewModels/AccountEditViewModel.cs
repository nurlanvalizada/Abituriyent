using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.Models.AccountViewModels
{
    public class AccountEditViewModel
    {
        [Required(ErrorMessage = "{0}-nı daxil etmək tələb olunur")]
        [StringLength(40, ErrorMessage = "{0} ən azı {2} ən çoxu {1} simvoldan ibarət olmalıdır.", MinimumLength = 3)]
        [Display(Name = "İstifadəçi Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0}-nı daxil etmək tələb olunur")]
        [EmailAddress(ErrorMessage = "{0}-nı düzgün daxil edin")]
        [Display(Name = "Email Ünvanı")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}-ni daxil etmək tələb olunur")]
        [StringLength(100, ErrorMessage = "{0} ən azı {2} simvoldan ibarət olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}-ı daxil etmək tələb olunur")]
        [RegularExpression(@"^[a-zA-Z]{3,20}$", ErrorMessage = "{0} ən az 3, ən çox 20 hərfdən ibarət ola bilər")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0}-ı daxil etmək tələb olunur")]
        [RegularExpression(@"^[a-zA-Z]{3,20}$", ErrorMessage = "{0} ən az 3, ən çox 20 hərfdən ibarət ola bilər")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0}-nı daxil etmək tələb olunur")]
        [StringLength(40, ErrorMessage = "{0} ən azı {2} ən çoxu simvoldan ibarət olmalıdır.", MinimumLength = 3)]
        [Display(Name = "Ata Adı")]
        public string FatherName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Ev Telefonu")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{2}-\d{2}$", ErrorMessage = "{0} (012) 123-45-67 formatda daxil edilməlidir")]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "{0}-u daxil etmək tələb olunur")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobil Telefon")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{2}-\d{2}$", ErrorMessage = "{0} (050) 123-45-67 bu formatda daxil edilməlidir")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "{0}-ı daxil etmək tələb olunur")]
        [StringLength(40, ErrorMessage = "{0} ən azı {2} ən çoxu simvoldan ibarət olmalıdır.", MinimumLength = 4)]
        [Display(Name = "Ünvan")]
        public string Address { get; set; }

        [Range(1, 2, ErrorMessage = "{0}-si düzgün seçməmisiniz")]
        [Required(ErrorMessage = "{0}-i daxil etmək tələb olunur")]
        [Display(Name = "Cins")]
        public byte Gender { get; set; }

        [Range(1, 31, ErrorMessage = "Günü düzgün seçməmisiniz")]
        public int Day { get; set; }

        [Range(1, 12, ErrorMessage = "Ayı düzgün seçməmisiniz")]
        public int Month { get; set; }

        [Range(1930, 2020, ErrorMessage = "İli düzgün seçməmisiniz")]
        public int Year { get; set; }

        [Display(Name = "Qruplar")]
        public SelectList Groups { get; set; }

        [Display(Name = "Şəhərlər")]
        public SelectList Cities { get; set; }

        [Display(Name = "Cinslər")]
        public SelectList Genders { get; set; }

        [Display(Name = "Məktəblər")]
        public SelectList Schools { get; set; }

        [Range(1, 10, ErrorMessage = "Qrupu düzgün seçməmisiniz")]
        public int GroupId { get; set; }

        [Range(1, 40000, ErrorMessage = "Məktəbi düzgün seçməmisiniz")]
        public int SchoolId { get; set; }

        [Range(1, 1000, ErrorMessage = "Şəhəri düzgün seçməmisiniz")]
        public int CityId { get; set; }
    }
}