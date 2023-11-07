using System;
using System.Net;

namespace HttpClientBuilder;

public sealed class DeserializedResponseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public DeserializedResponseException(HttpStatusCode code)
    {
        StatusCode = code;
    }
}