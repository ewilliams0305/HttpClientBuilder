using System;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientBuilder.Requests;

namespace HttpClientBuilder;

/// <summary>
/// The default implementation used by the Http Client
/// </summary>
internal class DispatchHandlerWithoutBody : IDispatchHandler
{
    private readonly string _path;
    private readonly IRequestHandler _handler;
    private readonly Func<string, Task<HttpResponseMessage?>> _requestFunc;

    public DispatchHandlerWithoutBody(string path, Func<string, Task<HttpResponseMessage?>> requestFunc, IRequestHandler handler)
    {
        _path = path;
        _requestFunc = requestFunc;
        _handler = handler;
    }

    /// <inheritdoc />
    public async Task DispatchAsync(HttpContent? body = null)
    {
        var response = await _requestFunc.Invoke(_path);

        if (response != null)
        {
            await _handler.HandleRequest(response.StatusCode, response.Headers, response.Content);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _handler.Dispose();
    }
}

/// <summary>
/// The default implementation used by the Http Client
/// </summary>
internal class DispatchHandlerWithBody : IDispatchHandler
{
    private readonly string _path;
    private readonly IRequestHandler _handler;
    private readonly Func<string, HttpContent?, Task<HttpResponseMessage?>> _requestFunc;

    public DispatchHandlerWithBody(string path, Func<string, HttpContent?, Task<HttpResponseMessage?>> requestFunc, IRequestHandler handler)
    {
        _path = path;
        _requestFunc = requestFunc;
        _handler = handler;
    }

    /// <inheritdoc />
    public async Task DispatchAsync(HttpContent? body = null)
    {
        var response = await _requestFunc.Invoke(_path, body);

        if (response != null)
        {
            await _handler.HandleRequest(response.StatusCode, response.Headers, response.Content);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _handler.Dispose();
    }
}