using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using System.Net;

namespace HttpClientBuilder.IntegrationTests.GetRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public class GetContentTests
    {
        private readonly IHttpClient _client;
        public GetContentTests(AppFactory factory)
        {
            // Arrange
            _client = factory.GetPrefixedClient();
        }

        [Fact]
        public async Task GetContent_DeserializedObject_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<Utils.WeatherForecast>();

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContent_DeserializedEnumerable_WithContent()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<IEnumerable<Utils.WeatherForecast>>("weatherForecast");

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetContent_ReturnsContent_WithStreamFunction()
        {
            // Arrange

            // Act
            var result = await _client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromStream: (code, stream) =>
            {
                code.Should().Be(HttpStatusCode.OK);
                stream.Should().NotBeNull();
                stream.Length.Should().BeGreaterThan(10);

                // We aren't asserting against your code, just assuming your code works!
                return Utils.BogusWeatherData.GetForecast();
            });

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContent_ReturnsContent_WithStreamFunctionAsync()
        {
            // Arrange

            // Act
            var result = await _client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromStreamAsync: (code, stream) =>
            {
                code.Should().Be(HttpStatusCode.OK);
                stream.Should().NotBeNull();
                stream.Length.Should().BeGreaterThan(10);

                // We aren't asserting against your code, just assuming your code works!
                var result = Utils.BogusWeatherData.GetForecast();

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(result);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            });

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetContent_ReturnsContent_WithBytesFunction()
        {
            // Arrange

            // Act
#pragma warning disable CS8625
            var result = await _client.GetContentAsync<Utils.WeatherForecast>(null, createResultFromBytes: (code, bytes) =>
#pragma warning restore CS8625
            {
                code.Should().Be(HttpStatusCode.OK);
                bytes.Should().NotBeNull();
                bytes.Length.Should().BeGreaterThan(10);

                // We aren't asserting against your code, just assuming your code works!
                return Utils.BogusWeatherData.GetForecast();
            });

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContent_ReturnsContent_WithBytesFunctionAsync()
        {
            // Arrange

            // Act
            var result = await _client.GetContentAsync<Utils.WeatherForecast>("", createResultFromBytesAsync: (code, bytes) =>
            {
                code.Should().Be(HttpStatusCode.OK);
                bytes.Should().NotBeNull();
                bytes.Length.Should().BeGreaterThan(10);

                // We aren't asserting against your code, just assuming your code works!
                var result = Utils.BogusWeatherData.GetForecast();

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(result);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            });

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetContent_ReturnsContent_WithHttpContentFunction()
        {
            // Arrange

            // Act
            var result = await _client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromContent: (code, content) =>
            {
                code.Should().Be(HttpStatusCode.OK);
                content.Should().NotBeNull();
                content.Should().NotBeNull();

                // We aren't asserting against your code, just assuming your code works!
                return Utils.BogusWeatherData.GetForecast();
            });

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContent_ReturnsContent_WithHttpContentFunctionAsync()
        {
            // Arrange

            // Act
            var result = await _client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromContentAsync: (code, content) =>
            {
                code.Should().Be(HttpStatusCode.OK);
                content.Should().NotBeNull();
                content.Should().NotBeNull();

                // We aren't asserting against your code, just assuming your code works!
                var result = Utils.BogusWeatherData.GetForecast();

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(result);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            });

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetContent_DoesNotProcess_BadRequests()
        {
            // Arrange

            // Act
            var result = await _client.GetContentFromJsonAsync<Utils.WeatherForecast>("/errors/400");

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
        }
    }
}