using System;

namespace Abituriyent.Info.Core.Domain
{
    public class StudentLesson : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseLessonId { get; set; }
        public CourseLesson CourseLesson { get; set; }
    }
}