using HttpClientBuilder.Model;

namespace HttpClientBuilder
{
    public interface IHostBuilder
    {
        IRouteBuilder ConfigureHost(string host, SchemeType scheme = SchemeType.Https, int? port = null);
    }

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

    public interface IRouteBuilder: IBaseUriBuilder, IAuthorizationBuilder
    {

    }
}