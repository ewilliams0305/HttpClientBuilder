using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace HttpClientBuilder;

internal sealed partial class HttpBuilderClient
{
    #region Implementation of IHttpHandleRequests

    /// <inheritdoc />
    public IDispatchHandler CreateGetHandler(string path, IRequestHandler handler) => new DispatchHandlerWithoutBody(RemoveSlashes(path), _client.GetAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreatePostHandler(string path, IRequestHandler handler) => new DispatchHandlerWithBody(RemoveSlashes(path), _client.PostAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreatePutHandler(string path, IRequestHandler handler) => new DispatchHandlerWithBody(RemoveSlashes(path), _client.PutAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreateDeleteHandler(string path, IRequestHandler handler) => new DispatchHandlerWithoutBody(RemoveSlashes(path), _client.DeleteAsync, handler);

    /// <inheritdoc />
    public IDispatchHandler CreateGetHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler)
        where TResponseBody : class =>
        new DispatchHandlerWithoutBody<TResponseBody>(path, async (p) => await GetContentFromJsonAsync<TResponseBody>(RemoveSlashes(p)), handler);

    /// <inheritdoc />
    public IDispatchHandler CreatePostHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
	{
        return new DispatchHandlerWithBody<TResponseBody>(path, async (p, c) =>
        {
            try
            {
                var response = await _client.PostAsync(RemoveSlashes(p), c).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<TResponseBody>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
                }

                var result =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return result != null
                    ? new Response<TResponseBody>(response.StatusCode, response.Headers, result)
                    : new Response<TResponseBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody>(jsonException);
            }
        }, handler);
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePutHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
	{
        return new DispatchHandlerWithBody<TResponseBody>(path, async (p, c) =>
        {
            try
            {
                var response = await _client.PutAsync(RemoveSlashes(p), c).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<TResponseBody>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
                }

                var result =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return result != null
                    ? new Response<TResponseBody>(response.StatusCode, response.Headers, result)
                    : new Response<TResponseBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody>(jsonException);
            }
        }, handler);
    }

    /// <inheritdoc />
    public IDispatchHandler CreateDeleteHandler<TResponseBody>(string path, IRequestHandler<TResponseBody> handler) 
        where TResponseBody : class
	{
        return new DispatchHandlerWithoutBody<TResponseBody>(path, async (p) =>
        {
            try
            {
                var response = await _client.DeleteAsync(RemoveSlashes(p)).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<TResponseBody>(response.StatusCode, response.Headers, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody>(response.StatusCode, response.Headers, new EmptyBodyResponseException(response.StatusCode));
                }

                var result =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return result != null
                    ? new Response<TResponseBody>(response.StatusCode, response.Headers, result)
                    : new Response<TResponseBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody>(jsonException);
            }
        }, handler);
    }

    /// <inheritdoc />
    public IDispatchHandler CreateGetHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        return new DispatchHandlerWithoutBody<TResponseBody, TErrorBody>(path, async (p) =>
        {
            try
            {
                var response = await _client.GetAsync(RemoveSlashes(p)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody, TErrorBody>(new EmptyBodyResponseException(response.StatusCode));
                }

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody =
                        await DeserializeType<TErrorBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                    return errorBody != null
                        ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, errorBody)
                        : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
                }

                var successBody =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return successBody != null
                    ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, successBody)
                    : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody, TErrorBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody, TErrorBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody, TErrorBody>(jsonException);
            }
        }, handler);
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePostHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        return new DispatchHandlerWithBody<TResponseBody, TErrorBody>(path, async (p, c) =>
        {
            try
            {
                var response = await _client.PostAsync(RemoveSlashes(p), c).ConfigureAwait(false);
                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody, TErrorBody>(new EmptyBodyResponseException(response.StatusCode));
                }

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody =
                        await DeserializeType<TErrorBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                    return errorBody != null
                        ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, errorBody)
                        : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
                }

                var successBody =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return successBody != null
                    ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, successBody)
                    : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody, TErrorBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody, TErrorBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody, TErrorBody>(jsonException);
            }
        }, handler);
    }

    /// <inheritdoc />
    public IDispatchHandler CreatePutHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        return new DispatchHandlerWithBody<TResponseBody, TErrorBody>(path, async ( p, c) =>
        {
            try
            {
                var response = await _client.PutAsync(RemoveSlashes(p), c).ConfigureAwait(false);
                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody, TErrorBody>(new EmptyBodyResponseException(response.StatusCode));
                }

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody =
                        await DeserializeType<TErrorBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                    return errorBody != null
                        ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, errorBody)
                        : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
                }

                var successBody =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return successBody != null
                    ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, successBody)
                    : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody, TErrorBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody, TErrorBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody, TErrorBody>(jsonException);
            }
        }, handler);
    }

    /// <inheritdoc />
    public IDispatchHandler CreateDeleteHandler<TResponseBody, TErrorBody>(string path, IRequestHandler<TResponseBody, TErrorBody> handler) 
        where TResponseBody : class
        where TErrorBody : class
	{
        return new DispatchHandlerWithoutBody<TResponseBody, TErrorBody>(path, async (p) =>
        {
            try
            {
                var response = await _client.DeleteAsync(RemoveSlashes(p)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new Response<TResponseBody, TErrorBody>(new EmptyBodyResponseException(response.StatusCode));
                }

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody =
                        await DeserializeType<TErrorBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                    return errorBody != null
                        ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, errorBody)
                        : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
                }

                var successBody =
                    await DeserializeType<TResponseBody>(content, JsonSerializerOptions.Default, CancellationToken.None);

                return successBody != null
                    ? new Response<TResponseBody, TErrorBody>(response.StatusCode, response.Headers, successBody)
                    : new Response<TResponseBody, TErrorBody>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new Response<TResponseBody, TErrorBody>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new Response<TResponseBody, TErrorBody>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new Response<TResponseBody, TErrorBody>(jsonException);
            }
        }, handler);
    }

    #endregion
}