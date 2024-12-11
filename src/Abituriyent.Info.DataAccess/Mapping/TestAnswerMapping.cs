using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class TestAnswerMapping
    {
        public TestAnswerMapping(EntityTypeBuilder<TestAnswer> entityBuilder)
        {
            entityBuilder.ToTable("TestAnswers");

            entityBuilder.HasKey(ta => ta.Id);

            entityBuilder.HasIndex(ta => new { ta.TestId, ta.AnswerId });

            //configuring one to one relationships between entities
            entityBuilder
                .HasOne(ta => ta.Answer)
                .WithOne(a => a.TestAnswer)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne(ta => ta.Test)
                .WithOne(t => t.TestAnswer)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}