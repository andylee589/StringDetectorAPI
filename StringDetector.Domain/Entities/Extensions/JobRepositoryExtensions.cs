using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Entities
{
   public static  class JobRepositoryExtensions
    {

       public static IQueryable<JobEntity> GetJobsByJobStaus(this IEntityRepository<JobEntity> jobRepository, JobStatusEnum jobStatus)
       {
           var jobs = from job in jobRepository.AllIncluding(x => x.JobStates)
                      where (job.JobStates.OrderBy(x => x.CreatedOn).Last().JobStatus == jobStatus)
                      select job;
           return jobs;
       }

       public static JobEntity GetJobByJobNumber(this IEntityRepository<JobEntity> jobRepository, String jobNumber)
       {
           var jobs = from job in jobRepository.GetAll() where (job.JobNumber == jobNumber ) select job;
           return jobs.FirstOrDefault();
       }

       //public static IQueryable<JobEntity> GetRunningJobs(this IEntityRepository<JobEntity> jobRepository)
       //{

       //    return GetJobsByJobStaus(jobRepository, JobStatusEnum.RUNNING);
       //}

       //public static IQueryable<JobEntity> GetSuccessedJobs(this IEntityRepository<JobEntity> jobRepository)
       //{

       //    return GetJobsByJobStaus(jobRepository, JobStatusEnum.ENDS_WITH_SUCCESS);
       //}

       //public static IQueryable<JobEntity> GetFailedJobs(this IEntityRepository<JobEntity> jobRepository)
       //{

       //    return GetJobsByJobStaus(jobRepository, JobStatusEnum.ENDS_WITH_FAILURE);
       //}

       //public static IQueryable<JobEntity> GetTerminatedJobs(this IEntityRepository<JobEntity> jobRepository)
       //{

       //    return GetJobsByJobStaus(jobRepository, JobStatusEnum.TERMINATED);
       //}

    }
}
