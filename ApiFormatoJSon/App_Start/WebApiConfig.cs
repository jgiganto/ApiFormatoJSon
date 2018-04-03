using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace ApiFormatoJSon
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter); //eliminar el formato xml
            ConfigurarFormatoJson(config);//siempre indicarlo antes del mapeo

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ConfigurarFormatoJson(HttpConfiguration config)
        {
            //capturamos el formato json para darle otra salida
            //RECUPERAMOS EL FORMATO
            var formato = config.Formatters.JsonFormatter;

            //PONEMOS TODAS LAS PROPIEDADES A MINUSCULA
            formato.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            //INDICAMOS QUE NO QUEREMOS MOSTRAR VALORES NULOS
            formato.SerializerSettings.NullValueHandling =
                Newtonsoft.Json.NullValueHandling.Ignore;

            //CAMBIAR EL FORMATO DE FECHA
            formato.SerializerSettings.DateFormatHandling =
                Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;

            //CAMBIAR LA CULTURA DE LO QUE EXPONEMOS
            //A ESPAÑA, COMO POR EJEMPLO MONEDAS
            formato.SerializerSettings.Culture = new CultureInfo("es-ES");

        }
    }

}
