using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

[Collection(Definitions.WebApiCollection)]
public class PutGenericHandlerTests
{
    private readonly IHttpClient _client;
    public PutGenericHandlerTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetPrefixedClient();
    }

    [Fact]
    public async Task Handler_CreatesSuccessObject_WhenRequestIsSuccessful()
    {
        // Arrange
        var handler = new TestingGenericHandler();
        var dispatch = _client.CreatePutHandler<WeatherForecast>("/", handler);

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
        var dispatch = _client.CreatePutHandler<WeatherForecast>("/api/weather/exception", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.Failed.Should().BeTrue();
    }
}