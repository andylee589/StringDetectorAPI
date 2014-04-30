using System;
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
using StringDetector.API.Connector;
using System.IO;


namespace StringDetector.API.Controllers
{
   [RoutePrefix("api/jobs")]
   public  class JobsController : ApiController
    {
       private readonly IJobService _jobService;
       private readonly IJobStateService _jobStateService;
       private readonly IAutoGenerateKeyService _autoKeyService;
       private readonly IConnector _connector;

       public JobsController(IJobService jobService, IJobStateService jobStateService,IAutoGenerateKeyService autoKeyService, IConnector connector)
       {
           _jobService = jobService;
           _jobStateService = jobStateService;
           _autoKeyService = autoKeyService;
           _connector = connector;
       }

       /*****************************************About Jobs************************************/
      // [EmptyParameterFilter("cmd")]
       [Route("")]
       [HttpGet]
       public PaginatedDto<JobDto> GetJobsCmd([FromUri]PaginatedRequestCommand cmd )
       {
           
           var jobs = _jobService.GetJobs(cmd.Page, cmd.Take);
          // var jobs = _jobService.GetJobs(page, take);
           return jobs.ToPaginatedDto(jobs.Select(job => job.ToJobDto()));
       }
        [Route("")]
       [HttpGet]
       public PaginatedDto<JobDto> Getjobs()
       {
           var jobs = _jobService.GetJobs();
           return jobs.ToPaginatedDto(jobs.Select(job => job.ToJobDto()));
       }

       [Route("")]
       // will implement POE later
       [HttpPost]
       [EmptyParameterFilter("requestModel")]
       public HttpResponseMessage PostJobs(JobCreateRequestModel requestModel){
           if(requestModel.JobNumber==null||requestModel.JobNumber==""){
               // will generate a six digit figure 
               if (_autoKeyService.hasNextKey())
               {
                   requestModel.JobNumber = _autoKeyService.getNextKey().ToString();
               }
           }

           if(_jobService.isJobExsistsByJobNumber(requestModel.JobNumber)){
               return new HttpResponseMessage(HttpStatusCode.BadRequest);
           }

           var oprationResult = _jobService.AddJob(requestModel.ToJob());
           if (!oprationResult.IsSuccess)
           {
               return new HttpResponseMessage(HttpStatusCode.Conflict);
           }

           var response = Request.CreateResponse(HttpStatusCode.Created, oprationResult.Entity.ToJobDto());
           string baseUrl = Request.RequestUri.AbsoluteUri;
           response.Headers.Location = new Uri(baseUrl+"/" + oprationResult.Entity.JobNumber);
           return response;
           
       }

       /*****************************************About Job************************************/
       [Route("{jobNumber:regex(^[0-9]{6}$)}")]
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

       // start the job
       [Route("{jobNumber:regex(^[0-9]{6}$)}/Task")]
       [HttpPost]
       // [OptionalEmptyParameterFilter("withModel","requestModel")]
       public HttpResponseMessage PostJob(string jobNumber, JobLaunchRequestModel requestModel )
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

           //if (requestModel != null)
           //{
           //    string configuration = requestModel.Configuration;
           //    // post the configuration to the tool end

           //}


           requestModel = requestModel ?? new JobLaunchRequestModel(job.Configuration);
           string configuration = requestModel.Configuration;
           // update configiguration



           double period = requestModel.Period;

           //check  whether the job sourcep path and configuration file exesists in the tool provider;
           if (!_connector.CheckIsReadyForLaunch(job.SourcePath).IsSuccess)
           {
               return new HttpResponseMessage(HttpStatusCode.BadRequest);
           }
          // JobStateEntity launchJobState = _jobService.AddJobState(jobNumber, JobStatusEnum.BEGIN_LAUNCH).Entity;
           var apiResponse =  _connector.LaunchJobAsync(jobNumber, job.Configuration);
           TJob tjob = apiResponse.Model;

           if (!apiResponse.IsSuccess)
           {
               //_jobStateService.DeleteJobState(launchJobState);
               return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
           }
           //job launched
           _jobService.AddJobState(jobNumber, JobStatusEnum.RUNNING);
           // save the report path
           string reportPath = tjob.reportPath;
           job.Report = reportPath;
           _jobService.UpdateJobByJobNumber(job.JobNumber,job);

          return  Request.CreateResponse(HttpStatusCode.Accepted,job.ToJobDto());
       }



       // stop the job
       [Route("{jobNumber:regex(^[0-9]{6}$)}/task")]
       [HttpDelete]
       public HttpResponseMessage DeleteJobTask(string jobNumber)
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
               if (_connector.StopJob(jobNumber).IsSuccess)
               {
                   return new HttpResponseMessage(HttpStatusCode.OK);
               }
               else
               {
                   return new HttpResponseMessage(HttpStatusCode.BadRequest);
               }
           }
           return new HttpResponseMessage(HttpStatusCode.BadRequest);
       }

       // other  operations on  job, action : pause ,restart and so on

       [Route("{jobNumber:regex(^[0-9]{6}$)}/task")]
       [HttpDelete]
       public HttpResponseMessage PutJobTask(string jobNumber, [FromBody]string state)
       {
           if (state == "pause")
           {

           }
           else if (state == "restart")
           {

           }

           return new HttpResponseMessage(HttpStatusCode.OK);
       }



       [Route("{jobNumber:regex(^[0-9]{6}$)}")]
       [HttpPut]
       [EmptyParameterFilter("requestModel")]
       public JobDto PutJob(string jobNumber, JobUpdateRequestModel requestModel)
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
                   if (property != null)
                   {
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


       [Route("{jobNumber:regex(^[0-9]{6}$)}")]
       [HttpPut]
       public JobDto PutJob(string jobNumber, [FromUri] JobUpdateOptionalModel requestModel)
       {
           return null;
       }


       //private JobReportDto getJobReportDto(string reportPath, JobEntity job)
       //{
       //    if (!File.Exists(reportPath))
       //    {
       //        throw new HttpResponseException(HttpStatusCode.NotFound);
       //    }
          
       //     MemoryStream responseStream = new MemoryStream();
       //     Stream fileStream = File.Open(reportPath, FileMode.Open);
       //     fileStream.CopyTo(responseStream);
       //     fileStream.Close();
       //     responseStream.Position = 0;

       //     string contentStr = new StreamContent(responseStream).ReadAsStringAsync().Result;
       //     // save the report content 

       //     return   new JobReportDto { ReportContent = contentStr, ReportUrl = reportPath );
              
           
       // }

      }
}
