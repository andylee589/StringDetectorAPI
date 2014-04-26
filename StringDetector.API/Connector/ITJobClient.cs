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
   public interface ITJobClient
    {
       HttpApiResponseMessage<TJob>  SubmitJob(string jobNumber, string configuration, string reportPath = "empty", string appPath = "C:\\Dev\\String_Detector\\string_detector.bat");
       HttpApiResponseMessage<TJob> GetJobStatus(string JobNumber);
       HttpApiResponseMessage StopJob(string JobNumber);
    }
}
