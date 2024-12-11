using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class SchoolMapping
    {
        public SchoolMapping(EntityTypeBuilder<School> entityBuilder)
        {
            entityBuilder.ToTable("Schools");

            entityBuilder.HasKey(s => s.Id);

            entityBuilder.Property(s => s.Name).HasMaxLength(200).IsRequired();

            //configuring one to many between entities
            entityBuilder
                .HasOne(s => s.City)
                .WithMany(c => c.Schools)
                .HasForeignKey(s => s.CityId);
        }
    }
}