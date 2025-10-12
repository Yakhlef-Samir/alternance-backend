using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Conventions;
using Alternance.Domain.Entities;

namespace Alternance.Infrastructure.MongoDb;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private static bool _conventionsRegistered = false;
    private static readonly object _lock = new object();

    public MongoDbContext(IConfiguration configuration)
    {
        // Register conventions ONCE before any MongoDB operations
        RegisterConventions();

        string connectionString = configuration.GetConnectionString("MongoDB") ?? throw new InvalidOperationException("Connection string not found");
        MongoClient client = new(connectionString);
        string databaseName = MongoUrl.Create(connectionString).DatabaseName;
        _database = client.GetDatabase(databaseName);

        // Initialize indexes for all collections
        _ = EnsureIndexesAsync();
    }

    private static void RegisterConventions()
    {
        if (_conventionsRegistered) return;

        lock (_lock)
        {
            if (_conventionsRegistered) return;

            var conventionPack = new MongoDbConventions();
            ConventionRegistry.Register("CustomConventions", conventionPack, t => true);

            _conventionsRegistered = true;
        }
    }

    public IMongoCollection<T> GetCollection<T>(string name) =>
        _database.GetCollection<T>(name);



    // Méthode alternative pour obtenir la collection avec le nom de la classe
    public IMongoCollection<T> GetCollection<T>() =>
        _database.GetCollection<T>(typeof(T).Name);


    public async Task EnsureIndexesAsync()
    {
        try
        {
            // Create indexes for common collections
            //** Job **//
            await CreateIndexForCollection<Domain.Entities.Job>("Id");
            await CreateIndexForCollection<Domain.Entities.Job>("JobId");
            //** Application **//
            await CreateIndexForCollection<Domain.Entities.Application>("Id");
            //** Company **//
            await CreateIndexForCollection<Domain.Entities.Company>("Id");
            //** Student **//
            await CreateIndexForCollection<Domain.Entities.Student>("Id");
            await CreateIndexForCollection<Domain.Entities.Student>("StudentId");
            //** User **//
            await CreateIndexForCollection<Domain.Entities.User>("Id");
            await CreateIndexForCollection<Domain.Entities.User>("UserId");

            // Create composite indexes if needed
            await CreateCompositeIndexForJob();

            // Index pour trier les Jobs par date (plus récent en premier)
            await CreateCreatedAtIndexForJob();
            await CreateCreatedAtIndexForUser();
        }
        catch (Exception ex)
        {
            // Log error but don't fail startup
            Console.WriteLine($"Error creating indexes: {ex.Message}");
        }
    }

    private async Task CreateCompositeIndexForJob()
    {
        string collectionName = "Job";
        IMongoCollection<Job> collection = _database.GetCollection<Job>(collectionName);

        // Create index on CompanyId and Status for common queries
        IndexKeysDefinition<Job>? indexKeys = Builders<Job>.IndexKeys
            .Ascending(j => j.CompanyId)
            .Ascending(j => j.Status);

        CreateIndexModel<Job>? indexModel = new CreateIndexModel<Job>(indexKeys);
        await collection.Indexes.CreateOneAsync(indexModel);
    }

    private async Task CreateCreatedAtIndexForJob()
    {
        string collectionName = "Job";
        IMongoCollection<Job> collection = _database.GetCollection<Job>(collectionName);

        // Index décroissant sur CreatedAt pour trier du plus récent au plus ancien
        IndexKeysDefinition<Job> indexKeys = Builders<Job>.IndexKeys
            .Descending(j => j.CreatedAt);

        CreateIndexModel<Job> indexModel = new CreateIndexModel<Job>(indexKeys);
        await collection.Indexes.CreateOneAsync(indexModel);
    }
    private async Task CreateCreatedAtIndexForUser()
    {
        string collectionName = "User";
        IMongoCollection<User> collection = _database.GetCollection<User>(collectionName);

        // Index décroissant sur CreatedAt pour trier du plus récent au plus ancien
        IndexKeysDefinition<User> indexKeys = Builders<User>.IndexKeys
            .Descending(u => u.CreatedAt);

        CreateIndexModel<User> indexModel = new CreateIndexModel<User>(indexKeys);
        await collection.Indexes.CreateOneAsync(indexModel);
    }

    private async Task CreateIndexForCollection<T>(string fieldName)
    {
        string collectionName = typeof(T).Name;
        IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);

        IndexKeysDefinition<T>? indexKeys = Builders<T>.IndexKeys.Ascending(fieldName);
        CreateIndexModel<T>? indexModel = new CreateIndexModel<T>(indexKeys);

        await collection.Indexes.CreateOneAsync(indexModel);
    }
}
