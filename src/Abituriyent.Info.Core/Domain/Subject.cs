using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public bool Status { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
