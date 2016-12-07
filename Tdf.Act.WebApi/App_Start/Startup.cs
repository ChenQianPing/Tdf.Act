using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using Tdf.Act.WebApi.Providers;
using Tdf.Commanding;
using Tdf.Dependency;
using Tdf.Utils.Config;

[assembly: OwinStartup(typeof(Tdf.Act.WebApi.App_Start.Startup))]

namespace Tdf.Act.WebApi.App_Start
{
    public class Startup
    {
        private ICommandBus _commandBus;

        public Startup(ICommandBus commandBus)
        {
            this._commandBus = commandBus;
        }

        public Startup()
            : this(ObjectContainer.Resolve<ICommandBus>())
        {
        }

        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            //var config = new HttpConfiguration();
            //WebApiConfig.Register(config);

            // $.ajax 跨域请求WebApi
            app.UseCors(CorsOptions.AllowAll);

            ConfigureOAuth(app);
            //app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(ConfigHelper.GetValue("TokenExpireMinute", "120"))),
                RefreshTokenProvider = new TdfRefreshTokenProvider(_commandBus),
                Provider = new TdfAuthorizationServerProvider(_commandBus)

            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);

            var opts = new OAuthBearerAuthenticationOptions()
            {
                Provider = new TdfOAuthBearerProvider("Token")
            };
            app.UseOAuthBearerAuthentication(opts);

        }


    }
}
