namespace Alternance.Application.DTOs;

public record ApplicationDto(
    Guid Id,
    Guid StudentId,
    Guid JobId,
    string Status,
    string CoverLetter,
    string ResumeUrl,
    DateTime AppliedAt
);
