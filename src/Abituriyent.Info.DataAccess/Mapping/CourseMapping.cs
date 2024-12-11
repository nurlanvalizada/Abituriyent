using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class CourseMapping
    {
        public CourseMapping(EntityTypeBuilder<Course> entityBuilder)
        {
            entityBuilder.ToTable("Courses");

            entityBuilder.HasKey(c => c.Id);
            
            entityBuilder.Property(c => c.Description).HasMaxLength(3000);
            entityBuilder.Property(c => c.ImageUrl).HasMaxLength(100);
            entityBuilder.Property(c => c.TeacherId).IsRequired(false);
            entityBuilder.Property(c => c.ScoreRate).IsRequired();

            //configuring one to many between entities
            entityBuilder.HasOne(c => c.Group)
                .WithMany(g => g.Courses)
                .HasForeignKey(c => c.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(c => c.Subject)
             .WithMany(s => s.Courses)
             .HasForeignKey(c => c.SubjectId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}