using Inalambria.Core.DTOs;
using Inalambria.Core.DTOs.Request;
using Inalambria.Core.Interfaces.Infraestructure;
using Inalambria.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy;

namespace Inalambria.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DominoController : ControllerBase
    {
        public readonly IDominoService _dominoService;
        public readonly ILoggerService _loggerService;
        public DominoController(IDominoService dominoService, ILoggerService loggerService)
        {
            _dominoService = dominoService;
            _loggerService = loggerService;
        }
        [HttpPost]
        public async Task<IActionResult> BuildDomino(DominoRequest dominoRequest)
        {
            _loggerService.LogInformation("Start in DominoController->BuildDomino with {dominoRequest}", dominoRequest);
            List<DominoDtos> dominoDtosList = _dominoService.BuildDomino(dominoRequest);
            _loggerService.LogInformation("End in DominoController->BuildDomino Result {dominoDtosList} {dominoRequest}",dominoDtosList, dominoRequest);
            return Ok(dominoDtosList);
        }
    }
}
