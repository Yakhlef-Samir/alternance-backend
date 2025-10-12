using Alternance.Application.DTOs;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Implementation will query from Infrastructure Layer
        await Task.CompletedTask;
        return null;
    }
}
