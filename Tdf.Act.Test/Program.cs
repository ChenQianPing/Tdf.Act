using Autofac;
using NHibernate;
using System;
using System.Reflection;
using Tdf.Act.Application;
using Tdf.Act.DapperLambda;
using Tdf.Act.Domain.Commands;
using Tdf.Act.Domain.Repositories;
using Tdf.Act.EntityFramework;
using Tdf.Act.SqlSugar;
using Tdf.Commanding;
using Tdf.Dependency;
using Tdf.Domain.Uow;
using Tdf.Nhb;

namespace Tdf.Act.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //RegisterEfDependencies();
            //RegisterNhbDependencies();
            RegisterSugarDependencies();
            //RegisterDapperLambdaDependencies();

            //Console.Write("Congratulations, UserId is:" + Register());
            Console.Write("Congratulations, UserId is:" + LoginAndChangePwd());
            Console.Read();
        }

        #region test Register
        static string Register()
        {
            var command = new RegisterCommand();
            command.UserName = "Bobby";
            command.Password = "123456";
            command.ConfirmPassword = "123456";
            command.Email = "359960779@qq.com";
            command.Phone = "13312520027";

            try
            {
                new UserAppService(ObjectContainer.Resolve<ICommandBus>()).Register(command);
                var currentUserId = command.ExecutionResult.GeneratedUserId;
                return currentUserId;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + "\n");
                return "null";
            }

        }
        #endregion

        #region test LoginAndChangePwd
        static string LoginAndChangePwd()
        {
            var loginCommand = new LoginCommand();
            loginCommand.UserName = "Bobby";
            loginCommand.Password = "123456";

            new UserAppService(ObjectContainer.Resolve<ICommandBus>()).Login(loginCommand);
            var currentUserId = loginCommand.ExecutionResult.UserId;

            //var changePwdCommand = new ChangePwdCommand();
            //changePwdCommand.UserId = new Guid(currentUserId);
            //changePwdCommand.OldPwd = "654321";
            //changePwdCommand.NewPwd = "123456";
            //new UserAppService(ObjectContainer.Resolve<ICommandBus>()).ChangePwd(changePwdCommand);

            //currentUserId = changePwdCommand.ExecutionResult.UserId;
            return currentUserId;
        }
        #endregion

        #region RegisterDapperLambdaDependencies
        static void RegisterDapperLambdaDependencies()
        {
            ObjectContainer.Initialize(x =>
            {
                x.RegisterType<DapperLambdaUnitOfWork>().Named<IUnitOfWork>("UnitOfWorkImpl");

                x.Register(c =>
                {
                    if (UnitOfWorkContext.CurrentUnitOfWork != null)
                    {
                        return UnitOfWorkContext.CurrentUnitOfWork;
                    }
                    return c.ResolveNamed<IUnitOfWork>("UnitOfWorkImpl");
                }).As<IUnitOfWork>();

                x.RegisterType<DapperLambda.Repositories.UserRepository>().As<IUserRepository>();

                x.RegisterType<DefaultCommandBus>().As<ICommandBus>().SingleInstance();

                var asm = Assembly.Load("Tdf.Act.Domain");

                x.RegisterAssemblyTypes(asm).Where(it => !it.IsInterface && !it.IsAbstract).AsClosedTypesOf(typeof(ICommandExecutor<>));
            });
        }
        #endregion

        #region RegisterSugarDependencies
        static void RegisterSugarDependencies()
        {
            ObjectContainer.Initialize(x =>
            {
                x.RegisterType<SugarUnitOfWork>().Named<IUnitOfWork>("UnitOfWorkImpl");

                x.Register(c =>
                {
                    if (UnitOfWorkContext.CurrentUnitOfWork != null)
                    {
                        return UnitOfWorkContext.CurrentUnitOfWork;
                    }
                    return c.ResolveNamed<IUnitOfWork>("UnitOfWorkImpl");
                }).As<IUnitOfWork>();

                x.RegisterType<SqlSugar.Repositories.UserRepository>().As<IUserRepository>();

                x.RegisterType<DefaultCommandBus>().As<ICommandBus>().SingleInstance();

                var asm = Assembly.Load("Tdf.Act.Domain");

                x.RegisterAssemblyTypes(asm).Where(it => !it.IsInterface && !it.IsAbstract).AsClosedTypesOf(typeof(ICommandExecutor<>));
            });
        }
        #endregion

        #region RegisterEfDependencies
        static void RegisterEfDependencies()
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

        #region RegisterNhbDependencies
        static void RegisterNhbDependencies()
        {
            ObjectContainer.Initialize(x =>
            {
                x.Register(c => SessionManager.OpenSession()).As<ISession>();

                x.RegisterType<NhbUnitOfWork>().Named<IUnitOfWork>("UnitOfWorkImpl");

                x.Register(c =>
                {
                    if (UnitOfWorkContext.CurrentUnitOfWork != null)
                    {
                        return UnitOfWorkContext.CurrentUnitOfWork;
                    }
                    return c.ResolveNamed<IUnitOfWork>("UnitOfWorkImpl");
                }).As<IUnitOfWork>();

                //x.RegisterGeneric(typeof(NhbRepository<>)).As(typeof(IRepository<>));
                x.RegisterType<Nhb.Repositories.UserRepository>().As<IUserRepository>();

                x.RegisterType<DefaultCommandBus>().As<ICommandBus>().SingleInstance();

                var asm = Assembly.Load("Tdf.Act.Domain");

                x.RegisterAssemblyTypes(asm).Where(it => !it.IsInterface && !it.IsAbstract).AsClosedTypesOf(typeof(ICommandExecutor<>));
            });
        }
        #endregion

    }
}
