using Abituriyent.Info.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abituriyent.Info.DataAccess.Mapping
{
    public class AdminMapping
    {
        public AdminMapping(EntityTypeBuilder<Admin> entityBuilder)
        {
            entityBuilder.ToTable("Admins");

            entityBuilder.HasKey(a => a.Id);
        }
    }
}