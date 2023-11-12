using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

[Collection(Definitions.WebApiCollection)]
public class PostGenericHandlerTests
{
    private readonly IHttpClient _client;
    public PostGenericHandlerTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetPrefixedClient();
    }

    [Fact]
    public async Task Handler_CreatesSuccessObject_WhenRequestIsSuccessful()
    {
        // Arrange
        var handler = new TestingGenericHandler();
        var dispatch = _client.CreatePostHandler<WeatherForecast>("/", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.SuccessBody.Should().BeTrue();
    }
        
    [Fact]
    public async Task Handler_CreatesException_WhenRequestFails()
    {
        // Arrange
        var handler = new TestingGenericHandler();
        var dispatch = _client.CreatePostHandler<WeatherForecast>("/api/weather/exception", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.Failed.Should().BeTrue();
    }
}