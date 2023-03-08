using Inalambria.Core.Exceptions;
using Inalambria.Core.Interfaces.Infraestructure;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Inalambria.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerService _logger;
        public GlobalExceptionFilter(ILoggerService logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {

                var exception = (BusinessException)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Tittle = "Bad Request",
                    Detail = exception.Message
                };
                _logger.LogError("Error {Exception}: ", validation);
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new NotFoundObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
            else if (context.Exception.GetType() == typeof(NotFoundException))
            {

                var exception = (NotFoundException)context.Exception;
                var validation = new
                {
                    Status = 404,
                    Tittle = "Not Found",
                    Detail = exception.Message
                };
                _logger.LogInformation("Error Not Found: {Exception}: ", validation);
                var json = new
                {
                    errors = new[] { validation }
                };

                context.Result = new NotFoundObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.ExceptionHandled = true;
            }
            else if (context.Exception.GetType() == typeof(UnauthorizedException))
            {

                var exception = (UnauthorizedException)context.Exception;
                var validation = new
                {
                    Status = 401,
                    Tittle = "Unauthorized",
                    Detail = exception.Message
                };
                _logger.LogInformation("Error Unauthorized: {Exception}: ", validation);
                var json = new
                {
                    errors = new[] { validation }
                };

                context.Result = new UnauthorizedObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.ExceptionHandled = true;
            }
        }
    }
}
