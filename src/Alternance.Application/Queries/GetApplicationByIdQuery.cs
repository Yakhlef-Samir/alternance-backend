using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetApplicationByIdQuery(Guid Id) : IRequest<ApplicationDto?>
{
    public Guid ApplicationId { get; } = Id;
}
