using GAA.Shared.Modules.Common.Interfaces;
using GAA.Shared.Modules.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GAA.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController(IUtilityService utility) : ControllerBase
{
    [HttpGet("live")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult Live() =>
        Ok(new { status = "live", application = utility.ApplicationName });

    [HttpGet]
    [Route("/health")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult Health() =>
        Ok(new { status = "healthy", application = utility.ApplicationName });
}
