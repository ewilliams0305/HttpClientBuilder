using System.Net;
using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;
using HttpClientBuilder.IntegrationTests.Utils;
using HttpClientBuilder.Server.Models;

namespace HttpClientBuilder.IntegrationTests.PostRequestTests
{
    [Collection(Definitions.WebApiCollection)]
    public partial class PostContentContextTests
    {
        protected readonly IHttpClient Client;
        public PostContentContextTests(AppFactory factory)
        {
            // Arrange
            Client = factory.GetPrefixedClient();
        }

        [Fact]
        public async Task PostContent_DeserializedContextObject_WithContent()
        {
            // Arrange

            // Act
            var result = await Client.PostContentFromJsonAsync<Utils.WeatherForecast> ("/", null, WeatherForecastContext.Default);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task PostContent_DeserializedContextEnumerable_WithContent()
        {
            // Arrange

            // Act
            var result = await Client.PostContentFromJsonAsync<IEnumerable<Utils.WeatherForecast>>("weatherForecast", null, WeatherListForecastContext.Default);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task PostContent_DeserializedContext_WithBody()
        {
            // Arrange
            var bogus = new Echo() { Message = "HA HA HA", Timestamp = DateTime.Now };

            // Act
            var result = await Client.PostContentFromJsonAsync<Echo, Echo>("/echo", bogus);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();

            result.Value.Should().BeEquivalentTo(bogus);
        }

    }
}