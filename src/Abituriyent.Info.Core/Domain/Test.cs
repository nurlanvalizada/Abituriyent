using System.Collections.Generic;
using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Core.Domain
{
    public class Test : Entity
    {
        public string Content { get; set; }
        public bool Status { get; set; }
        public TestType TestType { get; set; }

        public TestAnswer TestAnswer { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<CourseLessonTest> CourseLessonTests { get; set; }
    }
}