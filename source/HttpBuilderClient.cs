﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HttpClientBuilder.Request;

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

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            _client?.Dispose();
        }

        #endregion

        #region Implementation of IHttpGetRequests

        /// <inheritdoc />
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route = "/", CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                
                var result = await JsonSerializer
                    .DeserializeAsync<TSuccessType>(content, JsonSerializerOptions.Default, cancellationToken)
                    .ConfigureAwait(false);

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
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, TSuccessType?> createResultFromContent, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var result = createResultFromContent.Invoke(response.StatusCode, response.Content);

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
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, HttpContent, Task<TSuccessType?>> createResultFromContentAsync, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var result = await createResultFromContentAsync.Invoke(response.StatusCode, response.Content).ConfigureAwait(false);

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
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], TSuccessType?> createResultFromBytes, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(new EmptyBodyResponseException(response.StatusCode));
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, byte[], Task<TSuccessType?>> createResultFromBytesAsync, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(new EmptyBodyResponseException(response.StatusCode));
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, TSuccessType?> createResultFromStream, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(new EmptyBodyResponseException(response.StatusCode));
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IRequestResult<TSuccessType>> GetContentAsync<TSuccessType>(string route, Func<HttpStatusCode, Stream, Task<TSuccessType?>> createResultFromStreamAsync, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(route, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new RequestResult<TSuccessType>(new HttpRequestResponseException(response.StatusCode));
                }

                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (content == null)
                {
                    return new RequestResult<TSuccessType>(new EmptyBodyResponseException(response.StatusCode));
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
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}