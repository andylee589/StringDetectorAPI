using StringDetector.API.Client.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForAPI.TestCase
{
    class JobReportClientTestCase
    {
        private IJobReportClient _client;

        JobReportClientTestCase(IJobReportClient client)
        {
            _client = client;
            
        }
    }
}
