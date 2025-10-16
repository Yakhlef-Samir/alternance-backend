using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alternance.Application.Commands;
using Alternance.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alternance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        public StudentController(IMediator mediator) : base(mediator){}

        //** Create a new student
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var studentId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetStudentById), new { id = studentId }, studentId);
        }

        //** Get all students
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStudents()
        {
            var query = new GetAllStudentsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        //** Get student by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = await Mediator.Send(query);
            return HandleResult(result);
        }

        //** Update student profile
        [HttpPut("{id}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateStudentProfileRequest request)
        {
            var command = new UpdateStudentProfileCommand(
                id,
                request.Phone,
                request.Bio,
                request.Location,
                request.ResumeUrl,
                request.Skills,
                request.ExperienceYears
            );

            var result = await Mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new { message = "Student profile updated successfully" });
        }
    }

    public record UpdateStudentProfileRequest(
        string? Phone,
        string? Bio,
        string? Location,
        string? ResumeUrl,
        List<string> Skills,
        int? ExperienceYears
    );
}