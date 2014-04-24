using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Connector
{
    // TJob means a job model in tool provider , at this time the  tool provider is simply a web project without class library , Just copy the model into the library.
    public class TJob  : IDto
    {
        public string JobNumber { get; set; }
        public string AppPath { get; set; }
        public string configuration { get; set; }
        public string reportPath { get; set; }
    }
}
