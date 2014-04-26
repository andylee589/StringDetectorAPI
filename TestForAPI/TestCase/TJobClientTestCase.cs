using StringDetector.API.Connector;
//using StringDetector.API.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestForAPI.TestCase
{
   public  class TJobClientTestCase
    {   private ITJobClient _client;

    public TJobClientTestCase(ITJobClient client)
    {
        _client = client;
    }
        
        public void TestSubmitJOb(){
        //    HttpResponseMessage response = _client.SubmitJobAsync("000001", "C:\\Dev\\String_Detector\\test_data\\src").Result;
        //    string content = response.Content.ReadAsStringAsync().Result;
         //   return new OperationResult<String>(response.IsSuccessStatusCode) { Entity = content };
          //Console.WriteLine("Test for job controller with GetJobs() action:");
          //Console.WriteLine("------------------------------------------------------------------------------");
          //Console.WriteLine("The jobs  info:");
          //Console.WriteLine("Total Count:{0}\t Page Size:{1}\t Page Index{2} ", jobs.TotalCount,jobs.PageSize,jobs.PageIndex);
          //Console.WriteLine("Job Details :");
          //  foreach ( JobDto job in jobs.Items){
          //      Console.WriteLine("Job Number:{0}\t Job Project Name:{1}\t Job Source Path:{2}",job.JobNumber,job.ProjectName,job.SourcePath);
          //      Console.WriteLine("Job Configuration:\n{0}", job.Configuration.Configuration);
          //      Console.WriteLine("Job report:\n{0}", job.Report.ReportUrl);
          //      foreach (JobStateDto state in job.JobStates)
          //      {
          //          Console.WriteLine("Job State:\n{0}", state.JobStatus);
          //          Console.WriteLine("Job Create On:\n{0}", state.CreatedOn);
          //      }
          //  }
          //Console.WriteLine("----------------------------------End-----------------------------------------");
        }
    }
}
