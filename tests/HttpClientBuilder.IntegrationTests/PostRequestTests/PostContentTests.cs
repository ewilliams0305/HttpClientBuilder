using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using System.Net;

namespace HttpClientBuilder.IntegrationTests.PostRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public class PostContentTests
    {
        private readonly IHttpClient _client;
        public PostContentTests(AppFactory factory)
        {
            // Arrange
            _client = factory.GetPrefixedClient();
        }

        [Fact]
        public async Task PostContent_DeserializedObject_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<Utils.WeatherForecast>();

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task PostContent_DeserializedEnumerable_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<IEnumerable<Utils.WeatherForecast>>("weatherForecast");

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().NotBeEmpty();
        }

        [Fact]
        public async Task PostContent_DoesNotProcess_BadRequests()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<Utils.WeatherForecast>("/errors/400");

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
        }
    }
}