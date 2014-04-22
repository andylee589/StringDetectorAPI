using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StringDetector.Domain.Entities
{
    public class JobEntity : IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "The Job Number requires  six digit  figure")]
        [StringLength(10)]
        [Index(IsUnique= true)]
        public string JobNumber { get; set; }

        [Required]
        public string SourcePath { get; set; }

        [Required]
        public string Configuration { get; set; }

       
        public string Report { get; set; }
        
        public virtual ICollection<JobStateEntity> JobStates { get; set; }


        public JobEntity()
        {
            JobStates = new HashSet< JobStateEntity>();
        }
    }
}
