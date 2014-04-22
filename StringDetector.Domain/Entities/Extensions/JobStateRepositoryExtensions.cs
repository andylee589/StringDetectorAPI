using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Entities
{
     public static  class JobStateRepositoryExtensions
    {
         public static IQueryable<JobStateEntity> GetAllInOrderByJobKey(this IEntityRepository<JobStateEntity> jobStateRepository, Guid jobKey)
         {
             return jobStateRepository.GetAll().Where(x => x.JobKey == jobKey).OrderBy(x => x.CreatedOn);
         }


         public static JobStateEntity GetLatestStateByJobKey(this IEntityRepository<JobStateEntity> jobStateRepository, Guid jobKey)
         {
             return jobStateRepository.GetAll().Where(x => x.JobKey == jobKey).OrderByDescending(x => x.CreatedOn).First();
         }

         public static IQueryable<JobStateEntity> GetAllInOrderByJobNumber(this IEntityRepository<JobStateEntity> jobStateRepository, string jobNumber)
         {
             return jobStateRepository.AllIncluding(jobState => jobState.Job).Where(x => x.Job.JobNumber == jobNumber).OrderBy(x => x.CreatedOn);
         }

         public static JobStateEntity GetLatestStateByJobNumber(this IEntityRepository<JobStateEntity> jobStateRepository, string jobNumber)
         {
             return jobStateRepository.AllIncluding(jobState => jobState.Job).Where(x => x.Job.JobNumber == jobNumber).OrderBy(x => x.CreatedOn).LastOrDefault();
         }

    }
}
