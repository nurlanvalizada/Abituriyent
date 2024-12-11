using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class ExamTestMapping
    {
        public ExamTestMapping(EntityTypeBuilder<ExamTest> entityBuilder)
        {
            entityBuilder.ToTable("ExamTests");

            entityBuilder.HasKey(et => et.Id );

            entityBuilder.HasIndex(et => new { et.GroupExamId, et.CourseLessonTestId }).IsUnique();

            //configuring many to many
            entityBuilder
                .HasOne(et => et.GroupExam)
                .WithMany(e => e.ExamTests)
                .HasForeignKey(et => et.GroupExamId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(et => et.CourseLessonTest)
                .WithMany(t => t.ExamTests)
                .HasForeignKey(et => et.CourseLessonTestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}