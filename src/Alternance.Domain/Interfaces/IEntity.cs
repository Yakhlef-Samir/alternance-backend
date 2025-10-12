using MongoDB.Bson.Serialization.Attributes;

namespace Alternance.Domain.Interfaces;

public interface IEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
}
