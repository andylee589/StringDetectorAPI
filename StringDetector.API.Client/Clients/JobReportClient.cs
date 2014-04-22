using StringDetector.API.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client;

namespace StringDetector.API.Client.Clients
{
    public class JobReportClient : HttpApiClient<JobReportDto>, IJobReportClient
    {
        private const string BaseUriTemplate = "api/jobs/{jobNumber}/report";
        public JobReportClient(HttpClient httpClient)
            : base(httpClient, MediaTypeFormatterCollection.Instance) {

        }
        public async Task<Model.DataTransferObjects.JobReportDto> GetReportAsyc(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.GetSingleAsync(BaseUriTemplate, parameters);
            var jobReport = await ClientHelper.HandleResponseAsync(responseTask);
            return jobReport;
        }
    }
}
