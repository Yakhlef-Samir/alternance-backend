using MediatR;

namespace Alternance.Application.Commands;

public record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string UserType
) : IRequest<Guid>;
