namespace Abituriyent.Info.Core.Domain
{
    public class StudentLessonTestAnswer : Entity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}