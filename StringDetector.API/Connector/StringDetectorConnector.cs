using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.Domain.Services;
using System.Net.Http;
using StringDetector.API.Client;

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



        public OperationResult<String> LaunchJob(string jobNumber, string configurationDirectory)
        {
           HttpResponseMessage response =  _tjobClient.SubmitJobAsync(jobNumber, configurationDirectory).Result;
           string content =   response.Content.ReadAsStringAsync().Result;
           return new OperationResult<String>(response.IsSuccessStatusCode) { Entity = content };
        }

        public OperationResult StopJob(string jobNumber)
        {
            HttpResponseMessage response = _tjobClient.StopJobAsync(jobNumber).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return new OperationResult<String>(response.IsSuccessStatusCode) { Entity = content };
        }

        public OperationResult<String> CheckIsJobRunning(string jobNumber)
        {
            HttpResponseMessage response = _tjobClient.GetJobStatusAsync(jobNumber).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return new OperationResult<String>(response.IsSuccessStatusCode) { Entity = content };
        }

        public OperationResult CheckIsReadyForLaunch(string sourcePath)
        {

            return new OperationResult<string>(true);
        }
    }
}
