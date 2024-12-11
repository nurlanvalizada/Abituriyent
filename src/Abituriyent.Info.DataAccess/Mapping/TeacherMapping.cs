using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class TeacherMapping
    {
        public TeacherMapping(EntityTypeBuilder<Teacher> entityBuilder)
        {
            entityBuilder.ToTable("Teachers");

            entityBuilder.HasKey(t => t.Id);

            entityBuilder.Property(t => t.Profession).HasMaxLength(50).IsRequired();
            entityBuilder.Property(t => t.About).HasMaxLength(2000).IsRequired();
            entityBuilder.Property(t => t.FacebookProfile).HasMaxLength(100);
            entityBuilder.Property(t => t.TwitterProfile).HasMaxLength(100);
            entityBuilder.Property(t => t.GooglePlusProfile).HasMaxLength(100);
            entityBuilder.Property(t => t.FullName).HasMaxLength(40).IsRequired();
            entityBuilder.Property(t => t.ImageUrl).HasMaxLength(100);
        }
    }
}