using System;
using System.Net;

namespace HttpClientBuilder;

public sealed class HttpRequestResponseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public HttpRequestResponseException(HttpStatusCode code)
    {
        StatusCode = code;
    }
}