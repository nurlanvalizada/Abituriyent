using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class ContactMessageMapping
    {
        public ContactMessageMapping(EntityTypeBuilder<ContactMessage> entityBuilder)
        {
            entityBuilder.ToTable("ContactMessages");

            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.FullName).HasMaxLength(50).IsRequired();
            entityBuilder.Property(c => c.Email).HasMaxLength(50).IsRequired();
            entityBuilder.Property(c => c.Topic).HasMaxLength(100).IsRequired();
            entityBuilder.Property(c => c.Message).HasMaxLength(3000).IsRequired();
            entityBuilder.Property(c => c.SendDate).ForSqlServerHasColumnType("datetime2(1)").IsRequired();
        }
    }
}