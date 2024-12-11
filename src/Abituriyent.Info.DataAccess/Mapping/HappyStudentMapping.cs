using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class HappyStudentMapping
    {
        public HappyStudentMapping(EntityTypeBuilder<HappyStudent> entityBuilder)
        {
            entityBuilder.ToTable("HappyStudents");

            entityBuilder.HasKey(hs => hs.Id);

            entityBuilder.Property(hs => hs.FullName).HasMaxLength(60);
            entityBuilder.Property(hs => hs.ImageUrl).HasMaxLength(100);
            entityBuilder.Property(hs => hs.Review).HasMaxLength(2000);
        }
    }
}