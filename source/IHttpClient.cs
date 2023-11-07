using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClientBuilder
{
    public interface IHttpClient : IDisposable
    {
        Uri BaseAddress { get; }

        AuthenticationHeaderValue AuthenticationHeader { get; }

        HttpRequestHeaders RequestHeaders { get; }
    }
}