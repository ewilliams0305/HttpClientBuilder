using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientBuilder;

internal sealed partial class HttpBuilderClient
{

    #region Implementation of IHttpGetRequests

    /// <inheritdoc />
    public async Task<IResponse> GetAsync(string route = "", CancellationToken cancellationToken = default)
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? new Response(response.StatusCode, response.Headers)
                : new Response(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response(requestException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> GetContentFromJsonAsync<TSuccessType>(string route = "", JsonSerializerContext? context = null, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }

            var result = context == null
                ? await DeserializeType<TSuccessType>(content, JsonSerializerOptions.Default, cancellationToken)
                : await DeserializeType<TSuccessType>(content, context, cancellationToken);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessBody, TErrorBody>> GetContentFromJsonAsync<TSuccessBody, TErrorBody>(string route = "",
        CancellationToken cancellationToken = default) where TSuccessBody : class where TErrorBody : class
    {
        try
        {
            var response = await _client.GetAsync(RemoveSlashes(route), cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessBody, TErrorBody>(new EmptyBodyResponseException(response.StatusCode));
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorBody =
                    await DeserializeType<TErrorBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return errorBody != null
                    ? new Response<TSuccessBody, TErrorBody>(response.StatusCode, response.Headers, errorBody)
                    : new Response<TSuccessBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
            }

            var successBody =
                await DeserializeType<TSuccessBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

            return successBody != null
                ? new Response<TSuccessBody, TErrorBody>(response.StatusCode, response.Headers, successBody)
                : new Response<TSuccessBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessBody, TErrorBody>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessBody, TErrorBody>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessBody, TErrorBody>(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, TSuccessType?> createResultFromContent, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var result = createResultFromContent.Invoke(response.StatusCode, response.Content);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(response.StatusCode, response.Headers, new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    public async Task<IResponse<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, Task<TSuccessType?>> createResultFromContentAsync, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var result = await createResultFromContentAsync.Invoke(response.StatusCode, response.Content).ConfigureAwait(false);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(response.StatusCode, response.Headers, new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], TSuccessType?> createResultFromBytes, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }
            var result = createResultFromBytes.Invoke(response.StatusCode, content);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], Task<TSuccessType?>> createResultFromBytesAsync, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }

            var result = await createResultFromBytesAsync.Invoke(response.StatusCode, content).ConfigureAwait(false);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, TSuccessType?> createResultFromStream, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }

            var result = createResultFromStream.Invoke(response.StatusCode, content);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(new DeserializedResponseException(response.StatusCode));
        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, Task<TSuccessType?>> createResultFromStreamAsync, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        var path = RemoveSlashes(route);

        try
        {
            var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }

            var result = await createResultFromStreamAsync.Invoke(response.StatusCode, content);
            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result!)
                : new Response<TSuccessType>(new DeserializedResponseException(response.StatusCode));

        }
        catch (ArgumentException argumentException)
        {
            return new Response<TSuccessType>(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response<TSuccessType>(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response<TSuccessType>(jsonException);
        }
    }

    #endregion
}
