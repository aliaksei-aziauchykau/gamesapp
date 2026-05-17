using GAA.Application.Status;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAA.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IMediator mediator) : ControllerBase
{
    [HttpGet("status")]
    [AllowAnonymous]
    public async Task<ActionResult<StatusDto>> Status(CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetStatusQuery(), cancellationToken);
        return Ok(dto);
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        var name = User.Identity?.Name ?? User.FindFirst("preferred_username")?.Value ?? "unknown";
        return Ok(new { user = name, claims = User.Claims.Select(c => new { c.Type, c.Value }) });
    }
}
