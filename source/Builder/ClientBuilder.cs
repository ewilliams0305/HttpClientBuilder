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
        /// <exception cref="HttpClientBuilderException">The builder pipeline can throw a builder exception if the required parameters at any step are ignored.</exception>
        /// <returns>A reference to the client builder as an IHostBuilder</returns>
        public static IHostBuilder CreateBuilder() => new ClientBuilder();

        #region URI And Authorization

        /// <inheritdoc />
        public IRouteBuilder WithHost(string host, SchemeType scheme = SchemeType.Https, int? port = null)
        {
            _builderConfiguration.Host = host;
            _builderConfiguration.Port = port;
            _builderConfiguration.Scheme = scheme;
            return this;
        }

        /// <inheritdoc />
        public IAuthorizationBuilder WithBaseRoute(string route)
        {
            if (route.StartsWith("/") && route.EndsWith("/"))
            {
                _builderConfiguration.BasePath = route;
                return this;
            }

            var builder = new StringBuilder();
            
            if (!route.StartsWith("/"))
            {
                builder.Append("/");
            }

            builder.Append(route);

            if (!route.EndsWith("/"))
            {
                builder.Append("/");
            }

            _builderConfiguration.BasePath = builder.ToString();
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder WithBasicAuthorization(string username, string password)
        {
            var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            _builderConfiguration.Authentication = new AuthenticationHeaderValue("Basic", encoded);
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder WithBearerToken(string token)
        {
            _builderConfiguration.Authentication = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder WithApiKeyHeader(string apiKey, string header = "x-api-key")
        {
            _builderConfiguration.Headers.Add(header, apiKey);
            return this;
        }

        /// <inheritdoc />
        public IOptionsBuilder WithAuthorizationFactory(Func<KeyValuePair<string, string>?> headerFunc)
        {
            var header = headerFunc?.Invoke() ?? throw new HttpClientBuilderException(nameof(WithAuthorizationFactory));
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
        public IHeaderOrBuilder WithHandlerFactory(Func<HttpClientHandler> handlerFunc)
        {
            var handler = (handlerFunc?.Invoke()) ?? throw new HttpClientBuilderException(nameof(WithHandlerFactory));
            _builderConfiguration.Handler = handler;
            return this;
        }

        /// <inheritdoc />
        public IHeaderOrBuilder WithSelfSignedCerts()
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
        public IHttpClient BuildClient(Action<HttpClient>? clientAction = null)
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
        public IHttpClient BuildClient(Func<HttpClient> clientFactory)
        {
            var client = (clientFactory?.Invoke()) ?? throw new HttpClientBuilderException(nameof(BuildClient));
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
