using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class CityMapping
    {
        public CityMapping(EntityTypeBuilder<City> entityBuilder)
        {
            entityBuilder.ToTable("Cities");

            entityBuilder.HasKey(c => c.Id);

            entityBuilder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        }
    }
}