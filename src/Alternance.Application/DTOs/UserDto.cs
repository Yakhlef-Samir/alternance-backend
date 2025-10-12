namespace Alternance.Application.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string UserType,
    DateTime CreatedAt
);
