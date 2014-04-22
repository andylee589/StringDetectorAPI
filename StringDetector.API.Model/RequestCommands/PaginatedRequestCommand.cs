using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringDetector.API.Model.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;

namespace StringDetector.API.Model.RequestCommands
{
    public  class PaginatedRequestCommand : IRequestCommand
    {
        [Required]
        [Minimum(1)]
        public int Page { get; set; }

        [Required]
        [Minimum(1)]
        [Maximum(50)]
        public int Take { get; set; }
        public PaginatedRequestCommand(){}
        public PaginatedRequestCommand(int page, int take){
            Page = page;
            Take = take;
        }

       
    }

    
}
