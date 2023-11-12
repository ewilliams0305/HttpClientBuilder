using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;

namespace HttpClientBuilder.IntegrationTests.HandlerTests
{
    [Collection(Definitions.WebApiCollection)]
    public class PostBasicHandlerTests
    {
        private readonly IHttpClient _client;
        public PostBasicHandlerTests(AppFactory factory)
        {
            // Arrange
            _client = factory.GetPrefixedClient();
        }

        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(202)]
        [InlineData(400)]
        [InlineData(401)]
        [InlineData(402)]
        [InlineData(403)]
        [InlineData(404)]
        [InlineData(500)]
        [InlineData(501)]
        public async Task Handler_ReceivesCorrectStatusCode_WhenRequestIsSuccessful(int code)
        {
            // Arrange
            var handler = new TestingHandler();
            var dispatch = _client.CreatePostHandler($"status/{code}", handler);

            // Act
            await dispatch.DispatchAsync();

            // Assert
            handler.Should().NotBeNull();
            handler.Code.Should().Be(code);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(202)]
        [InlineData(400)]
        [InlineData(401)]
        [InlineData(402)]
        [InlineData(403)]
        [InlineData(404)]
        [InlineData(500)]
        [InlineData(501)]
        public async Task DisposableHandler_ReceivesCorrectStatusCode_WhenRequestIsSuccessful(int code)
        {
            // Arrange
            var handler = new TestingDisposableHandler();
            var dispatch = _client.CreatePostHandler($"status/{code}", handler);

            // Act
            await dispatch.DispatchAsync();

            // Assert
            handler.Should().NotBeNull();
            handler.Code.Should().Be(code);
        }

        [Fact]
        public async Task DisposableHandler_IsDisposed_WhenRequestCompletes()
        {
            // Arrange
            TestingDisposableHandler handler;

            // Arrange
            using (handler = new TestingDisposableHandler())
            {
                var dispatch = _client.CreatePostHandler($"status/200", handler);
                await dispatch.DispatchAsync();
                handler.Should().NotBeNull();
            }

            // Assert
            handler.Disposed.Should().BeTrue();
        }
    }
}