using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class NewsMapping
    {
        public NewsMapping(EntityTypeBuilder<News> entityBuilder)
        {
            entityBuilder.ToTable("News");

            entityBuilder.HasKey(n => n.Id);

            entityBuilder.Property(n => n.ImageUrl).HasMaxLength(100);
            entityBuilder.Property(n => n.ShortContent).HasMaxLength(1000);
            entityBuilder.Property(n => n.Content).ForSqlServerHasColumnType("nvarchar(MAX)").IsRequired();
            entityBuilder.Property(n => n.Title).HasMaxLength(200).IsRequired();
            entityBuilder.Property(n => n.PublishDate).ForSqlServerHasColumnType("datetime2(1)").IsRequired();

            //configuring one to many between entities
            entityBuilder.HasOne(n => n.Admin)
                .WithMany(a => a.Newses)
                .HasForeignKey(n => n.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}