using System.Net;
using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;

namespace HttpClientBuilder.IntegrationTests.GetRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public partial class GetContentContextTests
    {
        protected readonly IHttpClient Client;
        public GetContentContextTests(AppFactory factory)
        {
            // Arrange
            Client = factory.GetPrefixedClient();
        }

        [Fact]
        public async Task GetContent_DeserializedContextObject_WithContent()
        {
            // Arrange

            // Act
            var result = await Client.GetContentFromJsonAsync<Utils.WeatherForecast> ("/", WeatherForecastContext.Default);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContent_DeserializedContextEnumerable_WithContent()
        {
            // Arrange

            // Act
            var result = await Client.GetContentFromJsonAsync<IEnumerable<Utils.WeatherForecast>>("weatherForecast", WeatherListForecastContext.Default);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().NotBeEmpty();
        }
    }
}