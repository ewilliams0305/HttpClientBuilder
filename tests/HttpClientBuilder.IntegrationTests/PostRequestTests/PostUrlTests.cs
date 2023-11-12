using System.Net;
using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;

namespace HttpClientBuilder.IntegrationTests.PostRequestTests;

[Collection(Definitions.WebApiCollection)]
public class PostUrlTests
{
    private readonly IHttpClient _client;
        
    public PostUrlTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetDefaultClient();
    }

    [Fact]
    public async Task Post_ReturnsOk_WithLeadingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync("/api/weatherForecast");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Post_ReturnsOk_WithTrailingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync("api/weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Post_ReturnsOk_WithWrappedSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync("/api/weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_ReturnsOk_WithNullSlashes()
    {
        // Arrange

        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = await _client.PostAsync(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}