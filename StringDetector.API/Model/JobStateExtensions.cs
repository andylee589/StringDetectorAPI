using StringDetector.API.Model.DataTransferObjects;
using StringDetector.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Model
{
    internal static class JobStateExtensions
    {
        internal static JobStateDto ToJobStateDto(this JobStateEntity jobState)
        {
            if (jobState == null)
            {
                return new JobStateDto();
            }
            return new JobStateDto
            {
                 Key = jobState.Key,
                 JobKey = jobState.JobKey,
                 JobStatus = jobState.JobStatus.ToString(),
                 CreatedOn = jobState.CreatedOn
            };
        }
    }
}
