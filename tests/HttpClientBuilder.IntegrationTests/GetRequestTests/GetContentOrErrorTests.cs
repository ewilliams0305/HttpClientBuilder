using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;
using System.Net;

namespace HttpClientBuilder.IntegrationTests.GetRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public class GetContentOrErrorTests
    {
        private readonly IHttpClient _client;
        public GetContentOrErrorTests(AppFactory factory)
        {
            // Arrange
            _client = factory.GetPrefixedClient();
        }

        [Fact]
        public async Task GetContentTorE_DeserializedSuccess_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<Utils.WeatherForecast, ErrorResponse>("weather/error?error=false");

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContentTorE_DeserializedError_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<Utils.WeatherForecast, ErrorResponse>("weather/error?error=true");

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResponseState.HttpStatusError);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Value.Should().BeNull();
            result.ErrorValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContentTorE_DeserializedException_WithInvalidType()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<Utils.ErrorResponse, WeatherForecast>("weather/exception");

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResponseState.Exception);
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContentTorE_ResultInException_WhenThrows()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<Utils.ErrorResponse, WeatherForecast>("weather/exception");

            // Assert
            result.Status.Should().Be(ResponseState.Exception);
        }
    }
}