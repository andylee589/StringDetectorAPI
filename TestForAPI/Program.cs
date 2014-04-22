using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.API.Client;
using StringDetector.API.Client.Clients;
using StringDetector.API.Model.DataTransferObjects;
using WebApiDoodle.Net.Http.Client.Model;
using TestForAPI.TestCase;

namespace TestForAPI
{
   public  static class Program
    {
       public  static void Main(string[] args)
        {
            ApiClientContext clientContext = ApiClientContext.Create("http://localhost:59503/");

            Program.TestJobClient(clientContext);


            Console.Write("ok");
             
        }

       public static void TestJobClient(ApiClientContext clientContext)
       {
           IJobClient jobClient = clientContext.GetJobClient();
           JobClientTestCase testCase = new JobClientTestCase(jobClient);
           //testCase.TestGetJobs();
           //testCase.TestGetJobsWithPage();
           //testCase.TestPostJobs();
           //testCase.TestGetJob();
           testCase.TestPostJob();
       }


    }
}
