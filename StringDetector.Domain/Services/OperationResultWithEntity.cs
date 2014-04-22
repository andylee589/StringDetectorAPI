using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Services
{
    public class OperationResult<TEntity> : OperationResult
    {
        public TEntity Entity { get; set; }
        public OperationResult(bool isSuccess)
            : base(isSuccess) { }
    }
}
