using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class CourseLessonMapping
    {
        public CourseLessonMapping(EntityTypeBuilder<CourseLesson> entityBuilder)
        {
            entityBuilder.ToTable("CourseLessons");

            entityBuilder.HasKey(cl => cl.Id);

            entityBuilder.HasIndex(cl => new { cl.CourseId, cl.LessonId}).IsUnique();

            //configuring many to many between entities
            entityBuilder
                .HasOne(et => et.Course)
                .WithMany(e => e.CourseLessons)
                .HasForeignKey(et => et.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
               .HasOne(et => et.Lesson)
               .WithMany(c => c.CourseLessons)
               .HasForeignKey(et => et.LessonId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}