using FluentAssertions;
using System.Text;

namespace HttpClientBuilder.UnitTests.Builder;

public class ClientBuilderBasicAuthorizationTests
{
    [Theory]
    [InlineData("user", "password")]
    [InlineData("admin", "admin")]
    [InlineData("guest", "thingy")]
    [InlineData("wazzle", "wizzle")]
    public void CreateClient_WithBasicAuth_IncludesBasicHeader(string user, string password)
    {
        // Arrange
        var client = ClientBuilder
            .CreateBuilder()
            .WithHost("127.0.0.1")
            .WithBasicAuthorization(user, password)
            .BuildClient();

        // Act
        var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + password));

        // Assert
        client.AuthenticationHeader.Scheme.Should().Be("Basic");
        client.AuthenticationHeader.Parameter.Should().Be(encoded);
    } 
}