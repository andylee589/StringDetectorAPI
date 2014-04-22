using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.Domain.Entities;
using System.IO;

namespace StringDetector.Domain.Services
{
    public  interface IJobService 
    {
        // about jobs 
        PaginatedList<JobEntity> GetJobs(int pageIndex, int pageSize);
        PaginatedList<JobEntity> GetJobs();
        OperationResult<JobEntity> AddJob(JobEntity job);
        
        // about job 
        OperationResult< JobEntity> GetJobByJobNumber(String jobNumber);

        OperationResult<JobEntity> UpdateProjectNameByJobNumber(string jobNumber, string projectName);
        OperationResult<JobEntity> UpdateSourcePathByJobNumber(string jobNumber, string sourcePath);
        OperationResult<JobEntity> UpdateJobConstantByJobNumber(string jobNumber, string projectName, string soucePath);
        OperationResult<JobEntity> UpdateJobByJobNumber(string jobNumber, JobEntity jobEntity);
        OperationResult<JobStateEntity> AddJobState(string jobNumber, JobStatusEnum jobStatus);
        OperationResult<JobStateEntity>  LaunchJob(string jobNumber);
        OperationResult<String> GetJobConfiguration(string jobNumber);
        OperationResult<JobEntity> UpdateJobConfiguration(string jobNumber, string configuration);
        OperationResult<String> GetJobReportPath(string jobNumber);
        bool isJobExsistsByJobNumber(string jobNumber);
        //FileStream GetJobReport(string jobNumber);

    }
}
