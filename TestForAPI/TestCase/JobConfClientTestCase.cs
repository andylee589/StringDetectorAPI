using StringDetector.API.Client.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForAPI.TestCase
{
    class JobConfClientTestCase
    {
         private IJobConfigurationClient _client;

         JobConfClientTestCase(IJobConfigurationClient client)
        {
            _client = client;
        }
    }
}
