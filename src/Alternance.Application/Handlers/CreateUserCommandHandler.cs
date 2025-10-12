using Alternance.Application.Commands;
using MediatR;

namespace Alternance.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Implementation will use Domain Layer and Infrastructure Layer
        await Task.CompletedTask;
        return Guid.NewGuid();
    }
}
