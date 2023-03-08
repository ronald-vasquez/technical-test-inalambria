using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Core.Interfaces.Infraestructure
{
    public interface ILoggerService
    {
        void LogInformation(string message, params object[] agurments);
        void LogError(string message, params object[] agurments);
        void LogWarning(string message, params object[] agurments);
    }
}
