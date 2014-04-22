﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using StringDetector.Domain.Services;
using StringDetector.API.Model;
using StringDetector.API.Model.DataTransferObjects;
using StringDetector.API.Model.RequestCommands;
using StringDetector.API.Model.RequestModels;
using WebApiDoodle.Net.Http.Client.Model;
using System.Net.Http;
using System.Net;
using StringDetector.Domain.Entities;
using StringDetector.API.Filters;
using System.Reflection;
using System.ComponentModel;

namespace StringDetector.API.Controllers
{
 public    class JobController : ApiController
    {
       private readonly IJobService _jobService;
       private readonly IJobStateService _jobStateService;
       private readonly IAutoGenerateKeyService _autoKeyService;
       public JobController(IJobService jobService, IJobStateService jobStateService,IAutoGenerateKeyService autoKeyService)
       {
           _jobService = jobService;
           _jobStateService = jobStateService;
           _autoKeyService = autoKeyService;
       }

       [HttpGet]

       public JobDto GetJob(string jobNumber)
       {
           var operationResult = _jobService.GetJobByJobNumber(jobNumber);
           if (!operationResult.IsSuccess)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }

           return operationResult.Entity.ToJobDto();
       }



       [HttpPost]
      // [OptionalEmptyParameterFilter("withModel","requestModel")]
       public HttpResponseMessage PostJob(string jobNumber,  JobLaunchRequestModel requestModel /*, bool withModel = false*/)
       {
           var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
           if (!getJobResult.IsSuccess)
           {
               return new HttpResponseMessage(HttpStatusCode.NotFound);
           }
           var job = getJobResult.Entity;
           var getStateResult = _jobStateService.GetLatestStateByJobKey(job.Key);
           var latesState = getStateResult.Entity;
           if (latesState.JobStatus == JobStatusEnum.BEGIN_LAUNCH || latesState.JobStatus == JobStatusEnum.RUNNING)
           {
               return new HttpResponseMessage(HttpStatusCode.BadRequest);
           }
           requestModel = requestModel ?? new JobLaunchRequestModel(job.Configuration);
           string  configuration = requestModel.Configuration;
           double period = requestModel.Period;
          
           _jobService.AddJobState(jobNumber, JobStatusEnum.BEGIN_LAUNCH);
           //begin finding the source path

           //job launched
           _jobService.AddJobState(jobNumber, JobStatusEnum.RUNNING);
           // asynchronous task for running string detector

           return new HttpResponseMessage(HttpStatusCode.Accepted);

       }



       [HttpPut]
       [EmptyParameterFilter("requestModel")]
       public JobDto PutJob(string jobNumber, JobUpdateRequestModel requestModel )
       {

           var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
           if (!getJobResult.IsSuccess)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           var job = getJobResult.Entity;
           PropertyInfo[] properties = requestModel.GetType().GetProperties();
           foreach (PropertyInfo info in properties)
           {
               object value = info.GetValue(requestModel);
               if (value != null)
               {
                   PropertyInfo property = job.GetType().GetProperty(info.Name);
                   if(property!=null){
                        property.SetValue(job, value);
                   }
               }
           }

           // some other property for update

           var updatedJobResult = _jobService.UpdateJobByJobNumber(jobNumber, job);
           if (!updatedJobResult.IsSuccess)
           {
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           var updatedJob = updatedJobResult.Entity;
           return updatedJob.ToJobDto();
       }

        [HttpPut]
       public JobDto PutJob(string jobNumber, [FromUri] JobUpdateOptionalModel requestModel)
       {
           return null;
       }
      
    }
}