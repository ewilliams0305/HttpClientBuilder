using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientBuilder;

/// <summary>
/// Dispatches the request to the HTTP server.
/// When the response is received its forwarded to the <seealso cref="IDispatchHandlerFactory"/>
/// </summary>
public interface IDispatchHandler : IDisposable
{
    /// <summary>
    /// Executes the HTTP request to the server.
    /// When the server responds The associated <seealso cref="IRequestHandler"/> will be invoked.
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    Task DispatchAsync(HttpContent? body = null);
}