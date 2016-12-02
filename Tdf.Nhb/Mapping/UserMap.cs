using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Tdf.Act.Domain.Entities;

namespace Tdf.Nhb.Mapping
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("`Act_User`");

            Id(c => c.Id, m => m.Generator(Generators.Assigned));

            Property(c => c.Email);
            Property(c => c.UserName);
            Property(c => c.Phone);
            Property(c => c.Password);
        }
    }
}
