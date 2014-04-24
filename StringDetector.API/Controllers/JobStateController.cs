using StringDetector.API.Model.DataTransferObjects;
using StringDetector.Domain.Services;
using StringDetector.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiDoodle.Net.Http.Client.Model;
using System.Net.Http;
using StringDetector.Domain.Entities;
using StringDetector.API.Connector;


namespace StringDetector.API.Controllers
{
    [RoutePrefix("api/jobs/{jobNumber:regex(^[0-9]{6}$)}")]
    public  class JobStateController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly IJobStateService _jobStateService;
        private readonly IConnector _connector;

        public JobStateController(IJobService jobService , IJobStateService jobStateService, IConnector connector)
       {
           _jobService = jobService;
           _jobStateService = jobStateService;
           _connector = connector;
       }

        [Route("states")]
        [HttpGet]
        public PaginatedDto<JobStateDto> GetStates(string jobNumber)
        {
            var getStatesResult = _jobStateService.GetAllStatesByJobNumber(jobNumber);
            if (!getStatesResult.IsSuccess)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var jobStates = getStatesResult.Entity;
            return jobStates.ToPaginatedDto(jobStates.Select(jobState => jobState.ToJobStateDto()));
        }

        [Route("state")]
        [HttpGet]
        public JobStateDto GetState(string jobNumber)
        {
            var getStateResult = _jobStateService.GetLatestStateByJobNumber(jobNumber);
            if (!getStateResult.IsSuccess)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var jobState = getStateResult.Entity;
            // check if the job is sting running before returning the latest state
            if (jobState.JobStatus == JobStatusEnum.RUNNING || jobState.JobStatus == JobStatusEnum.BEGIN_LAUNCH)
            {
               var isRunningResult =  _connector.CheckIsJobRunning(jobNumber);

               if (!isRunningResult.IsSuccess)
               {
                   // default we set the job ends with success
                   jobState =  _jobService.AddJobState(jobNumber, JobStatusEnum.ENDS_WITH_SUCCESS).Entity;
               }
            }


            return jobState.ToJobStateDto();
        }


     
    }
}
