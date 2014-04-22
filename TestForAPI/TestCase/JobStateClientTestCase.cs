using StringDetector.API.Client.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForAPI.TestCase
{
    class JobStateClientTestCase
    {
         private IJobStateClient _client;

         JobStateClientTestCase(IJobStateClient client)
        {
            _client = client;
        }

    }
}
