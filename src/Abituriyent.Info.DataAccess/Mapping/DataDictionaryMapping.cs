using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class DataDictionaryMapping
    {
        public DataDictionaryMapping(EntityTypeBuilder<DataDictionary> entityBuilder)
        {
            entityBuilder.ToTable("DataDictionary");

            entityBuilder.HasKey(dd => dd.Id);

            entityBuilder.Property(dd => dd.Key).HasMaxLength(30).IsRequired();
            entityBuilder.Property(dd => dd.Header).HasMaxLength(500).IsRequired();
            entityBuilder.Property(dd => dd.Value).ForSqlServerHasColumnType("nvarchar(MAX)").IsRequired();
        }
    }
}