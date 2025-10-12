using Alternance.Domain.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Alternance.Domain.Common;

public abstract class BaseEntity : IEntity
{
    [BsonId]
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
