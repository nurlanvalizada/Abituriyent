using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class GroupExam : Entity
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public ICollection<ExamTest> ExamTests { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }
}