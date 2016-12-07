using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tdf.Act.Domain.Entities;
using Tdf.Act.EntityFramework.Mapping;
using Tdf.Domain.Uow;

namespace Tdf.Act.EntityFramework
{
    public class ActDbContext : DbContext
    {
        public ActDbContext()
            : base("name=DefaultDBConnection")
        {
            Database.SetInitializer<ActDbContext>(null);
            this.Database.Log = new Action<string>(q => Debug.WriteLine(q));
        }

        #region Property

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 设置禁用一对多级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            #region Mapping

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RefreshTokenMap());

            #endregion
        }

        
    }
}