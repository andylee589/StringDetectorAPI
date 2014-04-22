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
    public  class JobStateController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly IJobStateService _jobStateService;

        public JobStateController(IJobService jobService , IJobStateService jobStateService)
       {
           _jobService = jobService;
           _jobStateService = jobStateService;
       }


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
