using FluentAssertions;
using HttpClientBuilder.IntegrationTests.ApplicationFactory;

namespace HttpClientBuilder.IntegrationTests.HandlerTests
{
    [Collection(Definitions.WebApiCollection)]
    public class HandlerCallbackTests
    {
        private readonly IHttpClient _client;
        public HandlerCallbackTests(AppFactory factory)
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
        public async Task GetHandler_ReceivesCorrectStatusCode_WhenRequestIsSuccessful(int code)
        {
            // Arrange
            var result = 0;

            var dispatch = _client.CreateGetHandler($"status/{code}",  (statusCode, headers, arg3) =>
            {
                result = (int)statusCode;
                return Task.CompletedTask;
            });

            // Act
            await dispatch.DispatchAsync();

            // Assert
            result.Should().Be(code);
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
        public async Task PostHandler_ReceivesCorrectStatusCode_WhenRequestIsSuccessful(int code)
        {
            // Arrange
            var result = 0;

            var dispatch = _client.CreatePostHandler($"status/{code}",  (statusCode, headers, arg3) =>
            {
                result = (int)statusCode;
                return Task.CompletedTask;
            });

            // Act
            await dispatch.DispatchAsync();

            // Assert
            result.Should().Be(code);
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
        public async Task PutHandler_ReceivesCorrectStatusCode_WhenRequestIsSuccessful(int code)
        {
            // Arrange
            var result = 0;

            var dispatch = _client.CreatePutHandler($"status/{code}",  (statusCode, headers, arg3) =>
            {
                result = (int)statusCode;
                return Task.CompletedTask;
            });

            // Act
            await dispatch.DispatchAsync();

            // Assert
            result.Should().Be(code);
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
        public async Task DeleteHandler_ReceivesCorrectStatusCode_WhenRequestIsSuccessful(int code)
        {
            // Arrange
            var result = 0;

            var dispatch = _client.CreateDeleteHandler($"status/{code}",  (statusCode, headers, arg3) =>
            {
                result = (int)statusCode;
                return Task.CompletedTask;
            });

            // Act
            await dispatch.DispatchAsync();

            // Assert
            result.Should().Be(code);
        }
    }
}