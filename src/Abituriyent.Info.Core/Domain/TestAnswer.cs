namespace Abituriyent.Info.Core.Domain
{
    public class TestAnswer : Entity
    {
        public int TestId { get; set; }
        public Test Test { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}