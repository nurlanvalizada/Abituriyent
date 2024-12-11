namespace Abituriyent.Info.Core.Domain
{
    public class StudentExamTestAnswer : Entity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ExamTestId { get; set; }
        public ExamTest ExamTest { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}