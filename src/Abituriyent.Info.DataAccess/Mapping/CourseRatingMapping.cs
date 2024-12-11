using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class CourseRatingMapping
    {
        public CourseRatingMapping(EntityTypeBuilder<CourseRating> entityBuilder)
        {
            entityBuilder.ToTable("CourseRatings");

            entityBuilder.HasKey(cr => cr.Id);

            entityBuilder.HasIndex(cr => new { cr.StudentId, cr.CourseId }).IsUnique();

            entityBuilder.Property(cr => cr.Comment).HasMaxLength(1000);

            //configuring one to many between entities
            entityBuilder.HasOne(cr => cr.Student)
                .WithMany(s => s.CourseRatings)
                .HasForeignKey(cr => cr.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(cr => cr.Course)
                .WithMany(c => c.CourseRatings)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}