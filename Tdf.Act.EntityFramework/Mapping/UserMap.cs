using System.Data.Entity.ModelConfiguration;
using Tdf.Act.Domain.Entities;

namespace Tdf.Act.EntityFramework.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Act_User");

            this.Property(t => t.Id).HasColumnName("Id");
        }
    }
}

  
