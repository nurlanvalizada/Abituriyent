using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.Models.AdminViewModels
{
    public class ExamTestViewModel
    {

        [Display(Name = "ExamTest")]
        public ExamTest ExamTest { get; set; }

        [Display(Name = "CourseLesson")]
        public CourseLesson CourseLesson { get; set; }

        [Display(Name = "CourseLessonTests")]
        public List<CourseLessonTest> CourseLessonTests { get; set; }

        [Display(Name = "Lessons")]
        public SelectList Lessons { get; set; }

        [Display(Name = "TestIds")]
        public List<int> TestIds { get; set; }

    }
}
