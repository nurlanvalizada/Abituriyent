using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class TestMapping
    {
        public TestMapping(EntityTypeBuilder<Test> entityBuilder)
        {
            entityBuilder.ToTable("Tests");

            entityBuilder.HasKey(t => t.Id);

            entityBuilder.Property(t => t.Content).ForSqlServerHasColumnType("nvarchar(MAX)");

            //configuring one to one relationships between entities
            entityBuilder
                .HasOne(t => t.TestAnswer)
                .WithOne(ta => ta.Test)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}