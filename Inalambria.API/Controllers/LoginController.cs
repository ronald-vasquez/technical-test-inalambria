using Inalambria.Core.DTOs.Request;
using Inalambria.Core.Interfaces.Infraestructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inalambria.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILoggerService _logger;
        public LoginController(ILoginService loginService, ILoggerService logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(UserLoginRequest userLoginRequest)
        {

            _logger.LogInformation("Start in User->Authenticate with {UserLoginRequestDto}", userLoginRequest);
            string authenticationResponse = await _loginService.Login(userLoginRequest);

            return Ok(authenticationResponse);
        }
    }
}
