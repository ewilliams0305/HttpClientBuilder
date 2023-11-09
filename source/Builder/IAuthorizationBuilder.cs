using System;
using System.Collections.Generic;

namespace HttpClientBuilder
{
    /// <summary>
    /// The authorization step in the <seealso cref="ClientBuilder"/> pipeline
    /// </summary>
    public interface IAuthorizationBuilder : 
        IBasicAuthBuilder, 
        IBearerTokenAuthBuilder, 
        IApiKeyHeader,
        IAuthorizationFunction,
        IClientBuilder
    {
    }

    /// <summary>
    /// Used for the builder pipeline to add basic authentication headers to the default requests.
    /// </summary>
    public interface IBasicAuthBuilder
    {
        /// <summary>
        /// Adds basic auth header as username and password base64 encoded and advances to the next step in the pipeline.
        /// </summary>
        /// <param name="username">Servers Username</param>
        /// <param name="password">Servers Password</param>
        /// <returns>Option step in the builder pipeline</returns>
        IOptionsBuilder WithBasicAuthorization(string username, string password);
    }

    /// <summary>
    /// Used for the builder pipeline to add basic authentication headers to the default requests.
    /// </summary>
    public interface IBearerTokenAuthBuilder
    {
        /// <summary>
        /// Adds the bearer token header and advances to the next step in the pipeline.
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>Option step in the builder pipeline</returns>
        IOptionsBuilder WithBearerToken(string token);
    }

    /// <summary>
    /// Used for the builder pipeline to add basic authentication headers to the default requests.
    /// </summary>
    public interface IApiKeyHeader
    {
        /// <summary>
        /// Adds an api key header and advances to the next step in the pipeline.
        /// </summary>
        /// <param name="apiKey">API Key Value of the header</param>
        /// <param name="header">Optional parameter to override the header key required by the server<remarks>defaults to x-api-key</remarks></param>
        /// <returns>Option step in the builder pipeline</returns>
        IOptionsBuilder WithApiKeyHeader(string apiKey, string header = "x-api-key");
    }

    /// <summary>
    /// Used for the builder pipeline to add basic authentication headers to the default requests.
    /// </summary>
    public interface IAuthorizationFunction
    {
        /// <summary>
        /// Adds a custom header by providing a function.  The consumer must provider a header key and value.
        /// The returned Key Value Pair will be added to the default request headers.
        /// </summary>
        /// <param name="headerFunc">Function to return a key value pair</param>
        /// <returns>Option step in the builder pipeline</returns>
        IOptionsBuilder WithAuthorizationFactory(Func<KeyValuePair<string, string>?> headerFunc);
    }
}