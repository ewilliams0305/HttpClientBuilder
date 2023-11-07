using System.Net;
using ClientBuilder.Model;

namespace ClientBuilder
{
    public interface IHostBuilder
    {
        IAuthorizationBuilder ConfigureHost(string host, SchemeType scheme = SchemeType.Https, int? port = null);
    }
}