using System.Net.Http;
using System;

namespace HttpClientBuilder;

/// <summary>
/// Configures and <seealso cref="HttpClientHandler"/> to use for all <seealso cref="HttpMethod"/> requests.
/// </summary>
public interface IHandlerBuilder : ICustomerHandler, IAcceptAllCerts
{
        
}

/// <summary>
/// Adds a preconfigured handler to accept self signed certs.
/// </summary>
public interface IAcceptAllCerts
{
    /// <summary>
    /// Appends a handler to the pipeline that will accept all security errors and self signed certs.
    /// This should be used with caution and only when actually required..
    /// </summary>
    /// <returns>Next step in the pipeline</returns>
    IHeaderOrBuilder WithSelfSignedCerts();
}

/// <summary>
/// Adds a factory to provide a custom handler to all Http Requests.
/// </summary>
public interface ICustomerHandler
{
    /// <summary>
    /// Appends the provided handler
    /// </summary>
    /// <param name="handlerFunc">Function that returns a handler.</param>
    /// <returns>Next step in the pipeline</returns>
    IHeaderOrBuilder WithHandlerFactory(Func<HttpClientHandler> handlerFunc);
}