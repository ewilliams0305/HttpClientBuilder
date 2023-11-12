using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;
using System.Net;
using HttpClientBuilder.Server.Models;

namespace HttpClientBuilder.IntegrationTests.PostRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public class PostContentOrErrorTests
    {
        private readonly IHttpClient _client;
        public PostContentOrErrorTests(AppFactory factory)
        {
            // Arrange
            _client = factory.GetPrefixedClient();
        }

        [Fact]
        public async Task PostContentTorE_DeserializedSuccess_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<Utils.WeatherForecast, ErrorResponse, WeatherForecast>("weather/error?error=false");

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task PostContentTorE_DeserializedError_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<Utils.WeatherForecast, ErrorResponse>("weather/error?error=true", null, null, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResponseState.HttpStatusError);
        }

        [Fact]
        public async Task PostContentTorE_DeserializedException_WithInvalidType()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<Utils.ErrorResponse, WeatherForecast>("weather/exception", null, null, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResponseState.HttpStatusError);
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public async Task PostContentTorE_ResultInException_WhenThrows()
        {
            // Arrange

            // Act
            var result = await _client.PostContentFromJsonAsync<Utils.ErrorResponse, WeatherForecast>("weather/exception", null, null, CancellationToken.None);

            // Assert
            result.Status.Should().Be(ResponseState.HttpStatusError);
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public async Task PostContentTorE_SendBody_WithContent()
        {
            // Arrange
            var bogus = new Echo() { Message = "Hello World", Timestamp = DateTime.Now };

            // Act
            var result = await _client.PostContentFromJsonAsync<Echo, ErrorResponse, Echo>("/echo", bogus);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(bogus);
        }
    }
}