namespace Alternance.Application.DTOs;

public record StudentDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    List<string> Skills,
    string? Location,
    int? ExperienceYears
);
