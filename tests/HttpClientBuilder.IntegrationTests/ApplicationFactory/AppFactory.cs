using HttpClientBuilder.Server;
using Microsoft.AspNetCore.Mvc.Testing;


namespace HttpClientBuilder.IntegrationTests.ApplicationFactory
{
    public class AppFactory : WebApplicationFactory<IAssemblyMarker> , IAsyncLifetime
    {
        public IHttpClient GetBadHostClient()
        {
            var client = ClientBuilder.CreateBuilder()
                .WithHost("10.12.25.6")
                .BuildClient(CreateClient);

            return client;
        }

        public IHttpClient GetDefaultClient()
        {
            var client = ClientBuilder.CreateBuilder()
                .WithHost("127.0.0.1")
                .BuildClient(CreateClient);

            return client;
        }

        public IHttpClient GetPrefixedClient()
        {
            var client = ClientBuilder.CreateBuilder()
                .WithHost("127.0.0.1")
                .WithBaseRoute("api")
                .BuildClient(CreateClient);

            return client;
        }

        public IHttpClient GetBasicAuthClient(string user, string pass)
        {
            var client = ClientBuilder.CreateBuilder()
                .WithHost("127.0.0.1")
                .WithBasicAuthorization(user, pass)
                .BuildClient(CreateClient);

            return client;
        }
        public IHttpClient GetBearerTokenClient(string token)
        {
            var client = ClientBuilder.CreateBuilder()
                .WithHost("127.0.0.1")
                .WithBearerToken(token)
                .BuildClient(CreateClient);

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
