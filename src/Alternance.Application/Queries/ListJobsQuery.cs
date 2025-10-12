using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record ListJobsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? Location = null,
    string? ContractType = null
) : IRequest<List<JobDto>>;
