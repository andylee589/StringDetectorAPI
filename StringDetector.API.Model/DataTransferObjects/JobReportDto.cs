using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;
using System.IO;

namespace StringDetector.API.Model.DataTransferObjects
{
   public  class JobReportDto : IDto
    {
       public Guid Key { get; set; }
       //public FileStream ReportStream { get; set; }
       public string ReportUrl { get; set; }
       public string ReportContent { get; set; }
    }
}
