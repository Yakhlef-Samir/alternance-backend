using MediatR;

namespace Alternance.Application.Commands;

public record ApplyToJobCommand(
    Guid StudentId,
    Guid JobId,
    string CoverLetter,
    string ResumeUrl
) : IRequest<Guid>;
