using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;

namespace Abituriyent.Info.Web.Models.AdminViewModels
{
    public class TestViewModel
    {
        [Display(Name = "Test")]
        public CourseLessonTest Test { get; set; }

        [Display(Name = "Answers")]
        public List<Answer> Answers { get; set; }

        [Display(Name = "Groups")]
        public List<Group> Groups { get; set; }

        [Display(Name = "GroupIds")]
        public List<int> GroupIds { get; set; }

        [Display(Name = "CorrectAnswerId")]
        public int CorrectAnswerId { get; set; }
    }
}
