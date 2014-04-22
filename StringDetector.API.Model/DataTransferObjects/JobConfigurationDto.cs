using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Model.DataTransferObjects
{
    public class JobConfigurationDto : IDto
    {
        public Guid Key { get; set; }
        public string Configuration { get; set; }
    }
}
