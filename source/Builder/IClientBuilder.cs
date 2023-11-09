using System;
using System.Net.Http;

namespace HttpClientBuilder
{
    /// <summary>
    /// The final stage in the <seealso cref="ClientBuilder"/> pipeline.
    /// The Create client method processes the builder pipeline and configures an <seealso cref="HttpClient"/>
    /// </summary>
    public interface IClientBuilder
    {
        /// <summary>
        /// Using the build client method, a new http client will be created for you.  After the client is fully configured the
        /// optional <seealso cref="Action"/> will be invoked passing a reference to the client that was created.
        /// This provides the consumer one last chance to mutate the client.
        /// </summary>
        /// <param name="clientAction">Action passing a reference of the built client.</param>
        /// <returns>An http client</returns>
        IHttpClient BuildClient(Action<HttpClient>? clientAction = null);

        /// <summary>
        /// Using the build client factory method provide the consumer of the ClientBuilder a factory that returns an <seealso cref="HttpClient"/>.
        /// The <seealso cref="ClientBuilder"/> pipeline will then be applied to this HttpClient.
        /// <remarks>See the integration testing project AppFactory for an example and reasoning behind this build client method.</remarks>
        /// <remarks>This can also help consumers leverage the IHttpClientFactory</remarks>
        /// </summary>
        /// <param name="clientFactory">Function delegate returning an instance of an Http Client.</param>
        /// <returns>An http client</returns>
        IHttpClient BuildClient(Func<HttpClient> clientFactory);
    }
}