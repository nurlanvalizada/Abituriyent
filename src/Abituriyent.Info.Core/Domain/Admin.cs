using System.Collections.Generic;

namespace Abituriyent.Info.Core.Domain
{
    public class Admin : Entity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public ICollection<News> Newses { get; set; }
    }
}