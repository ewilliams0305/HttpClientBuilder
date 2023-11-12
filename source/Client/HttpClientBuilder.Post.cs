using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientBuilder;

internal sealed partial class HttpBuilderClient
{
    #region Implementation of IHttpPostRequests

    /// <inheritdoc />
    public async Task<IResponse> PostAsync(string route = "", HttpContent? body = default, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _client.PostAsync(RemoveSlashes(route), body, cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode 
                ? new Response(response.StatusCode, new HttpRequestResponseException(response.StatusCode)) 
                : new Response(response.StatusCode, response.Headers);
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
    public async Task<IResponse> PostAsync<TRequestBody>(string route = "", TRequestBody? body = default,
        CancellationToken cancellationToken = default) where TRequestBody : class
    {

        try
        {
            var contentBody = body != null
                ? new StringContent(JsonSerializer.Serialize<TRequestBody>(body!), Encoding.UTF8, "application/json")
                : null;
            var response = await _client.PostAsync(RemoveSlashes(route), contentBody, cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? new Response(response.StatusCode, new HttpRequestResponseException(response.StatusCode))
                : new Response(response.StatusCode, response.Headers);
        }
        catch (ArgumentException argumentException)
        {
            return new Response(argumentException);
        }
        catch (HttpRequestException requestException)
        {
            return new Response(requestException);
        }
        catch (JsonException jsonException)
        {
            return new Response(jsonException);
        }
    }

    /// <inheritdoc />
    public async Task<IResponse<TSuccessType>> PostContentFromJsonAsync<TSuccessType>(string route = "", HttpContent? body = default, JsonSerializerContext? context = null, CancellationToken cancellationToken = default) where TSuccessType : class
    {
        try
        {
            var response = await _client.PostAsync(RemoveSlashes(route), body, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }

            var result =
                await DeserializeType<TSuccessType>(content, JsonSerializerOptions.Default, CancellationToken.None);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result)
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
    public async Task<IResponse<TSuccessType>> PostContentFromJsonAsync<TSuccessType, TRequestBody>(string route = "",
        TRequestBody? body = default, JsonSerializerContext? context = null,
        CancellationToken cancellationToken = default) where TSuccessType : class where TRequestBody : class
    {
        try
        {
            var contentBody = body != null
                ? new StringContent(JsonSerializer.Serialize<TRequestBody>(body!), Encoding.UTF8, "application/json")
                : null;

            var response = await _client.PostAsync(RemoveSlashes(route), contentBody, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
            }

            var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (content == null)
            {
                return new Response<TSuccessType>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
            }

            var result =
                await DeserializeType<TSuccessType>(content, JsonSerializerOptions.Default, CancellationToken.None);

            return result != null
                ? new Response<TSuccessType>(response.StatusCode, response.Headers, result)
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
    public async Task<IResponse<TSuccessBody, TErrorBody>> PostContentFromJsonAsync<TSuccessBody, TErrorBody>(string route = "", HttpContent? body = default,
        CancellationToken cancellationToken = default) where TSuccessBody : class where TErrorBody : class
    {
        try
        {
            var response = await _client.PostAsync(RemoveSlashes(route), body, cancellationToken).ConfigureAwait(false);
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
    public async Task<IResponse<TSuccessBody, TErrorBody>> PostContentFromJsonAsync<TSuccessBody, TErrorBody, TRequestBody>(string route = "", TRequestBody? body = default, CancellationToken cancellationToken = default) where TSuccessBody : class where TErrorBody : class where TRequestBody : class
    {
        try
        {
            var contentBody = body != null
                ? new StringContent(JsonSerializer.Serialize<TRequestBody>(body!), Encoding.UTF8, "application/json")
                : null;

            var response = await _client.PostAsync(RemoveSlashes(route), contentBody, cancellationToken).ConfigureAwait(false);

            var strings = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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

    #endregion
}
