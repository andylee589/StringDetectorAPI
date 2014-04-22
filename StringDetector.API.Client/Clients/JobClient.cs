using StringDetector.API.Model.DataTransferObjects;
using StringDetector.API.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client;

namespace StringDetector.API.Client.Clients
{
   public  class JobClient :HttpApiClient<JobDto>,IJobClient
    {
       private const string BaseUriTemplate = "api/jobs";
       private const string BaseUriTemplateForSingle = "api/jobs/{jobNumber}";
       //private readonly string _affiliateKey;
       public JobClient(HttpClient httpClient)
            : base(httpClient, MediaTypeFormatterCollection.Instance) {

            //if (string.IsNullOrEmpty(affiliateKey)) {

            //    throw new ArgumentException("The argument 'affiliateKey' is null or empty.", "affiliateKey");
            //}

           // _affiliateKey = affiliateKey;
        }
        public async Task<WebApiDoodle.Net.Http.Client.Model.PaginatedDto<Model.DataTransferObjects.JobDto>> GetJobsAsync(/*Model.RequestCommands.PaginatedRequestCommand cmd*/ int page,int take)
        {
            //var parameters = new {  page = cmd.Page, take = cmd.Take };
            var parameters = new { page = page, take = take };
            var responseTask = base.GetAsync(BaseUriTemplate, parameters);
            var jobs = await ClientHelper.HandleResponseAsync(responseTask);
            
            return jobs;
        }

        public async Task<WebApiDoodle.Net.Http.Client.Model.PaginatedDto<Model.DataTransferObjects.JobDto>> GetJobsAsync()
        {
            var responseTask = base.GetAsync(BaseUriTemplate);
            var jobs = await ClientHelper.HandleResponseAsync(responseTask);
            return jobs;
        }

        public async Task<System.Net.Http.HttpResponseMessage> PostJobsAsync(Model.RequestModels.JobCreateRequestModel requestModel)
        {
            var responseTask = base.PostAsync(BaseUriTemplate, requestModel);
            var responseMessage = await ClientHelper.HandleResponseMessageAsync(responseTask);
            return responseMessage;
        }

        public async Task<Model.DataTransferObjects.JobDto> GetJobAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber};
            var responseTask = base.GetSingleAsync(BaseUriTemplateForSingle, parameters);
            var job = await ClientHelper.HandleResponseAsync(responseTask);
            return job;
        }
        public async Task<HttpResponseMessage> PostJobAsync(string jobNumber)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.PostAsync(BaseUriTemplateForSingle, parameters);
            var responseMessage = await ClientHelper.HandleResponseMessageAsync(responseTask);
            return responseMessage;
        }
        public async Task<System.Net.Http.HttpResponseMessage> PostJobAsync(string jobNumber, Model.RequestModels.JobLaunchRequestModel requestModel  /*string configuration*/)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.PostAsync(BaseUriTemplateForSingle, requestModel, parameters);
            var responseMessage = await ClientHelper.HandleResponseMessageAsync(responseTask);
            return responseMessage;
        }

        public async Task<Model.DataTransferObjects.JobDto> PutJobProjectNameAsync(string jobNumber, string projectName)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.PutAsync(BaseUriTemplateForSingle, projectName, parameters);
            var job = await ClientHelper.HandleResponseAsync(responseTask);
            return job;
        }

        public async Task<Model.DataTransferObjects.JobDto> PutJobSourcePathAsync(string jobNumber, string sourcePath)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.PutAsync(BaseUriTemplateForSingle, sourcePath, parameters);
            var job = await ClientHelper.HandleResponseAsync(responseTask);
            return job;
        }

        public async Task<Model.DataTransferObjects.JobDto> PutJobAsync(string jobNumber, Model.RequestModels.JobUpdateRequestModel requestModel)
        {
            var parameters = new { jobNumber = jobNumber };
            var responseTask = base.PutAsync(BaseUriTemplateForSingle, requestModel, parameters);
            var job = await ClientHelper.HandleResponseAsync(responseTask);
            return job;
        }








        public Task<HttpResponseMessage> PostJobAsync(string jobNumber, string configuration)
        {
            throw new NotImplementedException();
        }
    }
}
