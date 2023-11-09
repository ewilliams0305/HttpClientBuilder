using HttpClientBuilder.Request;
using System;
using System.Net.Http.Headers;

namespace HttpClientBuilder;

/// <summary>
/// 
/// </summary>
public interface IHttpClient : IHttpGetRequests, IDisposable
{
    /// <summary>
    /// Provides readonly access to the URI built bby the <seealso cref="IClientBuilder"/> pipeline.
    /// </summary>
    Uri BaseAddress { get; }

    /// <summary>
    /// Returns the authentication header configured in the <seealso cref="IClientBuilder"/> pipeline.
    /// </summary>
    AuthenticationHeaderValue AuthenticationHeader { get; }

    /// <summary>
    /// Returns the default request headers configured in the <seealso cref="IClientBuilder"/> pipeline.
    /// </summary>
    HttpRequestHeaders RequestHeaders { get; }

    /// <summary>
    /// Starts a new request builder pipeline that eventually returns an <seealso cref="IRequest"/> that can be used for Http Request.
    /// <remarks>This is not yet implemented and should NOT be used.</remarks>
    /// </summary>
    /// <returns>Request builder pipeline.</returns>
    IRequestBuilder CreateRequest();
}
