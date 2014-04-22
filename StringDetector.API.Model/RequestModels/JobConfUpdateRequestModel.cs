using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StringDetector.API.Model.RequestModels
{
   public  class JobConfUpdateRequestModel
    {
       [Required]
       public string Configuration { get; set; }
    }
}
