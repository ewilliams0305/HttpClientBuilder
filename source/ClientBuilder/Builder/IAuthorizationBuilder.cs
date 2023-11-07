using System;
using System.Net;

namespace ClientBuilder
{
    public interface IAuthorizationBuilder : 
        IBasicAuthBuilder, 
        IBearerTokenAuthBuilder, 
        IApiKeyHeader,
        IAuthorizationFunction
    {
    }

    public interface IBasicAuthBuilder
    {
        IOptionsBuilder ConfigureBasicAuthorization(string username, string password);
    }

    public interface IBearerTokenAuthBuilder
    {
        IOptionsBuilder ConfigureBearerToken(string token);
    }

    public interface IApiKeyHeader
    {
        IOptionsBuilder ConfigureApiKeyHeader(string apiKey, string header = "x-api-key");
    }

    public interface IAuthorizationFunction
    {
        IOptionsBuilder ConfigureAuthorization(Func<HttpRequestHeader> headerFunc);
    }
}