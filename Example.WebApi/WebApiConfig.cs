// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The web api config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.WebApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The web api config.
    /// </summary>
    public static class WebApiConfig
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });



            var dateTimeConverter = new IsoDateTimeConverter();
            // Default for IsoDateTimeConverter is yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK
            dateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm";

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            config.Formatters.JsonFormatter.SerializerSettings.Converters = new List<JsonConverter>
                                                                                {
                                                                                    dateTimeConverter
                                                                                };

            // Remove default XML handler
            var matches =
                config.Formatters.Where(
                    f =>
                    f.SupportedMediaTypes.Where(
                        m => m.MediaType.ToString() == "application/xml" || m.MediaType.ToString() == "text/xml")
                        .Count() > 0).ToList();
            foreach (var match in matches)
            {
                config.Formatters.Remove(match);
            }
        }

        #endregion
    }
}