using System.Collections.Generic;
using Abituriyent.Info.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abituriyent.Info.Web.Models.ViewModels
{
    public class RatingViewModel
    {
        public int ExamId { get; set; }
        public Student Student { get; set; }
        public IList<StudentExam> StudentExams { get; set; }
        public SelectList Exams { get; set; }

        public int CompletedLessonCount { get; set; }
        public int CompletedExamCount { get; set; }
    }
}
