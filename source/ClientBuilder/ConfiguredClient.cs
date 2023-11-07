using System.Net.Http;

namespace ClientBuilder
{
    internal sealed class ConfiguredClient : IHttpClient
    {
        private readonly HttpClient _client;

        public ConfiguredClient(HttpClient client)
        {
            _client = client;
        }

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            _client?.Dispose();
        }

        #endregion
    }
}