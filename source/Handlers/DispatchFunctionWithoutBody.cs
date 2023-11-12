using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientBuilder;

/// <summary>
/// The default implementation used by the Http Client
/// </summary>
internal class DispatchFunctionWithoutBody : IDispatchHandler
{
    private readonly string _path;
    private readonly Func<string, Task<HttpResponseMessage?>> _requestFunc;
    private readonly Func<HttpStatusCode, HttpContent, Task> _callback;

    public DispatchFunctionWithoutBody(
        string path, 
        Func<string, Task<HttpResponseMessage?>> requestFunc, 
        Func<HttpStatusCode, HttpContent, Task> callback)
    {
        _path = path;
        _requestFunc = requestFunc;
        _callback = callback;
    }

    /// <inheritdoc />
    public async Task DispatchAsync(HttpContent? body = null)
    {
        var response = await _requestFunc.Invoke(_path);

        if (response != null)
        {
            await _callback.Invoke(response.StatusCode, response.Content);
        }
    }

    #region IDisposable

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion
}

internal class DispatchFunctionWithBody : IDispatchHandler
{
    private readonly string _path;
    private readonly Func<string, HttpContent?, Task<HttpResponseMessage?>> _requestFunc;
    private readonly Func<HttpStatusCode, HttpContent, Task> _callback;

    public DispatchFunctionWithBody(
        string path, 
        Func<string, HttpContent?, Task<HttpResponseMessage?>> requestFunc, 
        Func<HttpStatusCode, HttpContent, Task> callback)
    {
        _path = path;
        _requestFunc = requestFunc;
        _callback = callback;
    }

    /// <inheritdoc />
    public async Task DispatchAsync(HttpContent? body = null)
    {
        var response = await _requestFunc.Invoke(_path, body);

        if (response != null)
        {
            await _callback.Invoke(response.StatusCode, response.Content);
        }
    }

    #region IDisposable

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion
}