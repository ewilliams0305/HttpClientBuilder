using System;
using System.Net;

namespace HttpClientBuilder;

/// <summary>
/// An exception returned in the <seealso cref="IRequestResult{TSuccessValue}"/> when a content body is empty that should be deserialized into a strongly typed object.
/// </summary>
public sealed class EmptyBodyResponseException : Exception
{
    /// <summary>
    /// The status code of the Http Response that returned empty content.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Creates a new exception with the status code.
    /// </summary>
    /// <param name="code">Status Code</param>
    public EmptyBodyResponseException(HttpStatusCode code)
    {
        StatusCode = code;
    }
}