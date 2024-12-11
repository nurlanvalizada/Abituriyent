using System.ComponentModel.DataAnnotations;

namespace Abituriyent.Info.Web.Models.AdminViewModels
{
    public class AdminViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Gender { get; set; }
    }
}