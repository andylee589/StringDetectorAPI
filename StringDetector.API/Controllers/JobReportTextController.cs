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
using StringDetector.API.Model.DataTransferObjects;

namespace StringDetector.API.Controllers
{
    public class JobReportTextController :ApiController
    {
        private readonly IJobService _jobService;
        public JobReportTextController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public HttpResponseMessage GetReportInFile(string jobNumber , bool isSimultaneous = false )
        {
            var getJobResult = _jobService.GetJobByJobNumber(jobNumber);
            if (!getJobResult.IsSuccess)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var reportPath = getJobResult.Entity.Report;
            var projectName = getJobResult.Entity.ProjectName;
            var key = getJobResult.Entity.Key;
            if(isSimultaneous){
                return GetReportSimultaneously(reportPath,key);
            }
            return GetReportAtOnce(reportPath,key);
        }


        private  HttpResponseMessage GetReportSimultaneously(string path,Guid key){

            return null;
        }


        private HttpResponseMessage GetReportAtOnce(string reportPath,Guid key)
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
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK,new JobReportDto{ ReportContent = contentStr, ReportUrl =reportPath , Key = key});
                return result;
            }
            catch (Exception excption)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
