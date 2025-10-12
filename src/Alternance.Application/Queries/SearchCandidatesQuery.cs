using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record SearchCandidatesQuery(
    string? Skills = null,
    string? Location = null,
    int? ExperienceYears = null
) : IRequest<List<StudentDto>>;
