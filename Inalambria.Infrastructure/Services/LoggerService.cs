using Inalambria.Core.Interfaces.Infraestructure;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] agurments)
        {
            if (agurments != null)
            {
                var obj = new object[agurments.Length];
                for (var i = 0; i < agurments.Length; i++)
                {
                    obj[i] = new JavaScriptSerializer().Serialize(agurments[i]);
                }
                _logger.LogInformation(message, obj);
            }
        }

        public void LogError(string message, params object[] agurments)
        {
            if (agurments != null)
            {
                var obj = new object[agurments.Length];
                for (var i = 0; i < agurments.Length; i++)
                {
                    obj[i] = new JavaScriptSerializer().Serialize(agurments[i]);
                }
                _logger.LogError(message, obj);
            }
        }

        public void LogWarning(string message, params object[] agurments)
        {
            if (agurments != null)
            {
                var obj = new object[agurments.Length];
                for (var i = 0; i < agurments.Length; i++)
                {
                    obj[i] = new JavaScriptSerializer().Serialize(agurments[i]);
                }
                _logger.LogWarning(message, obj);
            }
        }
    }
}
