using System;
using System.Net;

namespace HttpClientBuilder;

public sealed class EmptyBodyResponseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public EmptyBodyResponseException(HttpStatusCode code)
    {
        StatusCode = code;
    }
}