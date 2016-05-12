using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tamil.Grammar.Prosody.Parser.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            ConfigureCamelCaseSerialization(config);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureCamelCaseSerialization(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            // This next line is not required for it to work, but here for completeness - ignore data contracts.
            jsonFormatter.UseDataContractJsonSerializer = false;
            var settings = jsonFormatter.SerializerSettings;
#if DEBUG
            // Pretty json for developers.
            settings.Formatting = Formatting.Indented;
#else
        settings.Formatting = Formatting.None;
#endif
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
