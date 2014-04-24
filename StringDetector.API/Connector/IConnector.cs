using StringDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StringDetector.API.Connector
{
   public interface IConnector
    {
          OperationResult<String> LaunchJob (string jobNumber,string configurationDirectory);
          OperationResult StopJob(string jobNumber);
          OperationResult<String> CheckIsJobRunning(string jobNumber);
          OperationResult CheckIsReadyForLaunch(string sourcePath);

    }
}
