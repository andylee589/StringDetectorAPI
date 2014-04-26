using StringDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiDoodle.Net.Http.Client;

namespace StringDetector.API.Connector
{
   public interface IConnector
    {
          HttpApiResponseMessage<TJob> LaunchJobAsync(string jobNumber, string configurationDirectory);
          HttpApiResponseMessage StopJob(string jobNumber);
          OperationResult<string> CheckIsReadyForLaunch(string sourcePath);
          OperationResult<string> CheckIsJobRunning(string jobNumber);

    }
}
