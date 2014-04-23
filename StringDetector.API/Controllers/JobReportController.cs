using StringDetector.API.Model.DataTransferObjects;
using StringDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StringDetector.API.Controllers
{
  [RoutePrefix("api/jobs/{jobNumber:regex(^[0-9]{6}$)}/report")]
  public  class JobReportController :ApiController
    {  
        private readonly IJobService _jobService;
        public JobReportController(IJobService jobService)
        {
            _jobService = jobService;
        }


        [Route("file")]
        [HttpGet]
        public HttpResponseMessage GetReportInFile(string jobNumber)
        {

            var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
            if (!getJobResult.IsSuccess)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var reportPath = getJobResult.Entity.Report;
            // string reportPath = @"c:\hwsig.log";
            var projectName = getJobResult.Entity.ProjectName;

            if (!File.Exists(reportPath))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(reportPath, FileMode.Open);
                bool fullContent = true;
                fileStream.CopyTo(responseStream);
                fileStream.Close();
                responseStream.Position = 0;
                HttpResponseMessage result = new HttpResponseMessage();
                result.StatusCode = fullContent ? HttpStatusCode.OK : HttpStatusCode.PartialContent;
                result.Content = new StreamContent(responseStream);
                //a text file is actually an octet-stream (pdf, etc)
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                //we used attachment to force download
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "JOB" + jobNumber + "_" + projectName + "_Report.txt";
                return result;
            }
            catch (Exception excption)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        [Route("text")]
        [HttpGet]
        public HttpResponseMessage GetReportInText(string jobNumber, bool isSimultaneous = false)
        {
            var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
            if (!getJobResult.IsSuccess)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var reportPath = getJobResult.Entity.Report;
            var projectName = getJobResult.Entity.ProjectName;
            var key = getJobResult.Entity.Key;
            if (isSimultaneous)
            {
                return GetReportSimultaneously(reportPath, key);
            }
            return GetReportAtOnce(reportPath, key);
        }


        private HttpResponseMessage GetReportSimultaneously(string path, Guid key)
        {

            return null;
        }


        private HttpResponseMessage GetReportAtOnce(string reportPath, Guid key)
        {
            if (!File.Exists(reportPath))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(reportPath, FileMode.Open);
                fileStream.CopyTo(responseStream);
                fileStream.Close();
                responseStream.Position = 0;

                string contentStr = new StreamContent(responseStream).ReadAsStringAsync().Result;
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, new JobReportDto { ReportContent = contentStr, ReportUrl = reportPath, Key = key });
                return result;
            }
            catch (Exception excption)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
