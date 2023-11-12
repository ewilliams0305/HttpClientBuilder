using System.Net;

namespace HttpClientBuilder;

/// <summary>
/// Provides the <seealso cref="HttpStatusCode"/> as a response from an <seealso cref="IHttpClient"/> request.
/// </summary>
public interface IResponseCode
{
    /// <summary>
    /// returns the <seealso cref="HttpStatusCode"/> as a response from an <seealso cref="IHttpClient"/> request.
    /// </summary>
    HttpStatusCode? StatusCode { get; }
}