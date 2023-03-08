using Inalambria.Core.DTOs.Request;
using Inalambria.Core.Exceptions;
using Inalambria.Core.Interfaces.Infraestructure;
using Inalambria.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;
        private readonly AuthenticationOptions _options;
        public LoginService(IPasswordService passwordService, ITokenService tokenService, ILoggerService logger, IOptions<AuthenticationOptions> options)
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
            _logger = logger;
            _options = options.Value;
        }
        public async Task<string> Login(UserLoginRequest userLogin)
        {
            _logger.LogInformation("Start in UserService->Login-> {User}", userLogin);
            if(userLogin.Email.ToLower() != _options.UserName.ToLower())
            {
                throw new NotFoundException("User incorrect");
            }
            if (!_passwordService.Check(_options.Password,userLogin.Password))
            {
                throw new NotFoundException("User or password incorrect");
            }

            return _tokenService.GenerateToken(_options.UserName);
        }
    }
}
