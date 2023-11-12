using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

[Collection(Definitions.WebApiCollection)]
public class DeleteGenericErrorOrHandlerTests
{
    private readonly IHttpClient _client;
    public DeleteGenericErrorOrHandlerTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetPrefixedClient();
    }

    [Fact]
    public async Task Handler_CreatesSuccessObject_WhenRequestIsSuccessful()
    {
        // Arrange
        var handler = new TestingErrorOrHandler();
        var dispatch = _client.CreateDeleteHandler<WeatherForecast, ErrorResponse>($"/weather/error?error=false", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.SuccessBody.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handler_CreatesErrorObject_WhenRequestIsErrorCode()
    {
        // Arrange
        var handler = new TestingErrorOrHandler();
        var dispatch = _client.CreateDeleteHandler<WeatherForecast, ErrorResponse>($"/weather/error?error=true", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.ErrorBody.Should().BeTrue();
    }


    [Fact]
    public async Task Handler_CreatesException_WhenThrows()
    {
        // Arrange
        var handler = new TestingErrorOrHandler();
        var dispatch = _client.CreateDeleteHandler<WeatherForecast, ErrorResponse>($"/weather/exception", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.Failed.Should().BeTrue();
    }
  
}