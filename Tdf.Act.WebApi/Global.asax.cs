using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Tdf.WebApi.Filters;

namespace Tdf.Act.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // registerDependencies
            Config.AutofacConfig.RegisterEfDependencies();

            // log4net
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("Config/log4net.config")));

            // exceptionFilter
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionFilter());
        }
    }
}
