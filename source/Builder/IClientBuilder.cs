using System;
using System.Net.Http;

namespace HttpClientBuilder
{
    public interface IClientBuilder
    {
        IHttpClient CreateClient(Action<HttpClient>? clientAction = null);
        IHttpClient CreateClient(HttpClient client);
        IHttpClient CreateClient(Func<HttpClient> clientFactory);
        
    }
}