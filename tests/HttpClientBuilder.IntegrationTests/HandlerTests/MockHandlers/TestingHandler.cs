using System.Net;
using System.Net.Http.Headers;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

public class TestingHandler : IRequestHandler
{
    public int Code;
    public bool Failed;

    #region Implementation of IRequestHandler

    /// <inheritdoc />
    public Task HandleRequest(HttpStatusCode code, HttpContent content)
    {
        Code = (int)code;

        return Task.CompletedTask;
    }

    #endregion
}