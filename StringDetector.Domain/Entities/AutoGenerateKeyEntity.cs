using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Entities
{
    public class AutoGenerateKeyEntity : IEntity
    {
        [Key]
        public Guid Key {get;set;}
        [Required]
        public int NextKey { get; set; }
    }
}
