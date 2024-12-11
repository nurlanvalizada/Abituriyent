using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class StudentLessonMapping
    {
        public StudentLessonMapping(EntityTypeBuilder<StudentLesson> entityBuilder)
        {
            entityBuilder.ToTable("StudentLessons");

            entityBuilder.HasKey(sl => sl.Id);

            entityBuilder.HasIndex(sl => new { sl.StudentId, sl.CourseLessonId });

            entityBuilder.Property(sl => sl.StartDate).ForSqlServerHasColumnType("datetime2(1)").IsRequired();
            entityBuilder.Property(sl => sl.EndDate).ForSqlServerHasColumnType("datetime2(1)");

            //configuring many to many
            entityBuilder
                .HasOne(sl => sl.Student)
                .WithMany(s => s.StudentLessons)
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(sl => sl.CourseLesson)
                .WithMany(l => l.StudentLessons)
                .HasForeignKey(sl => sl.CourseLessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}