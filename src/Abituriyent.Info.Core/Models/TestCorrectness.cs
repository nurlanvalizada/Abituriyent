namespace Abituriyent.Info.Core.Models
{
    public class TestCorrectness
    {
        public int TestId { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public TestType TestType { get; set; }
    }
}