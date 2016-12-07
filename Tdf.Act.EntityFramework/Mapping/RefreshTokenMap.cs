using System.Data.Entity.ModelConfiguration;
using Tdf.Act.Domain.Entities;

namespace Tdf.Act.EntityFramework.Mapping
{
    public class RefreshTokenMap : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Act_RefreshToken");

            this.Property(t => t.Id).HasColumnName("RefreshTokenId");
        }
    }
}
