using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class QuizTestMapping
    {
        public QuizTestMapping(EntityTypeBuilder<QuizTest> entityBuilder)
        {
            entityBuilder.ToTable("QuizTests");

            entityBuilder.HasKey(qt => qt.Id);

            entityBuilder.HasIndex(qt => new { qt.CourseLessonId, qt.CourseLessonTestId }).IsUnique();

            //configuring one to many between entities
            entityBuilder
                .HasOne(qt => qt.CourseLesson)
                .WithMany(l => l.QuizLessons)
                .HasForeignKey(qt => qt.CourseLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            //configuring one to one between entities
            entityBuilder
             .HasOne(qt => qt.CourseLessonTest)
             .WithMany(t => t.QuizTests)
             .HasForeignKey(qt => qt.CourseLessonTestId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}