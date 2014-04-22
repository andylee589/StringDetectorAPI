using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Model.RequestModels
{
   public  class JobUpdateOptionalModel
    {
       public JobUpdateOptionalModel()
       {
          
       }

       public string IpAdress {get;set;}
       public string Account { get; set; }
       public string Password { get; set; }

    }
}
