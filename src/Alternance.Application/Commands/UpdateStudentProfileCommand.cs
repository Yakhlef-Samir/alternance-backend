using MediatR;

namespace Alternance.Application.Commands;

public record UpdateStudentProfileCommand(
    Guid StudentId,
    string? Phone,
    string? Bio,
    string? Location,
    string? ResumeUrl,
    List<string> Skills,
    int? ExperienceYears
) : IRequest<bool>;
