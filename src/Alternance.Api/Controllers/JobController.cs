using Alternance.Application.Commands;
using Alternance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alternance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : BaseController
{
    public JobController(IMediator mediator) : base(mediator) { }

    //** Create a new job
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var jobId = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetJobById), new { id = jobId }, jobId);
    }

    //** Get all jobs
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllJobs()
    {
        var query = new GetAllJobsQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    //** Get job by ID
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetJobById(Guid id)
    {
        var query = new GetJobByIdQuery(id);
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }
}
