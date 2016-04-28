using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using RjamSoft.Tamil.Grammar.Parser;

namespace Tamil.Grammar.Prosody.Parser.WebUI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IContainer Container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // Register types that expose interfaces
            builder.Register(t => new TamilAgarathi(HttpContext.Current.Server.MapPath(@"~/App_Data/agarathiValid.txt"))).As<ITamilAgarathi>().SingleInstance(); ;
            builder.RegisterType<ProsodyParser>().As<ProsodyParser>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
