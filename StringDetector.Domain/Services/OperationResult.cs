using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Services
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
