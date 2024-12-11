using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class GroupExamMapping
    {
        public GroupExamMapping(EntityTypeBuilder<GroupExam> entityBuilder)
        {
            entityBuilder.ToTable("GroupExams");

            entityBuilder.HasKey(ge => ge.Id );

            entityBuilder.HasIndex(ge => new { ge.GroupId, ge.ExamId }).IsUnique();

            //configuring many to many between entities
            entityBuilder
                .HasOne(ge => ge.Exam)
                .WithMany(s => s.GroupExams)
                .HasForeignKey(ge => ge.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(ge => ge.Group)
                .WithMany(e => e.GroupExams)
                .HasForeignKey(ge => ge.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}