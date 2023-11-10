using HttpClientBuilder.Request;
using HttpClientBuilder.Requests;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientBuilder
{
    internal sealed class HttpBuilderClient : IHttpClient
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

        #region Implementation of IHttpHandleRequests

        /// <inheritdoc />
        public IDispatchHandler CreateGetHandler(string path, IRequestHandler handler) => new DispatchHandlerWithoutBody(path, _client.GetAsync, handler);

        /// <inheritdoc />
        public IDispatchHandler CreatePostHandler(string path,  IRequestHandler handler) => new DispatchHandlerWithBody(path, _client.PostAsync, handler);

        /// <inheritdoc />
        public IDispatchHandler CreatePutHandler(string path, IRequestHandler handler) => new DispatchHandlerWithBody(path, _client.PutAsync, handler);

        /// <inheritdoc />
        public IDispatchHandler CreateDeleteHandler(string path, IRequestHandler handler) => new DispatchHandlerWithoutBody(path, _client.DeleteAsync, handler);

        #endregion

        #region Implementation of IHttpGetRequests

        /// <inheritdoc />
        public async Task<IResponse> GetAsync(string route = "", CancellationToken cancellationToken = default)
        {
            var path = RemoveSlashes(route);

            try
            {
                var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);

                return response.IsSuccessStatusCode
                    ? new Response(response.StatusCode)
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(response.StatusCode, new EmptyBodyResponseException(response.StatusCode));
                }

                var result = context == null
                    ? await DeserializeType<TSuccessType>(content, JsonSerializerOptions.Default, cancellationToken)
                    : await DeserializeType<TSuccessType>(content, context, cancellationToken);

                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var result = createResultFromContent.Invoke(response.StatusCode, response.Content);

                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(response.StatusCode, new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var result = await createResultFromContentAsync.Invoke(response.StatusCode, response.Content).ConfigureAwait(false);

                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(response.StatusCode, new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(response.StatusCode, new EmptyBodyResponseException(response.StatusCode));
                }
                var result = createResultFromBytes.Invoke(response.StatusCode, content);

                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(response.StatusCode, new EmptyBodyResponseException(response.StatusCode));
                }

                var result = await createResultFromBytesAsync.Invoke(response.StatusCode, content).ConfigureAwait(false);

                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(response.StatusCode, new EmptyBodyResponseException(response.StatusCode));
                }

                var result = createResultFromStream.Invoke(response.StatusCode, content);

                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(new DeserializedResponseException(response.StatusCode));
            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
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
                    return new RequestResult<TSuccessType>(response.StatusCode, new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(response.StatusCode, new EmptyBodyResponseException(response.StatusCode));
                }
                    
                var result = await createResultFromStreamAsync.Invoke(response.StatusCode, content);
                return result != null
                    ? new RequestResult<TSuccessType>(response.StatusCode, result!)
                    : new RequestResult<TSuccessType>(new DeserializedResponseException(response.StatusCode));

            }
            catch (ArgumentException argumentException)
            {
                return new RequestResult<TSuccessType>(argumentException);
            }
            catch (HttpRequestException requestException)
            {
                return new RequestResult<TSuccessType>(requestException);
            }
            catch (JsonException jsonException)
            {
                return new RequestResult<TSuccessType>(jsonException);
            }
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
}