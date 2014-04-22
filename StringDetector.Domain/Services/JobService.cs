using StringDetector.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Services
{
    public class JobService : IJobService
    {
        private readonly IEntityRepository<JobEntity> _JobRepository;
        private readonly IEntityRepository<JobStateEntity> _JobStateRepository;

        public JobService(IEntityRepository<JobEntity> jobRepository, IEntityRepository<JobStateEntity> jobStateRepository)
        {
            _JobRepository = jobRepository;
            _JobStateRepository = jobStateRepository;
        }



        public PaginatedList<JobEntity> GetJobs(int pageIndex, int pageSize)
        {
            var jobs = GetInitialJobs().ToPaginatedList(pageIndex, pageSize);
            return jobs;
        }

        public PaginatedList<JobEntity> GetJobs()
        {
            var query = GetInitialJobs();
            var jobs = query.ToPaginatedList(1, query.Count());
            return jobs;
        }

        public OperationResult<JobEntity> AddJob(JobEntity job)
        {
            var createdJob = InsertJob(job);
            if (createdJob != null)
            {
                return new OperationResult<JobEntity>(true) { Entity = createdJob };
            }
            return new OperationResult<JobEntity>(false);
        }


        public OperationResult<JobEntity> GetJobByJobNumber(string jobNumber)
        {
            var job = _JobRepository.GetJobByJobNumber(jobNumber);
            if (job != null)
            {
                return new OperationResult<JobEntity>(true) { Entity = job };
            }
            return new OperationResult<JobEntity>(false);
        }

        public OperationResult<JobEntity> UpdateProjectNameByJobNumber(string jobNumber, string projectName)
        {
            
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var job = operationResult.Entity;
                job.ProjectName = projectName;
                _JobRepository.Edit(job);
                _JobRepository.Save();

                var updatedJob = GetJob(job.Key);
                return new OperationResult<JobEntity>(true){Entity = updatedJob};
            }
            
            return new OperationResult<JobEntity>(false);
        }

        public OperationResult<JobEntity> UpdateSourcePathByJobNumber(string jobNumber, string sourcePath)
        {
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var job = operationResult.Entity;
                job.SourcePath = sourcePath;
                _JobRepository.Edit(job);
                _JobRepository.Save();

                var updatedJob = GetJob(job.Key);
                return new OperationResult<JobEntity>(true) { Entity = updatedJob };
            }

            return new OperationResult<JobEntity>(false);
        }

        public OperationResult<JobEntity> UpdateJobConstantByJobNumber(string jobNumber, string projectName, string sourcePath)
        {
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var job = operationResult.Entity;
                job.SourcePath = sourcePath;
                job.ProjectName = projectName;
                _JobRepository.Edit(job);
                _JobRepository.Save();

                var updatedJob = GetJob(job.Key);
                return new OperationResult<JobEntity>(true) { Entity = updatedJob };
            }

            return new OperationResult<JobEntity>(false);
        }


        public OperationResult<JobEntity> UpdateJobByJobNumber(string jobNumber, JobEntity jobEntity)
        {
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var job = operationResult.Entity;
                job = jobEntity;
                _JobRepository.Edit(job);
                _JobRepository.Save();

                var updatedJob = GetJob(job.Key);
                return new OperationResult<JobEntity>(true) { Entity = updatedJob };
            }

            return new OperationResult<JobEntity>(false);
        }

        public OperationResult<JobStateEntity> AddJobState(string jobNumber, JobStatusEnum jobStatus)
        {
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var jobState = InsertJobState(jobNumber, jobStatus);
               
                    return new OperationResult<JobStateEntity>(true)
                    {
                        Entity = jobState
                    };
            }
            return new OperationResult<JobStateEntity>(false);
        }


        public OperationResult<JobStateEntity> LaunchJob(string jobNumber)
        {
             

            var operationResult = AddJobState(jobNumber, JobStatusEnum.BEGIN_LAUNCH);
            //  waiting for  job running state

            return operationResult;
        }

        public OperationResult<String> GetJobConfiguration(string jobNumber)
        {
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var job = operationResult.Entity;
                return new OperationResult<string>(true) { Entity = job.Configuration };
            }
            return new OperationResult<string>(false) ;
            
        }

        public OperationResult<JobEntity>  UpdateJobConfiguration(string jobNumber, string configuration)
        {
            var operationResult = GetJobByJobNumber(jobNumber);
            if (operationResult.IsSuccess)
            {
                var job = operationResult.Entity;
                job.Configuration = configuration;
                _JobRepository.Edit(job);
                _JobRepository.Save();

                var updatedJob = GetJob(job.Key);
                return new OperationResult<JobEntity>(true) { Entity = updatedJob };
            }
            return new OperationResult<JobEntity>(false);
           
        }

        public OperationResult<String> GetJobReportPath(string jobNumber)
        {
             var operationResult = GetJobByJobNumber(jobNumber);
             if (operationResult.IsSuccess)
             {
                 var job = operationResult.Entity;
                 return new OperationResult<string>(true) { Entity = job.Report };
             }
             return new OperationResult<string>(false);
        }

        public bool isJobExsistsByJobNumber(string jobNumber)
        {
            return GetJobByJobNumber(jobNumber).IsSuccess;
        }

        //public FileStream GetJobReport(string jobNumber)
        //{
        //    var operationResult = GetJobReportPath(jobNumber);
        //    if (operationResult.IsSuccess)
        //    {
        //        string reportPath = operationResult.Entity;
        //        FileStream outStream = File.Open(reportPath, FileMode.Open);
        //    }
           
        //    return null;
        //}

        private IQueryable<JobEntity> GetInitialJobs()
        {
            return _JobRepository.AllIncluding(x => x.JobStates).OrderBy(x =>x.JobNumber);
        }

        private JobEntity GetJob(Guid key)
        {
            return _JobRepository.GetSingle(key);
        }

        private JobEntity InsertJob(JobEntity job)
        {
            job.Key = Guid.NewGuid();
            try {
                _JobRepository.Add(job);
                _JobRepository.Save();
                _JobStateRepository.GetAll();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            

            // add the first state for this job
            var jobState = AddJobState(job.JobNumber, JobStatusEnum.JOB_CRATED).Entity;
            job.JobStates = new List<JobStateEntity> { jobState };
            return job;
        }

        private JobStateEntity InsertJobState(string jobNumber, JobStatusEnum jobStatus)
        {
            var job = GetJobByJobNumber(jobNumber).Entity;
            var jobState = new JobStateEntity
            {
                Key = Guid.NewGuid(),
                JobKey = job.Key,
                JobStatus = jobStatus,
                CreatedOn = DateTime.Now,
            };
            _JobStateRepository.Add(jobState);
            _JobStateRepository.Save();
            return jobState;
        }




    }
}
