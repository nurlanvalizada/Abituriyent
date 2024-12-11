using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class GroupMapping
    {
        public GroupMapping(EntityTypeBuilder<Group> entityBuilder)
        {
            entityBuilder.ToTable("Groups");

            entityBuilder.HasKey(g => g.Id);

            entityBuilder.Property(g => g.Name).HasMaxLength(40).IsRequired();
        }
    }
}