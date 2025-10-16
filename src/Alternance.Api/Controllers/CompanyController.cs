using Alternance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alternance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : BaseController
{
    public CompanyController(IMediator mediator) : base(mediator) { }

    //** Get all companies
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCompanies()
    {
        var query = new GetAllCompaniesQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    //** Get company by ID
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyById(Guid id)
    {
        var query = new GetCompanyByIdQuery(id);
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }
}
