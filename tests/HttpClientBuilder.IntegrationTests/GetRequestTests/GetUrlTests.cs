using System.Net;
using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;

namespace HttpClientBuilder.IntegrationTests.GetRequestTests;

[Collection(Definitions.WebApiCollection)]
public class GetPrefixedUrlTests
{
    private readonly IHttpClient _client;
        
    public GetPrefixedUrlTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetPrefixedClient();
    }

    [Fact]
    public async Task Get_ReturnsOk_WithLeadingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("/weatherForecast");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Get_ReturnsOk_WithTrailingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Get_ReturnsOk_WithWrappedSlashes()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("/weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_ReturnsOk_WithNullSlashes()
    {
        // Arrange

        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = await _client.GetAsync(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

[Collection(Definitions.WebApiCollection)]
public class GetUrlTests
{
    private readonly IHttpClient _client;
        
    public GetUrlTests(AppFactory factory)
    {
        // Arrange
        _client = factory.GetDefaultClient();
    }

    [Fact]
    public async Task Get_ReturnsOk_WithLeadingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("/api/weatherForecast");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Get_ReturnsOk_WithTrailingSlashes()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("api/weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Fact]
    public async Task Get_ReturnsOk_WithWrappedSlashes()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("/api/weatherForecast/");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_ReturnsOk_WithNullSlashes()
    {
        // Arrange

        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = await _client.GetAsync(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}