using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;

namespace Abituriyent.Info.Web.Models.AdminViewModels
{
    public class LessonViewModel
    {

        [Display(Name = "CourseLesson")]
        public CourseLesson CourseLesson { get; set; }

        [Display(Name = "CourseLessonTests")]
        public List<CourseLessonTest> CourseLessonTests { get; set; }

        [Display(Name = "Lesson")]
        public Lesson Lesson { get; set; }

        [Display(Name = "Groups")]
        public List<Group> Groups { get; set; }

        [Display(Name = "GroupIds")]
        public List<int> GroupIds { get; set; }
    }
}
