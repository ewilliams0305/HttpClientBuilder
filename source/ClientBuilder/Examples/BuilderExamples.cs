using System.Collections.Generic;
using System.Net.Http;

namespace ClientBuilder.Examples
{
    internal class BuilderExamples
    {
        public BuilderExamples()
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("172.26.6.104")
                .ConfigureAuthorization(() => new KeyValuePair<string, string>("", ""))
                .ConfigureHandler(()=> new HttpClientHandler())
                .WithHeader("", "")
                .CreateClient();
        }
    }
}
