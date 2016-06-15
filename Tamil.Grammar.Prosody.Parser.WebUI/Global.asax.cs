using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Impl;
using ServiceStack.Redis;

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
            builder.Register(c => new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUri"])).As<IRedisClientsManager>();
            builder.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient()).SingleInstance();
            builder.RegisterType<DistributedCachingService>().As<IDistributedCachingService>();
            builder.Register(t => new TamilAgarathi(HostingEnvironment.MapPath(@"~/App_Data/agarathiValid.txt"))).As<ITamilAgarathi>().SingleInstance(); ;
            builder.RegisterType<SharedResourceManager>().As<ISharedResourceManager>();
            builder.RegisterType<ProsodyParser>().As<ProsodyParser>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            var cts = new CancellationTokenSource();
            var agarathi = LoadTamilAgarathi(cts.Token).Result;

        }

        private static Task<ITamilAgarathi> LoadTamilAgarathi(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var agarathi = Container.Resolve<ITamilAgarathi>();
                return agarathi;
            }, cancellationToken);
        }

        protected void Application_BeginRequest()
        {
            string language = "ta";

            var cookie = Request.Cookies.Get("__APPLICATION_LANGUAGE");
            if (cookie != null)
                language = cookie.Value;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }
    }
}
