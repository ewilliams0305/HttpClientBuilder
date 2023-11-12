using System.Net;
using System.Net.Http.Headers;

namespace HttpClientBuilder.IntegrationTests.HandlerTests;

public class TestingDisposableHandler : IDisposableRequestHandler
{
    public int Code;
    public bool Disposed;

    #region Implementation of IRequestHandler

    /// <inheritdoc />
    public Task HandleRequest(HttpStatusCode code, HttpContent content)
    {
        Code = (int)code;
        return Task.CompletedTask;
    }

    #endregion

    #region IDisposable

    /// <inheritdoc />
    public void Dispose()
    {
        Disposed = true;
        GC.SuppressFinalize(this);
    }

    #endregion
}