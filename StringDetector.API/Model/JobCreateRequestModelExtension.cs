using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.API.Model.RequestModels;
using StringDetector.Domain.Entities;

namespace StringDetector.API.Model
{
    internal static class JobCreateRequestModelExtension
    {
        internal static JobEntity ToJob(this JobCreateRequestModel requestModel){
            return new JobEntity{
                 JobNumber= requestModel.JobNumber,
                 ProjectName = requestModel.ProjectName,
                 SourcePath = requestModel.SourcePath,
                 Configuration = requestModel.Configuration
            };
        }
    }
}
