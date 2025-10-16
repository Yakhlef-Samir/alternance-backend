using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetJobByIdQuery(Guid Id) : IRequest<JobDto?>
{
    public Guid JobId { get; } = Id;
}
