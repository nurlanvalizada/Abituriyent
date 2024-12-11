using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class CourseLessonTestMapping
    {
        public CourseLessonTestMapping(EntityTypeBuilder<CourseLessonTest> entityBuilder)
        {
            entityBuilder.ToTable("CourseLessonTests");

            entityBuilder.HasKey(clt => clt.Id);

            entityBuilder.HasIndex(clt => new { clt.CourseLessonId, clt.TestId }).IsUnique();

            // configuring many to many between entities
            entityBuilder
                .HasOne(clt => clt.CourseLesson)
                .WithMany(cl => cl.CourseLessonTests)
                .HasForeignKey(clt => clt.CourseLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
               .HasOne(clt => clt.Test)
               .WithMany(t => t.CourseLessonTests)
               .HasForeignKey(clt => clt.TestId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}