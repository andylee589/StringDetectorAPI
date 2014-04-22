using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Entities
{
    public enum JobStatusEnum
    {
        JOB_CRATED =1,
        BEGIN_LAUNCH = 2,
        RUNNING = 3,
        TERMINATED = 4,
        ENDS_WITH_SUCCESS =5,
        ENDS_WITH_FAILURE =6,
    }
}
