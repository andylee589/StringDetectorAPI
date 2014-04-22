using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StringDetector.Domain.Entities
{
    public class JobStateEntity : IEntity
    {
        [Key]
        public Guid Key { get; set; }
      
        public Guid JobKey { get; set; }
        [Required]
        public JobStatusEnum JobStatus { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

       // [Required]
         // [ForeignKey("Job_Key")]
        public  virtual  JobEntity Job { get; set; }
    }
}
