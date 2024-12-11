using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class LessonMapping
    {
        public LessonMapping(EntityTypeBuilder<Lesson> entityBuilder)
        {
            entityBuilder.ToTable("Lessons");

            entityBuilder.HasKey(l => l.Id);

            entityBuilder.Property(l => l.Name).HasMaxLength(100).IsRequired();
            entityBuilder.Property(l => l.VideoUrl).HasMaxLength(100);
            entityBuilder.Property(l => l.PdfContentType).HasMaxLength(20);

            //configuring one to many between entities
            entityBuilder
               .HasOne(l => l.Subject)
               .WithMany(s => s.Lessons)
               .HasForeignKey(l => l.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}