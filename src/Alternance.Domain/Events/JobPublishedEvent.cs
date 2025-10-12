namespace Alternance.Domain.Events;

public record JobPublishedEvent(
    Guid JobId,
    Guid CompanyId,
    string Title,
    DateTime PublishedAt
);
