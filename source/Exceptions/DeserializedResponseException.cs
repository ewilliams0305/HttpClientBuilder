using System;
using System.Net;

namespace HttpClientBuilder;

/// <summary>
/// Exception when attempting to deserialize a JSON content response.
/// If the <seealso cref="IHttpClient"/> fails to convert the content body to the provided type this exception will be returned in the <seealso cref="IResponse{TSuccessValue}"/>.
/// <remarks>This exception will be returned any time the status code is a successful code, but the content body deserialization returns null.</remarks>
/// </summary>
public sealed class DeserializedResponseException : Exception
{
    /// <summary>
    /// The status code of the Http Response that failed to deserialize.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Creates a new exception with the status code.
    /// </summary>
    /// <param name="code">Status Code</param>
    public DeserializedResponseException(HttpStatusCode code)
    {
        StatusCode = code;
    }
}