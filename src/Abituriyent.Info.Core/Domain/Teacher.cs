using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Teacher : Entity
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Profession { get; set; }
        public string About { get; set; }
        public string FacebookProfile { get; set; }
        public string GooglePlusProfile { get; set; }
        public string TwitterProfile { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}