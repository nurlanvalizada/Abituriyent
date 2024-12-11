using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class StudentMapping
    {
        public StudentMapping(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.ToTable("Students");

            entityBuilder.HasKey(s => s.Id);

            entityBuilder.Property(s => s.Address).HasMaxLength(100);
            entityBuilder.Property(s => s.DateOfBirth).ForSqlServerHasColumnType("date").IsRequired();
            entityBuilder.Property(s => s.FatherName).HasMaxLength(30);
            entityBuilder.Property(s => s.HomePhone).HasMaxLength(15);
            entityBuilder.Property(s => s.MobilePhone).HasMaxLength(15);
            entityBuilder.Property(s => s.ImageContentType).HasMaxLength(20);

            //one to many relationships between entities
            entityBuilder
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
               .HasOne(s => s.School)
               .WithMany(sc => sc.Students)
               .HasForeignKey(s => s.SchoolId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}