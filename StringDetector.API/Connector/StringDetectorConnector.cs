using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.Domain.Services;
using System.Net.Http;
using StringDetector.API.Client;
using WebApiDoodle.Net.Http.Client;

namespace StringDetector.API.Connector
{
    public class StringDetectorConnector  : IConnector
    {
        private readonly ITJobClient _tjobClient;
        private const string BaseAdress = "http://localhost:60626/";
        public StringDetectorConnector()
        {
            ApiClientContext clientContext = ApiClientContext.Create(BaseAdress);
             _tjobClient = clientContext.GetTJobClient();
        }



        public HttpApiResponseMessage<TJob> LaunchJobAsync(string jobNumber, string configurationDirectory)
        {
            HttpApiResponseMessage<TJob> apiResponse = _tjobClient.SubmitJob(jobNumber, configurationDirectory);
            return apiResponse;
        }

        public HttpApiResponseMessage  StopJob(string jobNumber)
        {
            HttpApiResponseMessage response = _tjobClient.StopJob(jobNumber);
            return response;
        }

        public OperationResult<string> CheckIsJobRunning(string jobNumber)
        {
            HttpApiResponseMessage<TJob> apiResponse = _tjobClient.GetJobStatus(jobNumber);
            HttpResponseMessage response = apiResponse.Response;
            TJob job = apiResponse.Model;
            // for the reason that is running return false status and finish return success status code
            bool isRunning = !apiResponse.IsSuccess;
            string info ;
            if(isRunning){
                info ="The job is running in tool";
            }else {
                info = "The job is finised and removed from tool";
            }

            return new OperationResult<string>(isRunning) { Entity = info };
        }

        public OperationResult<string> CheckIsReadyForLaunch(string sourcePath)
        {

            return new OperationResult<string>(true);
        }
    }
}
