using HttpClientBuilder.Request;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientBuilder;

internal sealed partial class HttpBuilderClient : IHttpClient
{

    private readonly HttpClient _client;
    
    public HttpBuilderClient(HttpClient client)
    {
        _client = client;
    }

    #region Implementation of IHttpClient

    /// <inheritdoc />
    public Uri BaseAddress => _client.BaseAddress;

    /// <inheritdoc />
    public AuthenticationHeaderValue AuthenticationHeader => _client.DefaultRequestHeaders.Authorization;

    /// <inheritdoc />
    public HttpRequestHeaders RequestHeaders => _client.DefaultRequestHeaders;

    /// <inheritdoc />
    public IRequestBuilder CreateRequest()
    {
        return new RequestBuilder();
    }

    #endregion

    #region GUARDE ROUTE

    private static string RemoveSlashes(string? path)
    {
        if (path == null)
        {
            return string.Empty;
        }
        return path.StartsWith("/") ? path.Substring(1) : path;
    }

    #endregion

    #region PRIVATE JSON HELPERS

    private static async Task<TSuccessType?> DeserializeType<TSuccessType>(Stream content, JsonSerializerOptions options, CancellationToken cancellationToken) where TSuccessType : class =>
        await JsonSerializer
            .DeserializeAsync<TSuccessType>(content, options, cancellationToken)
            .ConfigureAwait(false);

    private static async Task<TSuccessType?> DeserializeType<TSuccessType>(Stream content, JsonSerializerContext context, CancellationToken cancellationToken) where TSuccessType : class =>
        await JsonSerializer
            .DeserializeAsync(content, typeof(TSuccessType), context, cancellationToken)
            .ConfigureAwait(false) as TSuccessType;

    #endregion

    #region IDisposable

    /// <inheritdoc />
    public void Dispose()
    {
        _client?.Dispose();
    }

    #endregion

}

