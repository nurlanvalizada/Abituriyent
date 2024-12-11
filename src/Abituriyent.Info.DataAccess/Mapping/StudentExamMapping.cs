using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class StudentExamMapping
    {
        public StudentExamMapping(EntityTypeBuilder<StudentExam> entityBuilder)
        {
            entityBuilder.ToTable("StudentExams");

            entityBuilder.HasKey(se => se.Id);

            entityBuilder.HasIndex(se => new { se.StudentId, se.GroupExamId }).IsUnique();

            entityBuilder.Property(se => se.StartTime).ForSqlServerHasColumnType("time(1)").IsRequired();
            entityBuilder.Property(se => se.EndTime).ForSqlServerHasColumnType("time(1)");

            //configuring many to many between entities
            entityBuilder
                .HasOne(se => se.Student)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(se => se.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(se => se.GroupExam)
                .WithMany(e => e.StudentExams)
                .HasForeignKey(se => se.GroupExamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}