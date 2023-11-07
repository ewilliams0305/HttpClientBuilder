using System;
using System.Net.Http;

namespace ClientBuilder
{
    public interface IClientBuilder
    {
        IHttpClient CreateClient();
        IHttpClient CreateClient(HttpClient client);
        IHttpClient CreateClient(Action<HttpClient> clientAction);
        IHttpClient CreateClient(Func<HttpClient> clientFactory);
        
    }
}