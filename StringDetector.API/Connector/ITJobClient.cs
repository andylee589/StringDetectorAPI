using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StringDetector.API.Connector
{
   public interface ITJobClient
    {
        Task<HttpResponseMessage> SubmitJobAsync(string JobNumber, string configuration, string reprotPath= "empty",  string AppPath = "C:\\Dev\\String_Detector\\string_detector.bat");
        Task<HttpResponseMessage> GetJobStatusAsync(string JobNumber);
        Task<HttpResponseMessage> StopJobAsync(string JobNumber);
    }
}
