using System.Net;
using ClientBuilder.Model;

namespace ClientBuilder
{
    public interface IHostBuilder
    {
        IAuthorizationBuilder ConfigureHost(string host, HttpScheme scheme = HttpScheme.Https, int port = 443);
    }
}