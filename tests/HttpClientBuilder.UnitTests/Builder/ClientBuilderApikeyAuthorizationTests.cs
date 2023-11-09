using FluentAssertions;

namespace HttpClientBuilder.UnitTests.Builder;

public class ClientBuilderApikeyAuthorizationTests
{
    [Theory]
    [InlineData("bfhwjbql")]
    [InlineData("afdgjuhmyurtvwrsv")]
    [InlineData("sdkmw;wlf;welfmm94joli5lknm")]
    [InlineData("1342324536dfgssvfb")]
    public void CreateClient_WithApiKey_IncludesXApiKeyHeader(string key)
    {
        // Arrange
        var client = ClientBuilder
            .CreateBuilder()
            .WithHost("127.0.0.1")
            .WithApiKeyHeader(key)
            .BuildClient();

        // Act

        // Assert
        client.AuthenticationHeader.Should().BeNull();
        client.RequestHeaders.Should().Contain(h => h.Key == "x-api-key");
    } 
}