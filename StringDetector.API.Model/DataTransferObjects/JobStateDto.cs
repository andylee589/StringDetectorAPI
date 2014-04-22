using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Model.DataTransferObjects
{
    public class JobStateDto : IDto
    {
        public Guid Key { get; set; }
        public Guid JobKey { get; set; }
        public string JobStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
