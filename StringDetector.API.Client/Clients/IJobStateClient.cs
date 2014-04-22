using StringDetector.API.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Client.Clients
{
    public interface IJobStateClient
    {
        Task<PaginatedDto<JobStateDto>> GetStatesAsync(string jobNumber);
        Task<JobStateDto> GetStateAsync(string jobNumber);
    }
}
