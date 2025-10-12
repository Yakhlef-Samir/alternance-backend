namespace Alternance.Domain.Events;

public record UserCreatedEvent(
    Guid UserId,
    string Email,
    string UserType,
    DateTime CreatedAt
);
