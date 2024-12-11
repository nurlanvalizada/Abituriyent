using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class PersonMapping
    {
        public PersonMapping(EntityTypeBuilder<Person> entityBuilder)
        {
            entityBuilder.ToTable("Persons");

            entityBuilder.HasKey(p => p.Id);

            entityBuilder.Property(p => p.FirstName).HasMaxLength(30).IsRequired();
            entityBuilder.Property(p => p.LastName).HasMaxLength(30).IsRequired();

            //one to one relationships between entities
            entityBuilder
                .HasOne(p => p.ApplicationUser)
                .WithOne(a => a.Person)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
               .HasOne(p => p.Student)
               .WithOne(s => s.Person)
               .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
               .HasOne(p => p.Admin)
               .WithOne(a => a.Person)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}