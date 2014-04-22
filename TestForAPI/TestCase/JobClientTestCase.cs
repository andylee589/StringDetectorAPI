using StringDetector.API.Client.Clients;
using StringDetector.API.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;
using StringDetector.API.Model.RequestModels;
using System.Net.Http;

namespace TestForAPI.TestCase
{
    class JobClientTestCase
    {
        private IJobClient _client;

        public JobClientTestCase(IJobClient client)
        {
            _client = client;
        }
        
        public void TestGetJobs(){
          PaginatedDto<JobDto> jobs=  _client.GetJobsAsync().Result;
          Console.WriteLine("Test for job controller with GetJobs() action:");
          Console.WriteLine("------------------------------------------------------------------------------");
          Console.WriteLine("The jobs  info:");
          Console.WriteLine("Total Count:{0}\t Page Size:{1}\t Page Index{2} ", jobs.TotalCount,jobs.PageSize,jobs.PageIndex);
          Console.WriteLine("Job Details :");
            foreach ( JobDto job in jobs.Items){
                Console.WriteLine("Job Number:{0}\t Job Project Name:{1}\t Job Source Path:{2}",job.JobNumber,job.ProjectName,job.SourcePath);
                Console.WriteLine("Job Configuration:\n{0}", job.Configuration.Configuration);
                Console.WriteLine("Job report:\n{0}", job.Report.ReportUrl);
                foreach (JobStateDto state in job.JobStates)
                {
                    Console.WriteLine("Job State:\n{0}", state.JobStatus);
                    Console.WriteLine("Job Create On:\n{0}", state.CreatedOn);
                }
            }
          Console.WriteLine("----------------------------------End-----------------------------------------");
        }

        public void TestGetJobsWithPage(){
            PaginatedDto<JobDto> jobs = _client.GetJobsAsync(1,1) .Result;
            Console.WriteLine("Test for job controller with GetJobs(int page, int take) action:");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("The jobs  info:");
            Console.WriteLine("Total Count:{0}\t Page Size:{1}\t Page Index{2} ", jobs.TotalCount, jobs.PageSize, jobs.PageIndex);
            Console.WriteLine("Job Details :");
            foreach (JobDto job in jobs.Items)
            {
                Console.WriteLine("Job Number:{0}\t Job Project Name:{1}\t Job Source Path:{2}", job.JobNumber, job.ProjectName, job.SourcePath);
                Console.WriteLine("Job Configuration:\n{0}", job.Configuration.Configuration);
                Console.WriteLine("Job report:\n{0}", job.Report.ReportUrl);
                foreach (JobStateDto state in job.JobStates)
                {
                    Console.WriteLine("Job State:\n{0}", state.JobStatus);
                    Console.WriteLine("Job Create On:\n{0}", state.CreatedOn);
                }
            }
            Console.WriteLine("----------------------------------End-----------------------------------------");
        }

        public void TestPostJobs()
        {
            JobCreateRequestModel requestModel = new JobCreateRequestModel { JobNumber = "000003", ProjectName = "DessertHouse", SourcePath = "basepath\\Project\\DessertHouse\\src", Configuration = "SOURCE_DIRECTORIES = ['{here}']" };

            HttpResponseMessage response = _client.PostJobsAsync(requestModel).Result;

            Console.WriteLine("Test for job controller with PostJobs(JobCreateRequestModel requestModel) action:  with job number");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("The Response  info:");
            Console.WriteLine("IsSuccessStatusCode :{0}", response.IsSuccessStatusCode);
            Console.WriteLine("Response Headers Location:{0}", response.Headers.Location);

            requestModel.JobNumber = null;
            response = _client.PostJobsAsync(requestModel).Result;
            Console.WriteLine("Test for job controller with PostJobs(JobCreateRequestModel requestModel) action:  without key number");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("The Response  info:");
            Console.WriteLine("IsSuccessStatusCode :{0}", response.IsSuccessStatusCode);
            Console.WriteLine("Response Headers Location:{0}", response.Headers.Location);
            Console.WriteLine("----------------------------------End-----------------------------------------");
        }

        public void TestGetJob()
        {
            JobDto job = _client.GetJobAsync("000001").Result;
            Console.WriteLine("Test for job controller with GetJob(string jobNumber) action:");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("The job  info:");
            Console.WriteLine("Job Number:{0}\t Job Project Name:{1}\t Job Source Path:{2}", job.JobNumber, job.ProjectName, job.SourcePath);
            Console.WriteLine("Job Configuration:\n{0}", job.Configuration.Configuration);
            Console.WriteLine("Job report:\n{0}", job.Report.ReportUrl);
            foreach (JobStateDto state in job.JobStates)
            {
                Console.WriteLine("Job State:\n{0}", state.JobStatus);
                Console.WriteLine("Job Create On:\n{0}", state.CreatedOn);
            }
            Console.WriteLine("----------------------------------End-----------------------------------------");
        }

        public void TestPostJob()
        {
            HttpResponseMessage response = _client.PostJobAsync("000001").Result;

            Console.WriteLine("Test for job controller with PostJob(string jobNumber) action:  with job number");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("The Response  info:");
            Console.WriteLine("IsSuccessStatusCode :{0}", response.IsSuccessStatusCode);
            //Console.WriteLine("Response Headers Location:{0}", response.Headers.Location);
            //JobDto job = _client.GetJobAsync("000001").Result;
            //Console.WriteLine("Test for job controller with GetJob(string jobNumber) action:");
            //Console.WriteLine("------------------------------------------------------------------------------");
            //Console.WriteLine("The job  info:");
            //Console.WriteLine("Job Number:{0}\t Job Project Name:{1}\t Job Source Path:{2}", job.JobNumber, job.ProjectName, job.SourcePath);
            //Console.WriteLine("Job Configuration:\n{0}", job.Configuration.Configuration);
            //Console.WriteLine("Job report:\n{0}", job.Report.ReportUrl);
            //foreach (JobStateDto state in job.JobStates)
            //{
            //    Console.WriteLine("Job State:\n{0}", state.JobStatus);
            //    Console.WriteLine("Job Create On:\n{0}", state.CreatedOn);
            //}
           
            Console.WriteLine("----------------------------------End-----------------------------------------");
        }

        public void TestPostJobWithModel()
        {

        }

        public void TestPubJobProjectName()
        {

        }

        public void TestPubJobSourcePath()
        {

        }

        public void TestPutJobConstant()
        {

        }

    }
}
