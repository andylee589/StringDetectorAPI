using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StringDetector.API.Model.RequestModels
{
    public class JobUpdateRequestModel
    {
        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        public string SourcePath { get; set; }
    }
}
