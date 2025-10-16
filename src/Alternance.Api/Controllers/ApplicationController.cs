using Alternance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alternance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : BaseController
{
    public ApplicationController(IMediator mediator) : base(mediator) { }

    //** Get all applications
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllApplications()
    {
        var query = new GetAllApplicationsQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    //** Get application by ID
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetApplicationById(Guid id)
    {
        var query = new GetApplicationByIdQuery(id);
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }
}
