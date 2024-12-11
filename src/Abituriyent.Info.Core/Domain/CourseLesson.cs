using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class CourseLesson : Entity
    {
        public bool Status { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public ICollection<QuizTest> QuizLessons { get; set; }
        public ICollection<CourseLessonTest> CourseLessonTests { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set; }
    }
}