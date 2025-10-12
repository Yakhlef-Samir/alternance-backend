using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Alternance.Infrastructure.MongoDb;

public class MongoDbConventions : ConventionPack
{
    public MongoDbConventions()
    {
        // Convention pour mapper Guid Id au _id BSON en string
        Add(new GuidAsStringIdConvention());
        
        // Convention pour tous les autres Guids
        Add(new GuidAsStringConvention());
        
        // Ignore les champs BSON supplémentaires
        Add(new IgnoreExtraElementsConvention(true));
    }
    
    // Convention pour mapper le champ Id de type Guid au _id BSON
    private class GuidAsStringIdConvention : ConventionBase, IClassMapConvention
    {
        public void Apply(BsonClassMap classMap)
        {
            // Cherche la propriété Id de type Guid
            BsonMemberMap? idProperty = classMap.DeclaredMemberMaps
                .FirstOrDefault(m => m.MemberName == "Id" && m.MemberType == typeof(Guid));
            
            if (idProperty is not null)
            {
                // Mappe Id comme identifiant BSON avec sérialisation en string
                classMap.MapIdMember(idProperty.MemberInfo)
                    .SetSerializer(new GuidSerializer(BsonType.String))
                    .SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.GuidGenerator.Instance);
            }
        }
    }
    
    // Convention pour tous les autres champs Guid (non-Id)
    private class GuidAsStringConvention : ConventionBase, IMemberMapConvention
    {
        public void Apply(BsonMemberMap memberMap)
        {
            if ((memberMap.MemberType == typeof(Guid) || memberMap.MemberType == typeof(Guid?)) 
                && memberMap.MemberName != "Id")
            {
                memberMap.SetSerializer(new GuidSerializer(BsonType.String));
            }
        }
    }
}
