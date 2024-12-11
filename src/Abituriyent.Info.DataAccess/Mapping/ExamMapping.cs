using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class ExamMapping
    {
        public ExamMapping(EntityTypeBuilder<Exam> entityBuilder)
        {
            entityBuilder.ToTable("Exams");

            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entityBuilder.Property(e => e.Date).ForSqlServerHasColumnType("date").IsRequired();
            entityBuilder.Property(e => e.StartTime).ForSqlServerHasColumnType("time(1)").IsRequired();
            entityBuilder.Property(e => e.EndTime).ForSqlServerHasColumnType("time(1)").IsRequired();
        }
    }
}