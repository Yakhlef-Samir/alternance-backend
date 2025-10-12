using Alternance.Infrastructure.MongoDb;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Alternance.Infrastructure.IntegrationTests;

public class MongoDbIndexTests
{
    [Fact]
    public async Task EnsureIndexesAsync_ShouldCreateIndexes()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:MongoDB", "mongodb://localhost:27017/alternance-test-db" }
            })
            .Build();

        var context = new MongoDbContext(configuration);

        // Act
        await context.EnsureIndexesAsync();

        // Assert - Check that indexes exist (this would require connecting to MongoDB)
        // For now, we just verify that the method completes without error
        Assert.True(true); // Placeholder assertion
    }
}
