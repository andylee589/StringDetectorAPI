using StringDetector.API.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client;
using StringDetector.API.Client;

namespace StringDetector.API.Connector
{
    public class TJobClient : HttpApiClient<TJob>, ITJobClient
    {
        private const string BaseUriForJobsTemplate = "jobs";
        private const string BaseUriForJobTemplate = "jobs/{jobNumber}";
        

        public TJobClient(HttpClient httpClient)
            : base(httpClient, MediaTypeFormatterCollection.Instance)
        {
            
        }


        public    HttpApiResponseMessage<TJob> SubmitJob(string jobNumber, string configuration, string reportPath= "empty", string appPath = "C:\\Dev\\String_Detector\\string_detector.bat")
        {
            var parameters = new { JobNumber = jobNumber, AppPath = appPath, configuration = configuration, reportPath = reportPath };
            var responseTask = base.PostAsync(BaseUriForJobsTemplate, parameters);
            var response = responseTask.Result;
            return response;
        }

        public HttpApiResponseMessage<TJob> GetJobStatus(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.GetSingleAsync(BaseUriForJobTemplate,parameters);
            var response = responseTask.Result;
            return response;
        }

        public HttpApiResponseMessage StopJob(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseApiMessage =    base.DeleteAsync(BaseUriForJobTemplate, parameters).Result;
            return responseApiMessage ;
        }
    }
}
