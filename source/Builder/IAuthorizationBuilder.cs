using System;
using System.Collections.Generic;

namespace HttpClientBuilder
{
    public interface IAuthorizationBuilder : 
        IBasicAuthBuilder, 
        IBearerTokenAuthBuilder, 
        IApiKeyHeader,
        IAuthorizationFunction,
        IClientBuilder
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
        IOptionsBuilder ConfigureAuthorization(Func<KeyValuePair<string, string>?> headerFunc);
    }
}