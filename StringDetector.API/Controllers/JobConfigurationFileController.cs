using StringDetector.API.Model.DataTransferObjects;
using StringDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StringDetector.API.Controllers
{
    public class JobConfigurationFileController : ApiController
    {
        private readonly IJobService _jobService;
        public JobConfigurationFileController(IJobService jobService)
       {
           _jobService = jobService;
       }

        [HttpGet]
        public HttpResponseMessage GetConfigurationInFile(string jobNumber)
        {
            try { 

                var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
                if (!getJobResult.IsSuccess)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                var configruation = getJobResult.Entity.Configuration;
                var projectName = getJobResult.Entity.ProjectName;
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(configruation);
                //a text file is actually an octet-stream (pdf, etc)
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                //we used attachment to force download
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "JOB"+jobNumber+"_"+projectName+".config";
                return result;
            }
            catch (Exception excption)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


    }
}
