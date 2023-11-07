using System;
using System.Net;
using System.Net.Http;
using ClientBuilder.Model;

namespace ClientBuilder
{
    public sealed class ClientBuilder : IHostBuilder, IAuthorizationBuilder, IOptionsBuilder, IHeaderOrBuilder
    {
        private string _host = string.Empty;
        private int _port;
        private HttpScheme _scheme;

        /// <summary>
        /// Private constructor prevents direct access to the builder.
        /// This ensures the static factory is the only to start the configuration.
        /// </summary>
        private ClientBuilder()
        {
            
        }

        /// <summary>
        /// Starts building the http client.  This is the entry point for the ClientBuilder.
        /// The create builder factory returns a <seealso cref="IHostBuilder"/> directing the
        /// consumer to the next step, host and scheme configuration.
        /// </summary>
        /// <returns>A reference to the client builder as an IHostBuilder</returns>
        public static IHostBuilder CreateBuilder() => new ClientBuilder();

        #region Implementation of IHostBuilder

        /// <inheritdoc />
        public IAuthorizationBuilder ConfigureHost(string host, HttpScheme scheme = HttpScheme.Https, int port = 443)
        {
            _host = host;
            _port = port;
            _scheme = scheme;
            return this;
        }

        #endregion

        #region Implementation of IBasicAuthBuilder

        /// <inheritdoc />
        public IOptionsBuilder ConfigureBasicAuthorization(string username, string password)
        {
            return this;
        }

        #endregion

        #region Implementation of IBearerTokenAuthBuilder

        /// <inheritdoc />
        public IOptionsBuilder ConfigureBearerToken(string token)
        {
            return this;
        }

        #endregion

        #region Implementation of IApiKeyHeader

        /// <inheritdoc />
        public IOptionsBuilder ConfigureApiKeyHeader(string apiKey, string header = "x-api-key")
        {
            return this;
        }

        #endregion

        #region Implementation of IAuthorizationFunction

        /// <inheritdoc />
        public IOptionsBuilder ConfigureAuthorization(Func<HttpRequestHeader> headerFunc)
        {
            return this;
        }

        #endregion


        #region Implementation of IHeaderOptions

        /// <inheritdoc />
        public IHeaderOrBuilder WithHeader(string key, string value)
        {
            return this;
        }

        #endregion

        #region Implementation of ICustomerHandler

        /// <inheritdoc />
        public IHeaderOrBuilder ConfigureHandler(Func<HttpClientHandler>? handler = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IAcceptAllCerts

        /// <inheritdoc />
        public IHeaderOrBuilder AcceptSelfSignedCerts()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IClientBuilder

        public IHttpClient CreateClient()
        {
            var uriBuilder = new UriBuilder(_scheme.ToString(), _host, _port);

            var client = new HttpClient
            {
                BaseAddress = uriBuilder.Uri,
            };
            return new ConfiguredClient(client);
        }

        /// <inheritdoc />
        public IHttpClient CreateClient(HttpClient client)
        {
            return new ConfiguredClient(client);
        }

        /// <inheritdoc />
        public IHttpClient CreateClient(Action<HttpClient> clientAction)
        {
            var client = new HttpClient();
            clientAction?.Invoke(client);
            return new ConfiguredClient(client);
        }

        /// <inheritdoc />
        public IHttpClient CreateClient(Func<HttpClient> clientFactory)
        {
            var client = clientFactory?.Invoke();
            return client == null
                ? throw new HttpClientBuilderException(nameof(CreateClient))
                : new ConfiguredClient(client);
        }

        #endregion

    }
}
