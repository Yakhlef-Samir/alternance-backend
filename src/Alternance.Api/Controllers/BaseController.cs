using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alternance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IActionResult HandleResult<T>(T result)
    {
        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
