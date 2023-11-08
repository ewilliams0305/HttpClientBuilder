using System.Net;
using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.GetRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public partial class GetContentTests
    {
        protected readonly IHttpClient Client;
        public GetContentTests(AppFactory factory)
        {
            // Arrange
            Client = factory.GetDefaultClient();
        }

        [Fact]
        public async Task GetContent_DeserializedObject_WithContent()
        {
            // Arrange

            // Act
            var result = await Client.GetContentAsync<Utils.WeatherForecast> ("/");

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
            var result = await Client.GetContentAsync<IEnumerable<Utils.WeatherForecast>>("weatherForecast");

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
            var result = await Client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromStream: (code, stream) =>
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
            var result = await Client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromStreamAsync: (code, stream) =>
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
            var result = await Client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromBytes: (code, bytes) =>
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
            var result = await Client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromBytesAsync: (code, bytes) =>
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
            var result = await Client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromContent: (code, content) =>
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
            var result = await Client.GetContentAsync<Utils.WeatherForecast>("/", createResultFromContentAsync: (code, content) =>
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
    }
}