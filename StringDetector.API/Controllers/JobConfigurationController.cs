﻿using StringDetector.API.Filters;
using StringDetector.API.Model.DataTransferObjects;
using StringDetector.API.Model.RequestModels;
using StringDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StringDetector.API.Controllers
{
     [RoutePrefix("api/jobs/{jobNumber:regex(^[0-9]{6}$)}")]
     public  class JobConfigurationController : ApiController
    {
       private readonly IJobService _jobService;
       public JobConfigurationController(IJobService jobService)
       {
           _jobService = jobService;
       }

       [Route("configuration")]
       [HttpGet]
       public JobConfigurationDto GetConfiguration(string jobNumber)
       {
           var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
           if (!getJobResult.IsSuccess)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           var job = getJobResult.Entity;
           return new JobConfigurationDto {  Key = job.Key , Configuration = job.Configuration};
       }

       [Route("configuration")]
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
    }
}