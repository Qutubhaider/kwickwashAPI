using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Cors;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace APIKwickWash
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services
           
            //Core Setting
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //Formatters
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
