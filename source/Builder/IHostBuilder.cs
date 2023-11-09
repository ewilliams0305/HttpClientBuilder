using System;
using System.Net.Http;
using HttpClientBuilder.Model;

namespace HttpClientBuilder
{
    /// <summary>
    /// Configures the host and scheme of the <seealso cref="HttpClient"/>
    /// </summary>
    public interface IHostBuilder
    {
        /// <summary>
        /// Configures the clients base <seealso cref="Uri"/>
        /// </summary>
        /// <param name="host">Ip address or host</param>
        /// <param name="scheme">Specify an Http Scheme of either Http or Https.
        /// <remarks>Default scheme is provided as Https</remarks>
        /// </param>
        /// <param name="port">Override the default TCP port for the Https or Http server</param>
        /// <returns>Next step in the builder pipeline</returns>
        IRouteBuilder WithHost(string host, SchemeType scheme = SchemeType.Https, int? port = null);
    }

    /// <summary>
    /// Appends a default route to the Http Client.
    /// <remarks>"/api/v2" with a host of 127.0.0.1 will produce https://127.0.0.1/api/v2</remarks>
    /// </summary>
    public interface IBaseUriBuilder
    {
        /// <summary>
        /// Appends an optional base url to the http requests.
        /// The provided string should be a valid path such as 'api' or 'api/v1'
        /// </summary>
        /// <param name="route">Route to append to all requests</param>
        /// <returns>The authorization configuration context</returns>
        IAuthorizationBuilder WithBaseRoute(string route);
    }

    /// <summary>
    /// Proceeds the <seealso cref="ClientBuilder"/> pipeline to the next step in the build pipeline
    /// advancing the consumers to append a route or configure authorization.
    /// </summary>
    public interface IRouteBuilder: IBaseUriBuilder, IAuthorizationBuilder
    {

    }
}