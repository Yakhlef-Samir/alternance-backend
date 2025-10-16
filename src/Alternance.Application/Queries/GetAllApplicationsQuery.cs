using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetAllApplicationsQuery : IRequest<List<ApplicationDto>>;
