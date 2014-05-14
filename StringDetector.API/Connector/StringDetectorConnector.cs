using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.Domain.Services;
using System.Net.Http;
using StringDetector.API.Client;
using WebApiDoodle.Net.Http.Client;
using System.Net;

namespace StringDetector.API.Connector
{
    public class StringDetectorConnector  : IConnector
    {
        private readonly ITJobClient _tjobClient;
#if DEBUG
        private const string BaseAdress = "http://localhost:60626/";
#else
        private const string BaseAdress = "http://vhwebdevserver.eng.citrite.net/";
#endif
        public StringDetectorConnector()
        {
            ApiClientContext clientContext = ApiClientContext.Create(BaseAdress);
             _tjobClient = clientContext.GetTJobClient();
        }



        public HttpApiResponseMessage<TJob> LaunchJobAsync(string jobNumber, string configuration)
        {
            HttpApiResponseMessage<TJob> apiResponse = _tjobClient.SubmitJob(jobNumber, configuration);
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
            HttpStatusCode retureJobStatus = response.StatusCode;
            TJob job = apiResponse.Model;
            //string info; bool isRunning;
            switch (retureJobStatus)
            {
                
                case HttpStatusCode.OK: //running
                    //info = "The job is running in tool";
                    //isRunning = true;
                    return new OperationResult<string>(true) { Entity = "The job is running in tool" };
                case HttpStatusCode.Accepted: //This means is finished
                case HttpStatusCode.NotFound: //Not existing
                    //info = "Job has finished";
                    //isRunning = false;
                    return new OperationResult<string>(false) { Entity = "The job does not exist anymore" };
                default:
                    return new OperationResult<string>(false) { Entity = "The job does not exist anymore" };
            }
        }

        public OperationResult<string> CheckIsReadyForLaunch(string sourcePath)
        {

            return new OperationResult<string>(true);
        }
    }
}
