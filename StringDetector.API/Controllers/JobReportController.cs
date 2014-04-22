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
     [RoutePrefix("api/jobs/{jobNumber:regex(^[0-9]{6}$)}")]
    public class JobReportController : ApiController
    {
       
            private readonly IJobService _jobService;
            public JobReportController(IJobService jobService)
            {
                _jobService = jobService;
            }

            [Route("report")]
            [HttpGet]
            public JobReportDto GetReport(string jobNumber)
            {
                var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
                if (!getJobResult.IsSuccess)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                var job = getJobResult.Entity;
                return new JobReportDto { ReportUrl = job.Report };
            }

           
    }
}
