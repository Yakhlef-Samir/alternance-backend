namespace Alternance.Application.DTOs;

public record StudentDto(
    Guid Id,
    Guid UserId,
    Guid StudentId,
    string? Phone,
    string? Bio,
    string? ResumeUrl,
    List<string> Skills,
    string? Location,
    int? ExperienceYears
);
