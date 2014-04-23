using StringDetector.API.Filters;
using StringDetector.API.Model.DataTransferObjects;
using StringDetector.API.Model.RequestModels;
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
    [RoutePrefix("api/jobs/{jobNumber:regex(^[0-9]{6}$)}/configuration")]
     public  class JobConfigurationController : ApiController
    {
       private readonly IJobService _jobService;
       public JobConfigurationController(IJobService jobService)
       {
           _jobService = jobService;
       }


       [Route("")]
       [HttpPut]
       [EmptyParameterFilter("requestModel")]
       public JobConfigurationDto PutConfiguration(string jobNumber, JobConfUpdateRequestModel requestModel)
       {
           var updateJobResult = _jobService.UpdateJobConfiguration(jobNumber, requestModel.Configuration);
           if (!updateJobResult.IsSuccess)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           var updatedJob = updateJobResult.Entity;
           return new JobConfigurationDto {  Key = updatedJob.Key, Configuration = updatedJob.Configuration };
       }

       [Route("file")]
       [HttpGet]
       public HttpResponseMessage GetConfigurationInFile(string jobNumber)
       {
           try
           {

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
               result.Content.Headers.ContentDisposition.FileName = "JOB" + jobNumber + "_" + projectName + ".config";
               return result;
           }
           catch (Exception excption)
           {
               throw new HttpResponseException(HttpStatusCode.InternalServerError);
           }
       }


       [Route("text")]
       [HttpGet]
       public JobConfigurationDto GetConfigurationInText(string jobNumber)
       {
           var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
           if (!getJobResult.IsSuccess)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           var job = getJobResult.Entity;
           return new JobConfigurationDto { Key = job.Key, Configuration = job.Configuration };
       }
    }
}
