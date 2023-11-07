using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClientBuilder
{
    internal sealed class ConfiguredClient : IHttpClient
    {
        private readonly HttpClient _client;

        public ConfiguredClient(HttpClient client)
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
    }
}