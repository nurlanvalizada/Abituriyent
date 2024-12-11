using System;

namespace Abituriyent.Info.Core.Domain
{
    public class StudentExam : Entity
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int ExamResult { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int GroupExamId { get; set; }
        public GroupExam GroupExam { get; set; }
    }
}