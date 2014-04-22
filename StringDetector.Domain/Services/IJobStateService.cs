using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.Domain.Entities;

namespace StringDetector.Domain.Services
{
    public interface IJobStateService 
    {
        OperationResult<PaginatedList<JobStateEntity>> GetAllStatesByJobNumber(String jobNumber);
        OperationResult<PaginatedList<JobStateEntity>> GetAllStatesByJobKey(Guid key);
        OperationResult<JobStateEntity> GetLatestStateByJobNumber(String jobNumber);
        OperationResult<JobStateEntity> GetLatestStateByJobKey(Guid key); 

    }
}
