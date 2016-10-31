using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MitchellApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Convert API to JSON
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Map the vehicle route. Other routes can be added similarly.
            config.Routes.MapHttpRoute(
                name: "VehicleApi",
                routeTemplate: "vehicles/{id}",
                defaults: new { Controller = "Vehicles", id = RouteParameter.Optional }
            );
        }
    }
}
