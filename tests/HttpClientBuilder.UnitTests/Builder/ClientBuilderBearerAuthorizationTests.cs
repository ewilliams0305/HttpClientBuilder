using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace HttpClientBuilder.UnitTests.Builder;

public class ClientBuilderBearerAuthorizationTests
{
    [Theory]
    [InlineData(
        "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2OTkzNzcxNzcsImV4cCI6MTczMDkxMzE3NywiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.nfZpo2tfGHdGabrDe0P1cvFNXX9gl_8pXPjLVbPIAZw")]
    [InlineData(
        "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2OTkzNzcxNzcsImV4cCI6MTczMDkxMzE3NywiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkVyaWMiLCJTdXJuYW1lIjoiV2lsbGlhbXMiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.lE3nfLpIuCXFOlZ0zYA7XZBeG25q3b3p1SayqcZ5W4E")]
    public void CreateClient_WithBasicAuth_IncludesBasicHeader(string token)
    {
        // Arrange
        var client = ClientBuilder
            .CreateBuilder()
            .WithHost("127.0.0.1")
            .WithBearerToken(token)
            .BuildClient();

        // Act

        // Assert
        client.AuthenticationHeader.Scheme.Should().Be("Bearer");
        client.AuthenticationHeader.Parameter.Should().Be(token);
    }
}