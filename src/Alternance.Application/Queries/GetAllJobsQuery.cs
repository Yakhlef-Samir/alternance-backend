using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetAllJobsQuery : IRequest<List<JobDto>>;
