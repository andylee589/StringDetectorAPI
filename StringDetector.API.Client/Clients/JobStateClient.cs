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
    public class JobStateClient : HttpApiClient<JobStateDto>, IJobStateClient
    {
        private const string BaseUriTemplate = "api/jobs{jobNumber}/states";
        private const string BaseUriTemplateForSingle = "api/jobs/{jobNumber}/state";
        public JobStateClient(HttpClient httpClient)
            : base(httpClient, MediaTypeFormatterCollection.Instance) {

        }

        public async Task<WebApiDoodle.Net.Http.Client.Model.PaginatedDto<Model.DataTransferObjects.JobStateDto>> GetStatesAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.GetAsync(BaseUriTemplate, parameters);
            var jobStates = await ClientHelper.HandleResponseAsync(responseTask);
            return jobStates;
        }

        public async Task<Model.DataTransferObjects.JobStateDto> GetStateAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.GetSingleAsync(BaseUriTemplateForSingle, parameters);
            var jobState = await ClientHelper.HandleResponseAsync(responseTask);
            return jobState;
        }
    }
}
