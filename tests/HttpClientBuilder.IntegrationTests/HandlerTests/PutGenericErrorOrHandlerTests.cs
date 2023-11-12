using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

[Collection(Definitions.WebApiCollection)]
public class PutGenericErrorOrHandlerTests
{
    private readonly IHttpClient _client;
    public PutGenericErrorOrHandlerTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetPrefixedClient();
    }

    [Theory]
    [InlineData(200)]
    public async Task Handler_CreatesSuccessObject_WhenRequestIsSuccessful(int code)
    {
        // Arrange
        var handler = new TestingErrorOrHandler();
        var dispatch = _client.CreatePutHandler<WeatherForecast, ErrorResponse>($"/weather/error?error=false", handler);

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
        var dispatch = _client.CreatePutHandler<WeatherForecast, ErrorResponse>($"/weather/error?error=true", handler);

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
        var dispatch = _client.CreatePutHandler<WeatherForecast, ErrorResponse>($"/weather/exception", handler);

        // Act
        await dispatch.DispatchAsync();

        // Assert
        handler.Should().NotBeNull();
        handler.Failed.Should().BeTrue();
    }
  
}