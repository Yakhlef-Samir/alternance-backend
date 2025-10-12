using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetApplicationsQuery(
    Guid? StudentId = null,
    Guid? CompanyId = null,
    string? Status = null
) : IRequest<List<ApplicationDto>>;
