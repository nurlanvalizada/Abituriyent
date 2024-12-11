using Abituriyent.Info.Core.Models;

namespace Abituriyent.Info.Core.Domain
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Student Student { get; set; }
        public Admin Admin { get; set; }
    }
}