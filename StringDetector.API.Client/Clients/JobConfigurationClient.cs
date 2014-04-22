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
    public class JobConfigurationClient :  HttpApiClient<JobConfigurationDto>,IJobConfigurationClient
    {
        private const string BaseUriTemplate = "api/jobs/{jobNumber}/configuration";
        public JobConfigurationClient(HttpClient httpClient)
            : base(httpClient, MediaTypeFormatterCollection.Instance) {
        }
        public async Task<Model.DataTransferObjects.JobConfigurationDto> GetConfigurationAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.GetSingleAsync(BaseUriTemplate, parameters);
            var jobConfiguration = await ClientHelper.HandleResponseAsync(responseTask);
            return jobConfiguration;
        }

        public async Task<Model.DataTransferObjects.JobConfigurationDto> PutConfigurationAsync(string jobNumber, Model.RequestModels.JobConfUpdateRequestModel requestModel)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.PutAsync(BaseUriTemplate, parameters);
            var jobConfiguration = await ClientHelper.HandleResponseAsync(responseTask);
            return jobConfiguration;
        }
    }
}
