using NSubstitute;

using Kratos.Api.Common.Types;
using Kratos.Api.Features.Testimonials.Add;

namespace Kratos.Api.Tests.UnitTests.Testimonials;

public class Add 
{
    [Fact]
    public async Task AddTwoTestimonialsForUser()
    {
        // Arrange
        Database.Models.Testimonial testimonial = new()
        {
            Text = "Test",
            UserId = 1
        };

        IRepository repo = Substitute.For<IRepository>();
        repo.ExistsForUser(1, CancellationToken.None).Returns(true);

        Service service = new(repo);
        
        // Act
        Result result = await service.AddAsync(testimonial, CancellationToken.None);
        
        // Assert
        Assert.IsType<ConflictError>(result.Error);
    }

    [Fact]
    public async Task AddFirstTestimonialForUser()
    {
        // Arrange
        Database.Models.Testimonial testimonial = new()
        {
            Text = "Test",
            UserId = 1
        };

        IRepository repo = Substitute.For<IRepository>();
        repo.ExistsForUser(1, CancellationToken.None).Returns(false);

        Service service = new(repo);
        
        // Act
        Result result = await service.AddAsync(testimonial, CancellationToken.None);
        
        // Assert
        await repo.Received(1).AddAsync(testimonial, CancellationToken.None);
        Assert.Null(result.Error);
        Assert.True(result.IsSuccess);
    }
}
