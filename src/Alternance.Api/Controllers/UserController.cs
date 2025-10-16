using Alternance.Application.Commands;
using Alternance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alternance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Update user profile
    /// </summary>
    [HttpPut("{id}/profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileRequest request)
    {
        UpdateProfileCommand command = new UpdateProfileCommand(
            id,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Phone,
            request.Bio
        );
        
        bool result = await Mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
            
            
        return Ok(new { message = "Profile updated successfully" });
    }
}

public record UpdateProfileRequest(
    string? Email,
    string? FirstName,
    string? LastName,
    string? Phone,
    string? Bio
);
