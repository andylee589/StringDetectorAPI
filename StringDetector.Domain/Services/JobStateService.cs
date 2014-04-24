using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.Domain.Entities;

namespace StringDetector.Domain.Services
{
   public  class JobStateService : IJobStateService
    {
       private readonly IEntityRepository<JobStateEntity> _jobStateRepository;
       private readonly IEntityRepository<JobEntity> _jobRepository;

       public JobStateService(IEntityRepository<JobStateEntity> jobStateRepository, IEntityRepository<JobEntity> jobRepository)
       {
           _jobStateRepository = jobStateRepository;
           _jobRepository = jobRepository;
       }

       private OperationResult<JobEntity> GetJobByJobNumber(string jobNumber)
       {
           var job = _jobRepository.GetJobByJobNumber(jobNumber);
           if (job != null)
           {
               return new OperationResult<JobEntity>(true) { Entity = job };
           }
           return new OperationResult<JobEntity>(false);
       }

       public OperationResult<PaginatedList<JobStateEntity>> GetAllStatesByJobNumber(string jobNumber)
       {
           var operationResult =  GetJobByJobNumber(jobNumber);
           if (!operationResult.IsSuccess)
           {
               return new OperationResult<PaginatedList< JobStateEntity>>(false);
           }
           var job = operationResult.Entity;
           var jobStates = _jobStateRepository.GetAllInOrderByJobKey(job.Key);

           return new OperationResult<PaginatedList<JobStateEntity>>(true) { Entity = jobStates.ToPaginatedList(1, jobStates.Count()) };
       }

       public OperationResult<JobStateEntity> GetLatestStateByJobNumber(string jobNumber)
       {
           var operationResult = GetJobByJobNumber(jobNumber);
           if (!operationResult.IsSuccess)
           {
               return new OperationResult<JobStateEntity>(false);
           }
           var job = operationResult.Entity;
           var jobState = _jobStateRepository.GetLatestStateByJobKey(job.Key);
           return new OperationResult<JobStateEntity>(true) { Entity = jobState };
       }


       public OperationResult<PaginatedList<JobStateEntity>> GetAllStatesByJobKey(Guid key)
       {
           var jobStates = _jobStateRepository.GetAllInOrderByJobKey(key);
           return new OperationResult<PaginatedList<JobStateEntity>>(true) { Entity = jobStates.ToPaginatedList(1, jobStates.Count()) };
       }

       public OperationResult<JobStateEntity> GetLatestStateByJobKey(Guid key)
       {
           var jobState = _jobStateRepository.GetLatestStateByJobKey(key);
           return new OperationResult<JobStateEntity>(true) { Entity = jobState };
       }


       public OperationResult DeleteJobState(JobStateEntity jobState)
       {
           _jobStateRepository.Delete(jobState);
           _jobStateRepository.Save();
           return new OperationResult (true);
       }
    }
}
