using StringDetector.API.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Client.Clients
{
    public interface IJobReportClient
    {
        Task<JobReportDto> GetReportAsyc(string jobNumber);
    }
}
