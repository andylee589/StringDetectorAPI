using StringDetector.API.Model.DataTransferObjects;
using StringDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StringDetector.API.Controllers
{
    public class JobConfigurationTextController : ApiController
    {
        private readonly IJobService _jobService;
        public JobConfigurationTextController(IJobService jobService)
       {
           _jobService = jobService;
       }

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
