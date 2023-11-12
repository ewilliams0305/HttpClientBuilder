using System.Net;
using System.Net.Http.Headers;

namespace HttpClientBuilder;

/// <summary>
/// Headers returned from the HTTP response.
/// </summary>
public interface IResponseHeaders
{
    /// <summary>
    /// Http Headers returned from the HTTP Request.
    /// </summary>
    HttpResponseHeaders? Headers { get; }
}