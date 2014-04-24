using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
//using PingYourPackage.API.Routing;
using System.Net.Http;
using System.Web.Http.Dispatcher;
using StringDetector.API.Routing;
//using PingYourPackage.API.Dispatcher;

namespace StringDetector.API.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
           
            // enable attribute mapping
            config.MapHttpAttributeRoutes();
            var routes = config.Routes;

            // enable route debugger
           // RouteDebug
        }
    }
}
