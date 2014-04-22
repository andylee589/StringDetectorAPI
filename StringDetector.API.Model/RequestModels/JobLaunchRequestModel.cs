using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StringDetector.API.Model.RequestModels
{
    public  class JobLaunchRequestModel
    {
        public JobLaunchRequestModel(string config)
        {
            Configuration = config;
            Period = 4;
        }

        public string Configuration { get; set; }


        [Range(0, 10)]
        public double Period { get; set; }
      
        
    }
}
