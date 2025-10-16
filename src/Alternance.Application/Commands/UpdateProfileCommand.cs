using MediatR;

namespace Alternance.Application.Commands;

public record UpdateProfileCommand(
    Guid UserId,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Phone,
    string? Bio
) : IRequest<bool>;
