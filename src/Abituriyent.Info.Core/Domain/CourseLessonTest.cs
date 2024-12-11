using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class CourseLessonTest : Entity
    {
        public bool Status { get; set; }

        public int CourseLessonId { get; set; }
        public CourseLesson CourseLesson { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<QuizTest> QuizTests { get; set; }
        public ICollection<ExamTest> ExamTests { get; set; }
    }
}