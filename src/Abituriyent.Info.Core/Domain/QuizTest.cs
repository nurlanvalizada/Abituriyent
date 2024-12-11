namespace Abituriyent.Info.Core.Domain
{
    public class QuizTest : Entity
    {
        public int CourseLessonId { get; set; }
        public CourseLesson CourseLesson { get; set; }

        public int CourseLessonTestId { get; set; }
        public CourseLessonTest CourseLessonTest { get; set; }
    }
}