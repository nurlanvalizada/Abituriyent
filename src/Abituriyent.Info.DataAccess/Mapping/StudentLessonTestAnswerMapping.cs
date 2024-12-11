using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class StudentLessonTestAnswerMapping
    {
        public StudentLessonTestAnswerMapping(EntityTypeBuilder<StudentLessonTestAnswer> entityBuilder)
        {
            entityBuilder.ToTable("StudentLessonTestAnswers");

            entityBuilder.HasKey(slta => slta.Id);

            entityBuilder.HasIndex(slta => new { slta.StudentId, slta.AnswerId });

            //configuring many to many
            entityBuilder
                .HasOne(slta => slta.Answer)
                .WithMany(a => a.StudentLessonTestAnswers)
                .HasForeignKey(slta => slta.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(slta => slta.Student)
                .WithMany(s => s.StudentLessonTestAnswers)
                .HasForeignKey(slta => slta.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}