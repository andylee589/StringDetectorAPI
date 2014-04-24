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
        private const string BaseUriForJobTemplate = "jobs/{{jobNumber}}";
        

        public TJobClient(HttpClient httpClient)
            : base(httpClient, MediaTypeFormatterCollection.Instance)
        {
            
        }


        public async Task<System.Net.Http.HttpResponseMessage> SubmitJobAsync(string jobNumber, string configuration, string reportPath, string appPath = "C:\\Dev\\String_Detector\\string_detector.bat")
        {
            var parameters = new { JobNumber = jobNumber, AppPath = appPath, configuration = configuration, reportPath = reportPath };
            var responseTask = base.PostAsync(BaseUriForJobsTemplate, parameters);
            var response = await ClientHelper.HandleResponseMessageAsync(responseTask);
            return response;
        }

        public async  Task<System.Net.Http.HttpResponseMessage> GetJobStatusAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.GetSingleAsync(BaseUriForJobTemplate,parameters);
            var response  = await ClientHelper.HandleResponseMessageAsync(responseTask);
            return response;
        }

        public async Task<System.Net.Http.HttpResponseMessage> StopJobAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseApiMessage =  await  base.DeleteAsync(BaseUriForJobTemplate, parameters);

            return responseApiMessage.Response; ;
        }
    }
}
