using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class School : Entity
    {
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}