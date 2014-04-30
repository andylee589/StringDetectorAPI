using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Model.DataTransferObjects
{
   public  class JobDto : IDto 
    {
       public Guid Key { get; set; }
       public string ProjectName { get; set; }
       public string JobNumber { get; set; }
       public string SourcePath { get; set; }


       public JobConfigurationDto Configuration { get; set; }
       public JobReportDto Report { get; set; }
      // public IEnumerable <JobStateDto> JobStates { get; set; }
       public JobStateDto JobState { get; set; }
    }
}
