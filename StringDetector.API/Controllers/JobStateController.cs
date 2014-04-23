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


namespace StringDetector.API.Controllers
{
    [RoutePrefix("api/jobs/{jobNumber:regex(^[0-9]{6}$)}")]
    public  class JobStateController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly IJobStateService _jobStateService;

        public JobStateController(IJobService jobService , IJobStateService jobStateService)
       {
           _jobService = jobService;
           _jobStateService = jobStateService;
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
            return jobState.ToJobStateDto();
        }


        [Route("state")]
        [HttpPost]
        public HttpResponseMessage PostState(string jobNumber ,string action)
        {

            if (action == "pause")
            {

            }
            else if (action == "stop")
            {

            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
