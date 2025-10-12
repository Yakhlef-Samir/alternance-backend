using Alternance.Infrastructure.MongoDb;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Alternance.Infrastructure.Tools;

public static class IndexManager
{
    public static async Task ShowIndexesAsync()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:MongoDB", "mongodb://localhost:27017/alternance-test-db" }
            })
            .Build();

        var context = new MongoDbContext(configuration);

        // Show indexes for Job collection
        var jobCollection = context.GetCollection<Domain.Entities.Job>("Job");
        var jobIndexes = await jobCollection.Indexes.ListAsync();
        Console.WriteLine("Job collection indexes:");
        await jobIndexes.ForEachAsync(index => Console.WriteLine($"  {index}"));

        // Show indexes for other collections
        var collections = new[] { "Application", "Company", "Student", "User" };

        foreach (var collectionName in collections)
        {
            var collection = context.GetCollection<Domain.Entities.Job>(collectionName); // Using Job as generic type
            var indexes = await collection.Indexes.ListAsync();
            Console.WriteLine($"\n{collectionName} collection indexes:");
            await indexes.ForEachAsync(index => Console.WriteLine($"  {index}"));
        }
    }
}
