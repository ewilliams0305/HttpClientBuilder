using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using HttpClientBuilder.Server;


namespace HttpClientBuilder.IntegrationTests.ApplicationFactory
{
    public class AppFactory : WebApplicationFactory<IAssemblyMarker> , IAsyncLifetime
    {

        public IHttpClient GetDefaultClient()
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("127.0.0.1")
                .CreateClient(CreateClient);

            return client;
        } 

        public IHttpClient GetBasicAuthClient(string user, string pass)
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("127.0.0.1")
                .ConfigureBasicAuthorization(user, pass)
                .CreateClient(CreateClient);

            return client;
        }
        public IHttpClient GetBearerTokenClient(string token)
        {
            var client = ClientBuilder.CreateBuilder()
                .ConfigureHost("127.0.0.1")
                .ConfigureBearerToken(token)
                .CreateClient(CreateClient);

            return client;
        } 

        #region Implementation of IAsyncLifetime

        /// <inheritdoc />
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public new Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
