using System;
using System.Net;

namespace HttpClientBuilder;

/// <summary>
/// Http exception returned in the <seealso cref="IResponse{TSuccessValue}"/> when the <seealso cref="HttpStatusCode"/> return a failure response.
/// </summary>
public sealed class HttpRequestResponseException : Exception
{
    /// <summary>
    /// The status code returned by the http request.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Creates a new exception with the status code returned from the request.
    /// </summary>
    /// <param name="code">The Http status code</param>
    public HttpRequestResponseException(HttpStatusCode code)
    {
        StatusCode = code;
    }
}