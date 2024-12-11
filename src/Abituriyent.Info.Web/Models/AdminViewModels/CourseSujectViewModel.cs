using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.Models.AdminViewModels
{
    public class CourseSubjectViewModel
    {
        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Display(Name = "Subject")]
        public Subject Subject { get; set; }

        [Display(Name = "CourseLesson")]
        public CourseLesson CourseLesson { get; set; }

        [Display(Name = "CourseLessonTests")]
        public List<CourseLessonTest> CourseLessonTests { get; set; }

        [Display(Name = "Subjects")]
        public SelectList Subjects { get; set; }

        [Display(Name = "Groups")]
        public List<Group> Groups { get; set; }

        [Display(Name = "Lessons")]
        public SelectList Lessons { get; set; }

        [Display(Name = "TestIds")]
        public List<int> TestIds { get; set; }

        [Display(Name = "GroupIds")]
        public List<int> GroupIds { get; set; }
    }
}
