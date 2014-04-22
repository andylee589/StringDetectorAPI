using StringDetector.API.Model.DataTransferObjects;
using StringDetector.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StringDetector.API.Model
{
    internal static class JobExtensions 
    {
        internal static JobDto ToJobDto(this JobEntity job)
        {
            if (job == null)
            {
                return new JobDto();
            }

            return new JobDto
            {
                Key = job.Key,
                JobNumber = job.JobNumber,
                ProjectName= job.ProjectName,
                SourcePath = job.SourcePath,
                JobStates = job.JobStates.Select(jobState => jobState.ToJobStateDto()),
                Report = new JobReportDto
                {
                     Key = job.Key,
                      ReportUrl = job.Report
                } ,
                Configuration = new JobConfigurationDto {
                    Key = job.Key,
                    Configuration = job.Configuration
                }
                
            };
        }
 
    }
}
