using Autofac;
using System.Linq;
using System.Reflection;
using Tdf.Act.Domain.Repositories;
using Tdf.Act.EntityFramework;
using Tdf.Commanding;
using Tdf.Dependency;
using Tdf.Domain.Uow;

namespace Tdf.Act.WebApi.Config
{
    public class AutofacConfig
    {
        #region RegisterEfDependencies
        public static void RegisterEfDependencies()
        {
            ObjectContainer.Initialize(x =>
            {

                var dbcontext = new ActDbContext();
                x.RegisterInstance(dbcontext).As<ActDbContext>();

                x.RegisterType<EfUnitOfWork>().Named<IUnitOfWork>("UnitOfWorkImpl");

                x.Register(c =>
                {
                    if (UnitOfWorkContext.CurrentUnitOfWork != null)
                    {
                        return UnitOfWorkContext.CurrentUnitOfWork;
                    }
                    return c.ResolveNamed<IUnitOfWork>("UnitOfWorkImpl");
                }).As<IUnitOfWork>();

                //x.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>));
                x.RegisterType<EntityFramework.Repositories.UserRepository>().As<IUserRepository>();

                x.RegisterType<DefaultCommandBus>().As<ICommandBus>().SingleInstance();

                var asm = Assembly.Load("Tdf.Act.Domain");

                x.RegisterAssemblyTypes(asm).Where(it => !it.IsInterface && !it.IsAbstract).AsClosedTypesOf(typeof(ICommandExecutor<>));
            });
        }
        #endregion
    }
}