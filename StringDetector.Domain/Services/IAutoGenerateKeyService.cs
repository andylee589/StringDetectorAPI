using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.Domain.Services
{
    public interface IAutoGenerateKeyService
    {
         int getNextKey();
         bool hasNextKey();
    }
}
