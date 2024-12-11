using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class StudentExamTestAnswerMapping
    {
        public StudentExamTestAnswerMapping(EntityTypeBuilder<StudentExamTestAnswer> entityBuilder)
        {
            entityBuilder.ToTable("StudentExamTestAnswers");

            entityBuilder.HasKey(seta => seta.Id);

            entityBuilder.HasIndex(seta => new { seta.StudentId, seta.ExamTestId, seta.AnswerId });

            //configuring many to many between entities
            entityBuilder
                .HasOne(seta => seta.Answer)
                .WithMany(a => a.StudentExamTestAnswers)
                .HasForeignKey(seta => seta.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(seta => seta.Student)
                .WithMany(s => s.StudentExamTestAnswers)
                .HasForeignKey(seta => seta.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(seta => seta.ExamTest)
                .WithMany(e => e.StudentExamTestAnswers)
                .HasForeignKey(seta => seta.ExamTestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}