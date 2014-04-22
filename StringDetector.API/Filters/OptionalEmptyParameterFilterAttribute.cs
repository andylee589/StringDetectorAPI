using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace StringDetector.API.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public  class OptionalEmptyParameterFilterAttribute :ActionFilterAttribute  {
         public string ModelParameterName { get; private set; }
         public string BoolParameterName { get; private set; }

         public OptionalEmptyParameterFilterAttribute(string boolParameterName, string modelParameterName)
         {

             if (string.IsNullOrEmpty(modelParameterName))
             {

                 throw new ArgumentNullException("modelParameterName");
            }
             if (string.IsNullOrEmpty(boolParameterName))
             {

                 throw new ArgumentNullException("boolParameterName");
             }

             ModelParameterName = modelParameterName;
             BoolParameterName = boolParameterName;
        }

        public override void OnActionExecuting(
            HttpActionContext actionContext) {
            
            object modelParameterValue;
            object boolParameterValue;
            if (actionContext.ActionArguments.TryGetValue(ModelParameterName, out modelParameterValue) && actionContext.ActionArguments.TryGetValue(BoolParameterName, out boolParameterValue))
            {

                if ((bool)boolParameterValue ==true && modelParameterValue == null) {

                    actionContext.ModelState.AddModelError(
                        ModelParameterName, FormatErrorMessage(ModelParameterName,BoolParameterName));

                    actionContext.Response = actionContext
                        .Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, actionContext.ModelState);
                }
            }
        }

        private string FormatErrorMessage(string modelParameterName,string boolParameterName) {

            return string.Format("The {0} cannot be null with {1} set to true.", modelParameterName,boolParameterName);
        }
    }
    
    
}
