using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TaxiOrNot.RestApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Taxis API routes
            config.Routes.MapHttpRoute(
                name: "UsersApi",
                routeTemplate: "api/users",
                defaults: new { controller = "users" });

            // Cities API routes
            config.Routes.MapHttpRoute(
                name: "CitiesApi",
                routeTemplate: "api/cities/{cityId}",
                defaults: new { controller = "cities", cityId = RouteParameter.Optional });

            // Taxis API routes
            config.Routes.MapHttpRoute(
                name: "TaxisApi",
                routeTemplate: "api/taxis/{taxiId}",
                defaults: new { controller = "taxis", taxiId = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "TaxisPutApi",
                routeTemplate: "api/taxis/{taxiId}/{action}",
                defaults: new { controller = "taxis" });

            // Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}