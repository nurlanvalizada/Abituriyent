using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class ExamTest : Entity
    {
        public int GroupExamId { get; set; }
        public GroupExam GroupExam { get; set; }

        public int CourseLessonTestId { get; set; }
        public CourseLessonTest CourseLessonTest { get; set; }

        public ICollection<StudentExamTestAnswer> StudentExamTestAnswers { get; set; }
        public ICollection<StudentExamOpenTestAnswer> StudentExamOpenTestAnswers { get; set; }
    }
}