using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ClientBuilder.Model;

namespace ClientBuilder.Examples
{
    internal class BuilderExamples
    {
        public BuilderExamples()
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("172.26.6.104")
                .ConfigureAuthorization(() => new HttpRequestHeader())
                .ConfigureHandler(()=> new HttpClientHandler())
                .WithHeader("", "")
                .CreateClient();
        }
    }
}
