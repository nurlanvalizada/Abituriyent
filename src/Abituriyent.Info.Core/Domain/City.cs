using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class City : Entity
    {
        public string Name { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}