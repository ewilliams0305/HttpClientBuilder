using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HttpClientBuilder.Model;

namespace HttpClientBuilder
{
    /// <summary>
    /// The client builder performs the actions required to configure and build a new <seealso cref="IHttpClient"/>
    /// The IHttpClient returned from the builder is a fully configured <see cref="HttpClient"/> that can be used for
    /// request response.
    ///
    /// While the resulting <seealso cref="IHttpClient"/> is just a wrapper around the HttpClient it's augmented with an expressive API
    /// allowing consumers to configure the client for operations against specific APIs.
    /// </summary>
    public sealed class ClientBuilder : IHostBuilder, IRouteBuilder, IOptionsBuilder, IHeaderOrBuilder
    {

        private readonly BuilderConfiguration _builderConfiguration;

        /// <summary>
        /// Private constructor prevents direct access to the builder.
        /// This ensures the static factory is the only to start the configuration.
        /// </summary>
        private ClientBuilder()
        {
            _builderConfiguration = new BuilderConfiguration();
        }

        /// <summary>
        /// Starts building the http client.  This is the entry point for the ClientBuilder.
        /// The create builder factory returns a <seealso cref="IHostBuilder"/> directing the
        /// consumer to the next step, host and scheme configuration.
        /// </summary>
        /// <returns>A reference to the client builder as an IHostBuilder</returns>
        public static IHostBuilder CreateBuilder() => new ClientBuilder();

        #region URI And Authorization

        /// <inheritdoc />
        public IRouteBuilder ConfigureHost(string host, SchemeType scheme = SchemeType.Https, int? port = null)
        {
            _builderConfiguration.Host = host;
            _builderConfiguration.Port = port;
            _builderConfiguration.Scheme = scheme;
            return this;
        }

        /// <inheritdoc />
        public IAuthorizationBuilder WithBaseRoute(string route)
        {
            _builderConfiguration.BasePath = route;
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder ConfigureBasicAuthorization(string username, string password)
        {
            var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            _builderConfiguration.Authentication = new AuthenticationHeaderValue("Basic", encoded);
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder ConfigureBearerToken(string token)
        {
            _builderConfiguration.Authentication = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder ConfigureApiKeyHeader(string apiKey, string header = "x-api-key")
        {
            _builderConfiguration.Headers.Add(header, apiKey);
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder ConfigureAuthorization(Func<KeyValuePair<string, string>?> headerFunc)
        {
            var header = headerFunc?.Invoke() ?? throw new HttpClientBuilderException(nameof(ConfigureAuthorization));
            _builderConfiguration.Headers.Add(header.Key, header.Value);
            return this;
        }

        #endregion


        #region Configure Optional Headers

        /// <inheritdoc />
        public IHeaderOrBuilder WithHeader(string key, string value)
        {
            _builderConfiguration.Headers.Add(key, value);
            return this;
        }

        /// <inheritdoc />
        public IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc)
        {
            var header = (headerFunc?.Invoke()) ?? throw new HttpClientBuilderException(nameof(WithHeader));
            _builderConfiguration.Headers.Add(header.Key, header.Value);
            return this;
        }

        #endregion

        #region Custom Handlers

        /// <inheritdoc />
        public IHeaderOrBuilder ConfigureHandler(Func<HttpClientHandler> handlerFunc)
        {
            var handler = (handlerFunc?.Invoke()) ?? throw new HttpClientBuilderException(nameof(ConfigureHandler));
            _builderConfiguration.Handler = handler;
            return this;
        }

        /// <inheritdoc />
        public IHeaderOrBuilder AcceptSelfSignedCerts()
        {
            _builderConfiguration.Handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) => true
            };

            return this;
        }   

        #endregion

        #region Implementation of IClientBuilder

        /// <inheritdoc />
        public IHttpClient CreateClient(HttpClient client)
        {
            var scheme = HttpScheme.CreateScheme(_builderConfiguration.Scheme);
            var uriBuilder = scheme.ToUriBuilder(_builderConfiguration.Host,_builderConfiguration.BasePath, _builderConfiguration.Port);

            client.BaseAddress = uriBuilder.Uri;

            if (_builderConfiguration.Authentication != null)
            {
                client.DefaultRequestHeaders.Authorization = _builderConfiguration.Authentication;
            }

            foreach (var header in _builderConfiguration.Headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, new[] { header.Value });
            }

            return new HttpBuilderClient(client);
        }

        /// <inheritdoc />
        public IHttpClient CreateClient(Action<HttpClient>? clientAction = null)
        {
            var scheme = HttpScheme.CreateScheme(_builderConfiguration.Scheme);
            var uriBuilder = scheme.ToUriBuilder(_builderConfiguration.Host, _builderConfiguration.BasePath, _builderConfiguration.Port);

            var client = _builderConfiguration.Handler != null
                ? new HttpClient(_builderConfiguration.Handler)
                {
                    BaseAddress = uriBuilder.Uri
                }
                : new HttpClient
                {
                    BaseAddress = uriBuilder.Uri
                };

            if (_builderConfiguration.Authentication != null)
            {
                client.DefaultRequestHeaders.Authorization = _builderConfiguration.Authentication;
            }

            foreach (var header in _builderConfiguration.Headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, new[] { header.Value });
            }

            clientAction?.Invoke(client);
            return new HttpBuilderClient(client);
        }

        /// <inheritdoc />
        public IHttpClient CreateClient(Func<HttpClient> clientFactory)
        {
            var client = (clientFactory?.Invoke()) ?? throw new HttpClientBuilderException(nameof(CreateClient));
            var scheme = HttpScheme.CreateScheme(_builderConfiguration.Scheme);
            var uriBuilder = scheme.ToUriBuilder(_builderConfiguration.Host, _builderConfiguration.BasePath, _builderConfiguration.Port);
            client.BaseAddress = uriBuilder.Uri;

            if (_builderConfiguration.Authentication != null)
            {
                client.DefaultRequestHeaders.Authorization = _builderConfiguration.Authentication;
            }

            foreach (var header in _builderConfiguration.Headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, new[] { header.Value });
            }

            return new HttpBuilderClient(client);
        }

        #endregion

        
    }
}
