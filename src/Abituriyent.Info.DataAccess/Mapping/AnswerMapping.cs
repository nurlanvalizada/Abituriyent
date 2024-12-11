using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class AnswerMapping
    {
        public AnswerMapping(EntityTypeBuilder<Answer> entityBuilder)
        {
            entityBuilder.ToTable("Answers");

            entityBuilder.HasKey(a => a.Id);

            entityBuilder.Property(a => a.Text).HasMaxLength(3000).IsRequired();

            //configuring one to many between entities
            entityBuilder.HasOne(a => a.Test)
                .WithMany(t => t.Answers)
                .HasForeignKey(a => a.TestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}