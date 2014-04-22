using StringDetector.API.Model.DataTransferObjects;
using StringDetector.API.Model.RequestCommands;
using StringDetector.API.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Client.Clients
{
public interface  IJobClient
{

    Task<PaginatedDto<JobDto>> GetJobsAsync(/*PaginatedRequestCommand cmd*/ int page,int take);
    Task<PaginatedDto<JobDto>> GetJobsAsync();
    Task<HttpResponseMessage> PostJobsAsync(JobCreateRequestModel requestModel);
    Task<JobDto> GetJobAsync(string jobNumber);
    Task<HttpResponseMessage> PostJobAsync(string  jobNumber);
    Task<HttpResponseMessage> PostJobAsync(string jobNumber, /*JobLaunchRequestModel requestModel*/ string configuration);
    Task<JobDto> PutJobProjectNameAsync(string jobNumber, string projectName);
    Task<JobDto> PutJobSourcePathAsync(string jobNumber, string sourcePath);
    Task<JobDto> PutJobAsync(string jobNumber, JobUpdateRequestModel requestModel);
}
}
