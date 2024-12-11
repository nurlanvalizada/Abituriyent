using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class SubjectMapping
    {
        public SubjectMapping(EntityTypeBuilder<Subject> entityBuilder)
        {
            entityBuilder.ToTable("Subjects");

            entityBuilder.HasKey(s => s.Id);

            entityBuilder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        }
    }
}