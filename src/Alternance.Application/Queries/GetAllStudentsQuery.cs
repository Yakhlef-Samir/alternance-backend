using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetAllStudentsQuery : IRequest<List<StudentDto>>;
