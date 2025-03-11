using NSubstitute;

using Kratos.Api.Common.Types;
using Kratos.Api.Features.Clients.Add;

namespace Kratos.Api.Tests.UnitTests.Clients;

public class Add
{
    [Fact]
    public async Task AddTwoClientsForUser()
    {
        // Arrange
        Database.Models.Client client = new()
        {
            CloudStorageLink = "https://google.com",
            UserId = 1
        };

        IRepository repo = Substitute.For<IRepository>();
        repo.ExistsForUser(1, CancellationToken.None).Returns(true);

        Service service = new(repo);

        // Act
        Result result = await service.AddAsync(client, CancellationToken.None);

        // Assert
        Assert.IsType<ConflictError>(result.Error);
    }

    [Fact]
    public async Task AddFirstClientForUser()
    {
        // Arrange
        Database.Models.Client client = new()
        {
            CloudStorageLink = "https://google.com",
            UserId = 1
        };

        IRepository repo = Substitute.For<IRepository>();
        repo.ExistsForUser(1, CancellationToken.None).Returns(false);

        Service service = new(repo);

        // Act
        Result result = await service.AddAsync(client, CancellationToken.None);

        // Assert
        await repo.Received(1).AddAsync(client, CancellationToken.None);
        Assert.Null(result.Error);
        Assert.True(result.IsSuccess);
    }
}