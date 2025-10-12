namespace Alternance.Domain.Events;

public record ApplicationSubmittedEvent(
    Guid ApplicationId,
    Guid StudentId,
    Guid JobId,
    DateTime SubmittedAt
);
