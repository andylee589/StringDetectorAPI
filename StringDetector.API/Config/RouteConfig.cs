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

            //// Pipelines
            //HttpMessageHandler affiliateShipmentsPipeline =
            //    HttpClientFactory.CreatePipeline(
            //        new HttpControllerDispatcher(config),
            //        new[] { new AffiliateShipmentsDispatcher() });



            //routes.MapHttpRoute(
            //    name:"JobsRoute",
            //    routeTemplate:"api/jobs/",
                
            //    defaults: new { controller = "Jobs" }
            //   );

            //routes.MapHttpRoute(
            //   name: "JobRoute",
            //   routeTemplate: "api/jobs/{jobNumber}",

            //   defaults: new {   controller ="Job" },
            //   constraints: new { jobNumber = @"^[0-9]{6}$" }
            //  );

          //  routes.MapHttpRoute(
          //     name: "JobStatesRoute",
          //     routeTemplate: "api/jobs/{jobNumber}/states",

          //     defaults: new { controller = "JobStates" },
          //     constraints: new { jobNumber = @"^[0-9]{6}$" }
          //  );

          //  routes.MapHttpRoute(
          //    name: "JobStateRoute",
          //    routeTemplate: "api/jobs/{jobNumber}/state",

          //    defaults: new { controller = "JobState" },
          //     constraints: new { jobNumber = @"^[0-9]{6}$" }
          // );

          //  routes.MapHttpRoute(
          //    name: "JobConfigurationRoute",
          //    routeTemplate: "api/jobs/{jobNumber}/configuration/",

          //    defaults: new { controller = "JobConfiguration"  },
          //     constraints: new { jobNumber = @"^[0-9]{6}$" }
          // );

          //  routes.MapHttpRoute(
          //   name: "JobConfigurationTextRoute",
          //   routeTemplate: "api/jobs/{jobNumber}/configuration/text",

          //   defaults: new { controller = "JobConfigurationText" },
          //    constraints: new { jobNumber = @"^[0-9]{6}$" }
          //);

          //  routes.MapHttpRoute(
          //   name: "JobConfigurationFileRoute",
          //   routeTemplate: "api/jobs/{jobNumber}/configuration/file",

          //   defaults: new { controller = "JobConfigurationFile"},
          //    constraints: new { jobNumber = @"^[0-9]{6}$" }
          //);
          //  routes.MapHttpRoute(
          //    name: "JoReportFileRoute",
          //    routeTemplate: "api/jobs/{jobNumber}/report/file",

          //    defaults: new { controller = "JobReportFile" },
          //    constraints: new { jobNumber = @"^[0-9]{6}$" }
          // );

          //  routes.MapHttpRoute(
          //   name: "JoReportTextRoute",
          //   routeTemplate: "api/jobs/{jobNumber}/report/text",

          //   defaults: new { controller = "JobReportText" },
          //   constraints: new { jobNumber = @"^[0-9]{6}$" }
          //);

            // enable route debugger
           // RouteDebug
        }
    }
}
