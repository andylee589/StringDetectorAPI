using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Model.RequestModels
{
    public  class JobCreateRequestModel
    {
        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }
        
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "The Job Number requires  six digit  figure")]
        public string JobNumber { get; set; }

        [Required]
        public string SourcePath { get; set; }

        [Required]
        public string Configuration { get; set; }

    }
}
