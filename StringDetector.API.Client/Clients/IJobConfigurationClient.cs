using StringDetector.API.Model.DataTransferObjects;
using StringDetector.API.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Client.Clients
{
    public interface IJobConfigurationClient
    {
        Task<JobConfigurationDto> GetConfigurationAsync(string jobNumber);
        Task<JobConfigurationDto> PutConfigurationAsync(string jobNumber, JobConfUpdateRequestModel requestModel);
    }
}
