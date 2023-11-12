﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientBuilder.Request;

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
        if (_handler is IDisposable disposable)
        {
            disposable.Dispose();
        }
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
        if (_handler is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}


/// <summary>
/// The default implementation used by the Http Client
/// </summary>
internal class DispatchHandlerWithoutBody<TResponseBody> : IDispatchHandler 
    where TResponseBody : class
{
    private readonly string _path;
    private readonly IRequestHandler<TResponseBody> _handler;
    private readonly Func<string, Task<IResponse<TResponseBody>>> _requestFunc;

    public DispatchHandlerWithoutBody(string path, Func<string, Task<IResponse<TResponseBody>>> requestFunc, IRequestHandler<TResponseBody> handler)
    {
        _path = path;
        _requestFunc = requestFunc;
        _handler = handler;
    }

    /// <inheritdoc />
    public async Task DispatchAsync(HttpContent? body = null) => 
        await _requestFunc.Invoke(_path)
            .HandleAsync(
                valueAsync: async (code, responseBody) => await _handler.HandleBody(code, null, responseBody),
                errorAsync: async (exception) => await _handler.HandleException(exception));

    /// <inheritdoc />
    public void Dispose()
    {
        if (_handler is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}

/// <summary>
/// The default implementation used by the Http Client
/// </summary>
internal class DispatchHandlerWithBody<TResponseBody, TErrorBody> : IDispatchHandler 
    where TResponseBody : class 
    where TErrorBody : class
{
    private readonly string _path;
    private readonly IRequestHandler<TResponseBody, TErrorBody> _handler;
    private readonly Func<string, HttpContent?, Task<HttpResponseMessage?>> _requestFunc;

    public DispatchHandlerWithBody(string path, Func<string, HttpContent?, Task<HttpResponseMessage?>> requestFunc, IRequestHandler<TResponseBody, TErrorBody> handler)
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
           // await _handler.HandleRequest(response.StatusCode, response.Headers, response.Content);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_handler is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}