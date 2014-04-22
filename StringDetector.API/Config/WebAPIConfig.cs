using StringDetector.API.Formatting;
using StringDetector.API.MessageHandlers;
using StringDetector.API.Model.RequestCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using WebApiDoodle.Web.Controllers;
using WebApiDoodle.Web.Filters;
//using PingYourPackage.API.Model.RequestCommands;
//using WebApiDoodle.Web.Filters;

namespace StringDetector.API.Config
{
   public  class WebAPIConfig
    {
       public static void Configure(HttpConfiguration config)
       {
          //Enable system diagnostics tracing
           config.EnableSystemDiagnosticsTracing();
           // Message Handlers
           //config.MessageHandlers.Add(new StringDetectorAuthHandler());
           // Formatters
           var jqueryFormatter = config.Formatters.FirstOrDefault(
               x => x.GetType() ==
                   typeof(JQueryMvcFormUrlEncodedFormatter));

           config.Formatters.Remove(
               config.Formatters.FormUrlEncodedFormatter);
           config.Formatters.Remove(jqueryFormatter);

           // Suppressing the IRequiredMemberSelector for all formatters
           foreach (var formatter in config.Formatters)
           {

               formatter.RequiredMemberSelector =
                   new SuppressedRequiredMemberSelector();
           }

           //// Filters
           config.Filters.Add(new InvalidModelStateFilterAttribute());

           //Default Services

           // If ExcludeMatchOnTypeOnly is true then we don't match on type only which means
           // that we return null if we can't match on anything in the request. This is useful
           // for generating 406 (Not Acceptable) status codes.
           config.Services.Replace(typeof(IContentNegotiator),
               new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));

           // Remove all the validation providers 
           // except for DataAnnotationsModelValidatorProvider
           config.Services.RemoveAll(typeof(ModelValidatorProvider),
               validator => !(validator is DataAnnotationsModelValidatorProvider));

           //// ParameterBindingRules

           // Any complex type parameter which is Assignable From 
           // IRequestCommand will be bound from the URI
           config.ParameterBindingRules.Insert(0, descriptor =>
               typeof(IRequestCommand).IsAssignableFrom(descriptor.ParameterType)
                   ? new FromUriAttribute().GetBinding(descriptor) : null);


           // Replace the default action IHttpActionSelector with
           // WebAPIDoodle.Controllers.ComplexTypeAwareActionSelector
           config.Services.Replace(
               typeof(IHttpActionSelector),
               new ComplexTypeAwareActionSelector());

       }
    }
}
