using System.Net;
using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;

namespace HttpClientBuilder.IntegrationTests.PostRequestTests;

[Collection(Definitions.WebApiCollection)]
public class PostPrefixedUrlTests
{
    private readonly IHttpClient _client;
        
    public PostPrefixedUrlTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetPrefixedClient();
    }

    [Fact]
    public async Task Post_ReturnsOk_WithLeadingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync("/weatherForecast");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Post_ReturnsOk_WithTrailingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync("weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Post_ReturnsOk_WithWrappedSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync("/weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_ReturnsOk_WithNullSlashes()
    {
        // Arrange

        // Act
        var result = await _client.PostAsync(null);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}