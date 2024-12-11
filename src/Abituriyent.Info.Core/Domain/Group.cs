using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Group : Entity
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<GroupExam> GroupExams { get; set; }
    }
}