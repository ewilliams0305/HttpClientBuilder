using FluentAssertions;

namespace HttpClientBuilder.UnitTests.Builder;

public class ClientBuilderNoAuthTests
{
    [Fact]
    public void CreateClient_WithoutAuth_DoesNotAddAuth()
    {
        // Arrange
        var client = ClientBuilder
            .CreateBuilder()
            .WithHost("127.0.0.1")
            .BuildClient();

        // Act

        // Assert
        client.AuthenticationHeader.Should().BeNull();
    }
}