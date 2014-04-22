using StringDetector.API.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace StringDetector.API.WebHost
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;

            RouteConfig.RegisterRoutes(config);


            //matching for attribute routing
           // WebAPIConfig.Configure(config);
            GlobalConfiguration.Configure(WebAPIConfig.Configure);
            AutofacWebAPI.Initialize(config);
            EFConfig.Initialize();
        }


    }
}