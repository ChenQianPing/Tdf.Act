﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Tdf.Act.WebApi.App_Start.Startup))]

namespace Tdf.Act.WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            // $.ajax 跨域请求WebApi
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
