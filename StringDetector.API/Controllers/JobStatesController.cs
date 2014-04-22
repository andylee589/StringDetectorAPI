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

namespace StringDetector.API.Controllers
{
    public class JobStatesController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly IJobStateService _jobStateService;

        public JobStatesController(IJobService jobService , IJobStateService jobStateService)
       {
           _jobService = jobService;
           _jobStateService = jobStateService;
       }

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
    }
}
